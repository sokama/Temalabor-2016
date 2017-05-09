using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes.Effects
{
    class DamageModifierEffect : LongEffect
    {
        public float damageMultiplier = 1f;

        public override void StartEffect(GameObject target)
        {
            if (target.GetComponent<Health>() != null)
                target.GetComponent<Health>().damageIntakeMultiplier *= damageMultiplier;
        }

        public override void StopEffect(GameObject target)
        {
            Health targetHealth = target.GetComponent<Health>();
            if(targetHealth != null)
            {
                if (damageMultiplier != 0)
                    targetHealth.damageIntakeMultiplier /= damageMultiplier;
                else
                    targetHealth.damageIntakeMultiplier = 1;
            }
        }

        public override LongEffect Clone()
        {
            return new DamageModifierEffect()
            {
                damageMultiplier = this.damageMultiplier,
                duration = this.duration
            };
        }
    }
}
