using UnityEngine;
using System.Collections;
using Assets.Classes.Effects;

namespace Assets.Classes.Items
{
    public class Item : MonoBehaviour
    {
        public string targetLayer;
        private int targetLayerID;

        protected EffectHolder effectHolder = new EffectHolder();

        void Awake()
        {
            targetLayerID = LayerMask.NameToLayer(targetLayer);
        }

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

        protected virtual void PickUp(GameObject target)
        {
            effectHolder.ActivateInstantEffects(target);
            effectHolder.PassLongEffectsToTarget(target);

            Destroy(gameObject);
            return;
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.layer == targetLayerID)
                PickUp(col.gameObject);
        }
    }
}