using UnityEngine;
using System.Collections;
using Assets.Classes.Effects;
using System.Collections.Generic;

namespace Assets.Classes.Weapons
{
    public class Bullet : MonoBehaviour
    {
        public GameObject ExplosionParticles;

        private EffectHolder effectHolder = new EffectHolder();

        public void SetEffectHolder(EffectHolder newEffectHolder)
        {
            if (newEffectHolder != null)
                effectHolder = newEffectHolder;
        }

        void OnTriggerEnter(Collider col)
        {
            effectHolder.ActivateInstantEffects(col.gameObject);
            effectHolder.PassLongEffectsToTarget(col.gameObject);

            Explode();
        }

        private void Explode()
        {
            Quaternion explosionRotation = new Quaternion();
            explosionRotation.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
            GameObject explosion = (GameObject)Instantiate(ExplosionParticles, transform.position, explosionRotation);
            Destroy(explosion, 0.5f);

            Destroy(gameObject);
            return;
        }
    }
}
