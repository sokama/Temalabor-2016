using Assets.Classes.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes.Weapons
{
    class ArealWeapon : Weapon
    {
        public float ReloadTime { get; set; }
        public float Intensity { get; set; }
        public float Duration { get; set; }

        public GameObject EffectingAreaPrefab;
        public GameObject Owner;
        public MonoBehaviour DummyMonobehaviour; //e.g. for starting coroutines

        private bool loaded = true;

        public override void AddLongEffect(LongEffect newEffect)
        {
            Debug.LogWarning("Areal weapons can't have long effects!");
        }

        public override bool CanShoot()
        {
            return loaded;
        }

        public override bool Fire()
        {
            if (!CanShoot())
                return false;

            loaded = false;
            GameObject area = (GameObject)Instantiate(EffectingAreaPrefab, Owner.transform.position, Owner.transform.rotation);
            area.GetComponent<EffectingArea>().Init(effectHolder, Owner, Intensity);
            DummyMonobehaviour.StartCoroutine(Reload());
            Destroy(area, Duration);

            return true;
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(ReloadTime);
            loaded = true;
        }
    }
}
