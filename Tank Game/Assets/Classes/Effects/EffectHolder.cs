using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes.Effects
{
    public class EffectHolder
    {
        public List<InstantEffect> InstantEffects = new List<InstantEffect>();
        public List<LongEffect> LongEffects = new List<LongEffect>();

        public void ActivateInstantEffects(GameObject target)
        {
            foreach (InstantEffect effect in InstantEffects)
                effect.Affect(target);
        }

        public void PassLongEffectsToTarget(GameObject target)
        {
            LongEffectHandler effectHandler = target.GetComponent<LongEffectHandler>();
            if (effectHandler != null)
            {
                foreach (LongEffect effect in LongEffects)
                    effectHandler.AddLongEffect(effect.Clone());
            }
        }
    }
}
