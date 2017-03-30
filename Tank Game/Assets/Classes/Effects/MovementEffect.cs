using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes.Effects
{
    public class MovementEffect : Effect
    {
        public float movementSpeedMultiplier = 1;
        public float rotationSpeedMultiplier = 1;
        public float duration = 1;

        public override void Affect(GameObject target)
        {
            //TODO majd ha lesz külön TankMovement script...
        }
    }
}
