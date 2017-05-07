using System;
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
        private List<float> reloadTimes = new List<float>();

        protected override void Awake()
        {
            base.Awake();
            Debug.Log("RSW meghivva");
            ResetReloadTime();
        }

        public void ResetReloadTime()
        {
            ReloadTime = defaultReloadTime;
        }
    }
}
