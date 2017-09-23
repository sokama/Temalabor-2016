using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes.Weapons
{
    class ReloadableShootingWeapon : ShootingWeapon
    {
        public float defaultReloadTime = 3f;
        public float ReloadTime { get; set; }

        public MonoBehaviour DummyMonoBehaviourForStartingCoroutines;

        protected override void Awake()
        {
            base.Awake();
            ResetReloadTime();
        }

        private IEnumerator ReloadABullet(float reloadTime)
        {
            yield return new WaitForSeconds(reloadTime);
            NumberOfBullets++;
        }

        public override bool Fire()
        {
            bool success = base.Fire();

            if (success)
                DummyMonoBehaviourForStartingCoroutines.StartCoroutine(ReloadABullet(ReloadTime));

            return success;
        }

        public void ResetReloadTime()
        {
            ReloadTime = defaultReloadTime;
        }
    }
}
