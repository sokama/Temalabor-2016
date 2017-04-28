using UnityEngine;
using System.Collections;
using Assets.Classes.Effects;
using System.Collections.Generic;

namespace Assets.Classes.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        private List<InstantEffect> instantEffects = new List<InstantEffect>();
        private List<LongEffect> longEffects = new List<LongEffect>();

        public void AddInstantEffect(InstantEffect newEffect)
        {
            if (newEffect != null)
                instantEffects.Add(newEffect);
        }

        public void AddLongEffect(LongEffect newEffect)
        {
            if (newEffect != null)
                longEffects.Add(newEffect);
        }

        protected void ActivateInstantEffects(GameObject target)
        {
            foreach (InstantEffect ie in instantEffects)
                ie.Affect(target);
        }

        protected void PassLongEffectsToTarget(GameObject target)
        {
            LongEffectHandler effectHandler = target.GetComponent<LongEffectHandler>();
            if (effectHandler != null)
            {
                foreach (LongEffect effect in longEffects)
                    effectHandler.AddLongEffect(effect.Clone());
            }
        }

        protected abstract void Hit(); //TODO: kell ez a fuggveny?..

        public abstract void Fire();
    }
}
