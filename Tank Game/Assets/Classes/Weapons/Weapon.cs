using UnityEngine;
using Assets.Classes.Effects;

namespace Assets.Classes.Weapons
{
    public abstract class Weapon : ScriptableObject
    {
        protected EffectHolder effectHolder = new EffectHolder();

        public void AddInstantEffect(InstantEffect newEffect)
        {
            if (newEffect != null)
                effectHolder.InstantEffects.Add(newEffect);
        }

        public virtual void AddLongEffect(LongEffect newEffect)
        {
            if (newEffect != null)
                effectHolder.LongEffects.Add(newEffect);
        }

        public abstract bool Fire();

        public abstract bool CanShoot();
    }
}
