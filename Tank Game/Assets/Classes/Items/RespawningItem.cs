using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes.Items
{
    class RespawningItem : Item
    {
        public float RespawnTime { get; set; }

        protected override void PickUp(GameObject target)
        {
            effectHolder.ActivateInstantEffects(target);
            effectHolder.PassLongEffectsToTarget(target);

            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.transform.FindChild("Graphics").gameObject.SetActive(false);
            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(RespawnTime);
            gameObject.transform.FindChild("Graphics").gameObject.SetActive(true);
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
