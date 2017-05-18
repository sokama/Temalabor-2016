using UnityEngine;
using System.Collections;
using Assets.Classes.Items;
using Assets.Classes.Effects;
using System;
using System.IO;
using System.Collections.Generic;

public class InitializeItems : MonoBehaviour
{

    public GameObject ItemPrefab;
    public GameObject RespawningItemPrefab;

    public GameObject ShieldGoodPrefab;
    public GameObject ShieldBadPrefab;

    void Awake()
    {
        SetItemsFromFile();
        //GameObject item1 = (GameObject)Instantiate(RespawningItemPrefab, new Vector3(-21f, 0f, 21f), Quaternion.identity);
        //GameObject item2 = (GameObject)Instantiate(ItemPrefab, new Vector3(21f, 0f, 21f), Quaternion.identity);
        //GameObject item3 = (GameObject)Instantiate(ItemPrefab, new Vector3(-21f, 0f, -21f), Quaternion.identity);
        //GameObject item4 = (GameObject)Instantiate(RespawningItemPrefab, new Vector3(21f, 0f, -21f), Quaternion.identity);

        //item1.GetComponent<RespawningItem>().AddLongEffect(new HealthLongEffect()
        //{
        //    healthModifier = 1f,
        //    intensity = 0.1f,
        //    duration = 5f,
        //    dummyMonoBehaviourForStartingCoroutines = this
        //});
        //item1.GetComponent<RespawningItem>().RespawnTime = 30f;
        //item2.GetComponent<Item>().AddLongEffect(new HealthLongEffect()
        //{
        //    healthModifier = 1f,
        //    intensity = 0.1f,
        //    duration = 5f,
        //    dummyMonoBehaviourForStartingCoroutines = this
        //});
        //item3.GetComponent<Item>().AddLongEffect(new HealthLongEffect()
        //{
        //    healthModifier = 1f,
        //    intensity = 0.1f,
        //    duration = 5f,
        //    dummyMonoBehaviourForStartingCoroutines = this
        //});
        //item4.GetComponent<RespawningItem>().AddLongEffect(new HealthLongEffect()
        //{
        //    healthModifier = 1f,
        //    intensity = 0.1f,
        //    duration = 5f,
        //    dummyMonoBehaviourForStartingCoroutines = this
        //});
        //item4.GetComponent<RespawningItem>().RespawnTime = 30f;
    }

    private void SetItemsFromFile()
    {
        string path = Application.dataPath + @"/items.csv";
        using (var fs = File.OpenRead(path))
        using (var reader = new StreamReader(fs))
        {
            int numberOfItems = int.Parse(reader.ReadLine().Split(';')[0]);
            for(int i = 0; i < numberOfItems; i++)
            {
                //read file
                List<string> itemParamsText = new List<string>(reader.ReadLine().Split(';'));
                List<string> itemInstantEffectsText = new List<string>(reader.ReadLine().Split(';'));
                List<string> itemLongEffectsText = new List<string>(reader.ReadLine().Split(';'));

                //Create item
                CreateItemFromText(itemParamsText, itemInstantEffectsText, itemLongEffectsText);
            }
        }
    }

    private void CreateItemFromText(List<string> paramsText, List<string> instantEffectsText, List<string> longEffectsText)
    {
        GameObject item;

        List<InstantEffect> instantEffects = CreateInstantEffectsFromText(instantEffectsText);
        List<LongEffect> longEffects = CreateLongEffectsFromText(longEffectsText);

        switch (paramsText[0])
        {
            case "Item":
                item = (GameObject)Instantiate(ItemPrefab, new Vector3(float.Parse(paramsText[1]), 0f, float.Parse(paramsText[2])), Quaternion.identity);

                foreach (InstantEffect effect in instantEffects)
                    item.GetComponent<Item>().AddInstantEffect(effect);
                foreach (LongEffect effect in longEffects)
                    item.GetComponent<Item>().AddLongEffect(effect);

                break;
            case "RespawningItem":
                item = (GameObject)Instantiate(RespawningItemPrefab, new Vector3(float.Parse(paramsText[1]), 0f, float.Parse(paramsText[2])), Quaternion.identity);

                item.GetComponent<RespawningItem>().RespawnTime = float.Parse(paramsText[3]);

                foreach (InstantEffect effect in instantEffects)
                    item.GetComponent<RespawningItem>().AddInstantEffect(effect);
                foreach (LongEffect effect in longEffects)
                    item.GetComponent<RespawningItem>().AddLongEffect(effect);

                break;
            default:
                Debug.LogError("Unknown item type: " + paramsText[0]);
                break;
        }
    }

    private List<LongEffect> CreateLongEffectsFromText(List<string> text)
    {
        List<LongEffect> effects = new List<LongEffect>();

        int i = -1;
        while (i < text.Count - 1)
        {
            switch (text[++i])
            {
                case "DamageModifierEffect":
                    DamageModifierEffect dmEffect = new DamageModifierEffect()
                    {
                        damageMultiplier = float.Parse(text[++i]),
                        duration = float.Parse(text[++i]),
                        shieldBadPrefab = ShieldBadPrefab,
                        shieldGoodPrefab = ShieldGoodPrefab
                    };
                    effects.Add(dmEffect);
                    break;
                case "HealthLongEffect":
                    HealthLongEffect hlEffect = new HealthLongEffect()
                    {
                        healthModifier = float.Parse(text[++i]),
                        intensity = float.Parse(text[++i]),
                        duration = float.Parse(text[++i]),
                        dummyMonoBehaviourForStartingCoroutines = this
                    };
                    effects.Add(hlEffect);
                    break;
                case "MovementEffect":
                    MovementEffect mEffect = new MovementEffect()
                    {
                        movementSpeedMultiplier = float.Parse(text[++i]),
                        rotationSpeedMultiplier = float.Parse(text[++i]),
                        duration = float.Parse(text[++i])
                    };
                    effects.Add(mEffect);
                    break;
                case "":
                    break;
                default:
                    Debug.LogError("Unknown long effect type: " + text[i]);
                    break;
            }
        }

        return effects;
    }

    private List<InstantEffect> CreateInstantEffectsFromText(List<string> text)
    {
        List<InstantEffect> effects = new List<InstantEffect>();

        int i = -1;
        while (i < text.Count - 1)
        {
            switch (text[++i])
            {
                case "HealthInstantEffect":
                    HealthInstantEffect healthInstantEffect = new HealthInstantEffect()
                    {
                        healthModifier = float.Parse(text[++i])
                    };
                    effects.Add(healthInstantEffect);
                    break;
                case "DestroyEffect":
                    DestroyEffect destroyEffect = new DestroyEffect();
                    effects.Add(destroyEffect);
                    break;
                case "":
                    break;
                default:
                    Debug.LogError("Unknown instant effect type: " + text[i]);
                    break;
            }
        }

        return effects;
    }
}
