using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Classes.Effects;
using UnityEngine;

namespace Assets.Classes.Weapons
{
    class LaserWeapon : Weapon
    {
        public float CooldownTime { get; set; }
        public float OverheatTime { get; set; }
        public float Intensity { get; set; }
        public float MaxLaserBeamLength { get; set; }

        public Transform ShootingPoint;
        public LineRenderer LaserBeam;
        public GameObject LaserBeamParticlesPrefab;
        public MonoBehaviour DummyMonoBehaviour; //e.g. for starting coroutines

        private float currentHeatLevel = 0;
        private float maxHeatLevel = 100;
        private bool overheated = false;
        private bool isShooting = false;

        private GameObject particles;
        private GameObject target;
        private Vector3 targetHitPoint;
        private Vector3 targetHitDirection;

        public override bool CanShoot()
        {
            return (currentHeatLevel < maxHeatLevel) && (!overheated);
        }

        public override bool Fire()
        {
            if (!isShooting)
                StartShooting();
            else
                StopShooting();

            return true;
        }

        public override void AddLongEffect(LongEffect newEffect)
        {
            Debug.LogWarning("Laser weapons can't have long effects!");
        }

        private void StartShooting()
        {
            isShooting = true;

            DummyMonoBehaviour.StartCoroutine(HittingTarget());
            DummyMonoBehaviour.StartCoroutine(DrawLaserBeam());
        }

        private IEnumerator HittingTarget()
        {
            while (isShooting)
            {
                FindTarget();
                HitTarget();

                currentHeatLevel += (maxHeatLevel / OverheatTime) * Intensity;
                if (currentHeatLevel >= maxHeatLevel)
                {
                    currentHeatLevel = maxHeatLevel;
                    Overheat();
                }
                Debug.Log("HeatLevel: " + currentHeatLevel);

                yield return new WaitForSeconds(Intensity);
            }
        }

        private IEnumerator DrawLaserBeam()
        {
            float waitTime = 0.001f;


            while (isShooting)
            {
                FindTarget();

                LaserBeam.SetPosition(0, ShootingPoint.position);
                if (target != null)
                {
                    LaserBeam.SetPosition(1, targetHitPoint);

                    if (particles == null)
                        particles = Instantiate(LaserBeamParticlesPrefab);
                    particles.transform.position = targetHitPoint;
                    particles.transform.rotation = Quaternion.LookRotation(targetHitDirection);
                }
                else
                {
                    LaserBeam.SetPosition(1, ShootingPoint.position + ShootingPoint.forward * MaxLaserBeamLength);

                    if (particles != null)
                        Destroy(particles);
                }

                LaserBeam.enabled = true;

                yield return new WaitForSeconds(waitTime);
            }

            LaserBeam.enabled = false;
        }

        private void HitTarget()
        {
            if (target == null)
                return;

            effectHolder.ActivateInstantEffects(target);
            // LaserWeapon can't have LongEffects, so we don't have to pass those
        }

        private void FindTarget()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(ShootingPoint.position, ShootingPoint.forward, out hitInfo, MaxLaserBeamLength))
            {
                target = hitInfo.collider.gameObject;

                targetHitPoint = hitInfo.point;
                targetHitDirection = -1f * ShootingPoint.forward;
            }
            else
            {
                target = null;
            }
        }

        private void Overheat()
        {
            overheated = true;
            StopShooting();
        }

        private void StopShooting()
        {
            isShooting = false;
            LaserBeam.enabled = false;
            Destroy(particles);

            DummyMonoBehaviour.StartCoroutine(ManageCooldown());
        }

        private IEnumerator ManageCooldown()
        {
            float waitTime = 0.1f;

            while (!isShooting)
            {
                yield return new WaitForSeconds(waitTime);

                if (!isShooting)
                {
                    currentHeatLevel -= (maxHeatLevel / CooldownTime) * waitTime;
                    if (currentHeatLevel <= 0)
                    {
                        currentHeatLevel = 0;
                        overheated = false;
                        Debug.Log("HeatLevel: " + currentHeatLevel);
                        break;
                    }
                    Debug.Log("HeatLevel: " + currentHeatLevel);
                }
            }
        }
    }
}
