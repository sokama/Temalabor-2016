using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes.Effects
{
    class HealthLongEffect : LongEffect
    {
        public float healthModifier = 0f;
        public float intensity = 1f;

        private bool isRunning = false;

        public MonoBehaviour dummyMonoBehaviourForStartingCoroutines;

        public override LongEffect Clone()
        {
            return new HealthLongEffect()
            {
                duration = this.duration,
                healthModifier = this.healthModifier,
                intensity = this.intensity,
                dummyMonoBehaviourForStartingCoroutines = this.dummyMonoBehaviourForStartingCoroutines,
                isRunning = this.isRunning
            };
        }

        public override void StartEffect(GameObject target)
        {
            isRunning = true;
            dummyMonoBehaviourForStartingCoroutines.StartCoroutine(ModifyHealth(target));
        }

        public override void StopEffect(GameObject target)
        {
            isRunning = false;
        }

        private IEnumerator ModifyHealth(GameObject target)
        {
            //if (target.GetComponent<Health>() != null)
            //    target.GetComponent<Health>().DecreaseHealth(-healthModifier);

            //yield return new WaitForSeconds(intensity);

            //if (isRunning)
            //    dummyMonoBehaviourForStartingCoroutines.StartCoroutine(ModifyHealth(target));

            Health targetHealth = target.GetComponent<Health>();
            while (isRunning)
            {
                if (targetHealth != null)
                    targetHealth.DecreaseHealth(-healthModifier);

                yield return new WaitForSeconds(intensity);
            }
        }
    }
}
