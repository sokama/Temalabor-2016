using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes.Effects
{
    public class HealthInstantEffect : InstantEffect
    {
        public float healthModifier = 0;

        public override void Affect(GameObject target)
        {
            if (target.GetComponent<Health>() != null)
            {
                target.GetComponent<Health>().DecreaseHealth(-healthModifier);
            }
        }
    }
}
