using UnityEngine;
using System.Collections;
using Assets.Classes.Effects;
using System.Collections.Generic;

namespace Assets.Classes.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        private List<Effect> effects = new List<Effect>();

        public void AddEffect(Effect newEffect)
        {
            if (newEffect != null)
                effects.Add(newEffect);
        }

        protected void ActivateEffects(GameObject target)
        {
            foreach (Effect e in effects)
                e.Affect(target);
        }

        protected abstract void Hit();

        public abstract void Fire();
    }
}
