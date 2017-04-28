using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Classes.Effects;
using System;

public class LongEffectHandler : MonoBehaviour {
    private List<LongEffect> effects = new List<LongEffect>();

    void Update()
    {
        List<LongEffect> expiredEffects = new List<LongEffect>();

        foreach (LongEffect effect in effects)
        {
            effect.duration -= Time.deltaTime;
            if (effect.duration <= 0)
                expiredEffects.Add(effect);
        }

        KillExpiredEffects(expiredEffects);
    }

    private void KillExpiredEffects(List<LongEffect> expiredEffects)
    {
        foreach (LongEffect effect in expiredEffects)
        {
            effect.StopEffect(gameObject);
            effects.Remove(effect);
        }
    }

    //Clone the effect before pass it as a parameter, because it will be modified!
    public void AddLongEffect(LongEffect newEffect)
    {
        if(newEffect != null)
        {
            newEffect.StartEffect(gameObject);
            effects.Add(newEffect);
        }
    }


}
