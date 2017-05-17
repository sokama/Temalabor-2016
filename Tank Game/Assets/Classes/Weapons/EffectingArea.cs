using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Classes.Effects;
using System;

public class EffectingArea : MonoBehaviour
{
    private float intensity;
    private float timeUntilNextEffecting = 0f;
    private List<GameObject> targets = new List<GameObject>();
    private GameObject owner;
    private EffectHolder effectHolder;
    private bool initalized = false;


    public void Init(EffectHolder effectHolder, GameObject owner, float intensity)
    {
        this.effectHolder = effectHolder;
        this.owner = owner;
        this.intensity = intensity;

        initalized = true;
    }

    void FixedUpdate()
    {
        if (!initalized)
            return;

        timeUntilNextEffecting -= Time.deltaTime;
        if (timeUntilNextEffecting <= 0)
        {
            EffectTargets();
            timeUntilNextEffecting = intensity;
        }
    }

    private void EffectTargets()
    {
        if (effectHolder == null)
            return;

        foreach (GameObject target in targets)
        {
            effectHolder.ActivateInstantEffects(target);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject != owner && !targets.Contains(col.gameObject))
            targets.Add(col.gameObject);
    }
    void OnTriggerExit(Collider col)
    {
        if (targets.Contains(col.gameObject))
            targets.Remove(col.gameObject);
    }

}
