using UnityEngine;
using System.Collections;
using Assets.Classes.Effects;
using System.Collections.Generic;

namespace Assets.Classes.Weapons
{
    public abstract class Weapon : ScriptableObject
    {
        //protected List<InstantEffect> instantEffects = new List<InstantEffect>();
        //protected List<LongEffect> longEffects = new List<LongEffect>();

        protected EffectHolder effectHolder = new EffectHolder();

        public void AddInstantEffect(InstantEffect newEffect)
        {
            if (newEffect != null)
                effectHolder.InstantEffects.Add(newEffect);
        }

        public void AddLongEffect(LongEffect newEffect)
        {
            if (newEffect != null)
                effectHolder.LongEffects.Add(newEffect);
        }

        //protected void ActivateInstantEffects(GameObject target)
        //{
        //    foreach (InstantEffect ie in instantEffects)
        //        ie.Affect(target);
        //}

        //protected void PassLongEffectsToTarget(GameObject target)
        //{
        //    LongEffectHandler effectHandler = target.GetComponent<LongEffectHandler>();
        //    if (effectHandler != null)
        //    {
        //        foreach (LongEffect effect in longEffects)
        //            effectHandler.AddLongEffect(effect.Clone());
        //    }
        //}

        public abstract bool Fire();

        public abstract bool CanShoot();
    }
}
