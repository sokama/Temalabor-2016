﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes.Effects
{
    public class HealthEffect : Effect
    {
        public int healthModifier = 0;

        public override void Affect(GameObject target)
        {
            if (target.GetComponent<Health>() != null)
            {
                target.GetComponent<Health>().DecreaseHealth(-healthModifier);
            }
        }
    }
}
