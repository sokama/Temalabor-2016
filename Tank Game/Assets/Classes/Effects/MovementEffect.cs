using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes.Effects
{
    public class MovementEffect : LongEffect
    {
        public float movementSpeedMultiplier = 1;
        public float rotationSpeedMultiplier = 1;


        public override void StartEffect(GameObject target)
        {
            if (target.GetComponent<TankMovementParameters>() != null)
            {
                target.GetComponent<TankMovementParameters>().TankMovementSpeed *= movementSpeedMultiplier;
                target.GetComponent<TankMovementParameters>().TankRotationSpeed *= rotationSpeedMultiplier;
            }
        }

        public override void StopEffect(GameObject target)
        {
            TankMovementParameters targetMovementParams = target.GetComponent<TankMovementParameters>();
            if (targetMovementParams != null)
            {
                if (movementSpeedMultiplier != 0)
                    targetMovementParams.TankMovementSpeed /= movementSpeedMultiplier;
                else
                    targetMovementParams.ResetTankMovementSpeed();

                if (rotationSpeedMultiplier != 0)
                    targetMovementParams.TankRotationSpeed /= rotationSpeedMultiplier;
                else
                    targetMovementParams.ResetTankRotationSpeed();
            }
        }

        public override LongEffect Clone()
        {
            return new MovementEffect() {
                duration = this.duration,
                movementSpeedMultiplier = this.movementSpeedMultiplier,
                rotationSpeedMultiplier = this.rotationSpeedMultiplier
            };
        }
    }
}
