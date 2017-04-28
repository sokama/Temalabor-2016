using UnityEngine;
using System.Collections;

namespace Assets.Classes.Effects
{
    public abstract class LongEffect : MonoBehaviour
    {
        public float duration;

        public abstract void StartEffect(GameObject target);
        public abstract void StopEffect(GameObject target);
        public abstract LongEffect Clone();     
    }
}
