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
        //private List<float> reloadTimes = new List<float>();

        public MonoBehaviour DummyMonoBehaviourForStartingCoroutines;

        protected override void Awake()
        {
            base.Awake();
            Debug.Log("RSW meghivva");

            ResetReloadTime();
        }

        //void Update()
        //{
        //    Debug.Log("RSW Update");
        //    UpdateReloadTimes(Time.deltaTime);
        //}

        //private void UpdateReloadTimes(float deltaTime)
        //{
        //    //search for expired reload times
        //    List<int> expiredReloadTimeIndexes = new List<int>();
        //    for (int i = 0; i < reloadTimes.Count; i++)
        //    {
        //        reloadTimes[i] -= deltaTime;
        //        if(reloadTimes[i] <= 0)
        //            expiredReloadTimeIndexes.Add(i);
        //    }

        //    //remove expired reloadtimes and add new bullets
        //    foreach (int index in expiredReloadTimeIndexes)
        //    {
        //        reloadTimes.RemoveAt(index);
        //        NumberOfBullets++;

        //        Debug.Log(NumberOfBullets + " bullets");
        //    }
        //}

        private IEnumerator ReloadABullet(float reloadTime)
        {
            yield return new WaitForSeconds(reloadTime);
            NumberOfBullets++;
        }

        public override bool Fire()
        {
            bool success = base.Fire();
            //reloadTimes.Add(ReloadTime);

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
