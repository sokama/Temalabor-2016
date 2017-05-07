using UnityEngine;
using System.Collections;

namespace Assets.Classes.Effects
{
    public abstract class LongEffect
    {
        public float duration;

        public abstract void StartEffect(GameObject target);
        public abstract void StopEffect(GameObject target);
        public abstract LongEffect Clone();     
    }
}
