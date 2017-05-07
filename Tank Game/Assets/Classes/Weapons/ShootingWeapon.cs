using Assets.Classes.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes.Weapons
{
    public class ShootingWeapon : Weapon
    {
        public GameObject BulletPrefab;
        public Transform ShootingPoint;

        public float defaultBulletSpeed = 10f;
        public int defaultNumberOfBullets = 3;

        public float BulletSpeed { get; set; }
        private int numberOfBullets;
        public int NumberOfBullets
        {
            get
            {
                return numberOfBullets;
            }
            set
            {
                if (value < 0)
                    numberOfBullets = 0;
                else if (value > defaultNumberOfBullets)
                    numberOfBullets = defaultNumberOfBullets;
                else
                    numberOfBullets = value;
            }
        }

        protected virtual void Awake()
        {
            Debug.Log("SW Awake meghivva");
            ResetBulletSpeed();
            ReloadAllBullets();
        }

        public void ResetBulletSpeed()
        {
            BulletSpeed = defaultBulletSpeed;
        }

        public void ReloadAllBullets()
        {
            NumberOfBullets = defaultNumberOfBullets;
        }

        protected bool CanShoot()
        {
            return NumberOfBullets > 0;
        }

        public override void Fire()
        {
            if (!CanShoot())
                return;

            GameObject bullet = (GameObject) Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);

            bullet.GetComponent<Bullet>().SetEffectHolder(effectHolder);

            Vector3 bulletVelocity = bullet.transform.forward * BulletSpeed;
            bullet.GetComponent<Rigidbody>().velocity = bulletVelocity;

            NumberOfBullets--;
        }
    }
}
