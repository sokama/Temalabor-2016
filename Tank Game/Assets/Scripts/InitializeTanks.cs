using UnityEngine;
using System.Collections;
using Assets.Classes.Weapons;
using Assets.Classes.Effects;
using System.IO;
using System;
using System.Collections.Generic;

public class InitializeTanks : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;

    public GameObject BulletPrefab;
    public GameObject ShieldGoodPrefab;
    public GameObject ShieldBadPrefab;
    public GameObject LaserBeamPrefab;
    public GameObject LaserBeamParticlesPrefab;
    public GameObject EffectingAreaPrefab;

    void Awake()
    {
        SetTankWeaponsFromFile();

        ////Player weapon init
        ////ArealWeapon playerPrimaryWeapon = (ArealWeapon)ScriptableObject.CreateInstance("ArealWeapon");
        ////playerPrimaryWeapon.ReloadTime = 5f;
        ////playerPrimaryWeapon.Intensity = 0.1f;
        ////playerPrimaryWeapon.Duration = 10f;
        ////playerPrimaryWeapon.EffectingAreaPrefab = EffectingAreaPrefab;
        ////playerPrimaryWeapon.Owner = Player;
        ////playerPrimaryWeapon.DummyMonobehaviour = this;
        ////playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -2f });

        //LaserWeapon playerPrimaryWeapon = (LaserWeapon)ScriptableObject.CreateInstance("LaserWeapon");
        //playerPrimaryWeapon.CooldownTime = 10f;
        //playerPrimaryWeapon.OverheatTime = 5f;
        //playerPrimaryWeapon.Intensity = 0.1f;
        //playerPrimaryWeapon.MaxLaserBeamLength = 30f;
        //playerPrimaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        //playerPrimaryWeapon.LaserBeam = Instantiate(LaserBeamPrefab).GetComponent<LineRenderer>();
        //playerPrimaryWeapon.LaserBeamParticlesPrefab = LaserBeamParticlesPrefab;
        //playerPrimaryWeapon.DummyMonoBehaviour = this;
        //playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -2f });
        ////playerPrimaryWeapon.AddInstantEffect(new DestroyEffect());

        ////ReloadableShootingWeapon playerPrimaryWeapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");
        ////playerPrimaryWeapon.BulletPrefab = BulletPrefab;
        ////playerPrimaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        ////playerPrimaryWeapon.defaultBulletSpeed = 10f;
        ////playerPrimaryWeapon.ResetBulletSpeed();
        ////playerPrimaryWeapon.defaultNumberOfBullets = 3;
        ////playerPrimaryWeapon.ReloadAllBullets();
        ////playerPrimaryWeapon.defaultReloadTime = 3f;
        ////playerPrimaryWeapon.ResetReloadTime();
        ////playerPrimaryWeapon.DummyMonoBehaviourForStartingCoroutines = this;
        ////playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });

        //ShootingWeapon playerSecondaryWeapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");
        //playerSecondaryWeapon.BulletPrefab = BulletPrefab;
        //playerSecondaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        //playerSecondaryWeapon.defaultBulletSpeed = 10f;
        //playerSecondaryWeapon.ResetBulletSpeed();
        //playerSecondaryWeapon.defaultNumberOfBullets = 5;
        //playerSecondaryWeapon.ReloadAllBullets();
        //playerSecondaryWeapon.AddInstantEffect(new DestroyEffect());

        //Player.GetComponent<WeaponHolder>().SetPrimaryWeapon(playerPrimaryWeapon);
        //Player.GetComponent<WeaponHolder>().SetSecondaryWeapon(playerSecondaryWeapon);

        ////Enemy weapon init
        ////ShootingWeapon enemyPrimaryWeapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");
        ////enemyPrimaryWeapon.BulletPrefab = BulletPrefab;
        ////enemyPrimaryWeapon.ShootingPoint = Enemy.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        ////enemyPrimaryWeapon.defaultBulletSpeed = 10f;
        ////enemyPrimaryWeapon.ResetBulletSpeed();
        ////enemyPrimaryWeapon.defaultNumberOfBullets = 1;
        ////enemyPrimaryWeapon.ReloadAllBullets();
        ////enemyPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -99f });
        ////enemyPrimaryWeapon.AddLongEffect(new MovementEffect() { movementSpeedMultiplier = 1, rotationSpeedMultiplier = 1, duration = 5 });
        ////enemyPrimaryWeapon.AddLongEffect(new DamageModifierEffect()
        ////{
        ////    damageMultiplier = 0,
        ////    duration = 10,
        ////    shieldGoodPrefab = ShieldGoodPrefab,
        ////    shieldBadPrefab = ShieldBadPrefab
        ////});
        //ReloadableShootingWeapon enemyPrimaryWeapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");
        //enemyPrimaryWeapon.BulletPrefab = BulletPrefab;
        //enemyPrimaryWeapon.ShootingPoint = Enemy.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        //enemyPrimaryWeapon.defaultBulletSpeed = 10f;
        //enemyPrimaryWeapon.ResetBulletSpeed();
        //enemyPrimaryWeapon.defaultNumberOfBullets = 3;
        //enemyPrimaryWeapon.ReloadAllBullets();
        //enemyPrimaryWeapon.defaultReloadTime = 3f;
        //enemyPrimaryWeapon.ResetReloadTime();
        //enemyPrimaryWeapon.DummyMonoBehaviourForStartingCoroutines = this;
        //enemyPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });

        //ReloadableShootingWeapon enemySecondaryWeapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");
        //enemySecondaryWeapon.BulletPrefab = BulletPrefab;
        //enemySecondaryWeapon.ShootingPoint = Enemy.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        //enemySecondaryWeapon.defaultBulletSpeed = 10f;
        //enemySecondaryWeapon.ResetBulletSpeed();
        //enemySecondaryWeapon.defaultNumberOfBullets = 1;
        //enemySecondaryWeapon.ReloadAllBullets();
        //enemySecondaryWeapon.defaultReloadTime = 15f;
        //enemySecondaryWeapon.ResetReloadTime();
        //enemySecondaryWeapon.DummyMonoBehaviourForStartingCoroutines = this;
        //enemySecondaryWeapon.AddInstantEffect(new DestroyEffect());
        ////enemySecondaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });
        ////enemySecondaryWeapon.AddLongEffect(new HealthLongEffect()
        ////{
        ////    healthModifier = -10f,
        ////    intensity = 0.5f,
        ////    duration = 20f,
        ////    dummyMonoBehaviourForStartingCoroutines = this
        ////});

        //Enemy.GetComponent<WeaponHolder>().SetPrimaryWeapon(enemyPrimaryWeapon);
        //Enemy.GetComponent<WeaponHolder>().SetSecondaryWeapon(enemySecondaryWeapon);
    }

    private void SetTankWeaponsFromFile()
    {
        string path = Application.dataPath + @"/weapons.csv";
        using (var fs = File.OpenRead(path))
        using (var reader = new StreamReader(fs))
        {
            //Read file
            List<string> playerPrimaryWeaponParamsText = new List<string>(reader.ReadLine().Split(';'));
            List<string> playerPrimaryWeaponInstantEffectsText = new List<string>(reader.ReadLine().Split(';'));
            List<string> playerPrimaryWeaponLongEffectsText = new List<string>(reader.ReadLine().Split(';'));

            List<string> playerSecondaryWeaponParamsText = new List<string>(reader.ReadLine().Split(';'));
            List<string> playerSecondaryWeaponInstantEffectsText = new List<string>(reader.ReadLine().Split(';'));
            List<string> playerSecondaryWeaponLongEffectsText = new List<string>(reader.ReadLine().Split(';'));

            List<string> enemyPrimaryWeaponParamsText = new List<string>(reader.ReadLine().Split(';'));
            List<string> enemyPrimaryWeaponInstantEffectsText = new List<string>(reader.ReadLine().Split(';'));
            List<string> enemyPrimaryWeaponLongEffectsText = new List<string>(reader.ReadLine().Split(';'));

            List<string> enemySecondaryWeaponParamsText = new List<string>(reader.ReadLine().Split(';'));
            List<string> enemySecondaryWeaponInstantEffectsText = new List<string>(reader.ReadLine().Split(';'));
            List<string> enemySecondaryWeaponLongEffectsText = new List<string>(reader.ReadLine().Split(';'));

            //Create weapons
            Weapon playerPrimaryWeapon = CreateWeaponFromText(Player, playerPrimaryWeaponParamsText, playerPrimaryWeaponInstantEffectsText, playerPrimaryWeaponLongEffectsText);
            Weapon playerSecondaryWeapon = CreateWeaponFromText(Player, playerSecondaryWeaponParamsText, playerSecondaryWeaponInstantEffectsText, playerSecondaryWeaponLongEffectsText);

            Weapon enemyPrimaryWeapon = CreateWeaponFromText(Enemy, enemyPrimaryWeaponParamsText, enemyPrimaryWeaponInstantEffectsText, enemyPrimaryWeaponLongEffectsText);
            Weapon enemySecondaryWeapon = CreateWeaponFromText(Enemy, enemySecondaryWeaponParamsText, enemySecondaryWeaponInstantEffectsText, enemySecondaryWeaponLongEffectsText);

            //Set weapons
            Player.GetComponent<WeaponHolder>().SetPrimaryWeapon(playerPrimaryWeapon);
            Player.GetComponent<WeaponHolder>().SetSecondaryWeapon(playerSecondaryWeapon);

            Enemy.GetComponent<WeaponHolder>().SetPrimaryWeapon(enemyPrimaryWeapon);
            Enemy.GetComponent<WeaponHolder>().SetSecondaryWeapon(enemySecondaryWeapon);
        }
    }

    private Weapon CreateWeaponFromText(GameObject owner, List<string> paramsText, List<string> instantEffectsText, List<string> longEffectsText)
    {
        Weapon weapon = null;
        switch (paramsText[0])
        {
            case "ShootingWeapon":
                weapon = CreateShootingWeaponFromText(owner, paramsText, instantEffectsText, longEffectsText);
                break;
            case "ReloadableShootingWeapon":
                weapon = CreateReloadableShootingWeaponFromText(owner, paramsText, instantEffectsText, longEffectsText);
                break;
            case "LaserWeapon":
                weapon = CreateLaserWeaponFromText(owner, paramsText, instantEffectsText, longEffectsText);
                break;
            case "ArealWeapon":
                weapon = CreateArealWeaponFromText(owner, paramsText, instantEffectsText, longEffectsText);
                break;
            default:
                Debug.LogError("Unknown weapon type: " + paramsText[0]);
                break;
        }
        return weapon;
    }

    private ArealWeapon CreateArealWeaponFromText(GameObject owner, List<string> paramsText, List<string> instantEffectsText, List<string> longEffectsText)
    {
        ArealWeapon weapon = (ArealWeapon)ScriptableObject.CreateInstance("ArealWeapon");

        //Parameters
        weapon.ReloadTime = float.Parse(paramsText[1]);
        weapon.Intensity = float.Parse(paramsText[2]);
        weapon.Duration = float.Parse(paramsText[3]);
        weapon.EffectingAreaPrefab = EffectingAreaPrefab;
        weapon.Owner = owner;
        weapon.DummyMonobehaviour = this;

        //Instant effects
        List<InstantEffect> instantEffects = CreateInstantEffectsFromText(instantEffectsText);
        foreach (InstantEffect effect in instantEffects)
            weapon.AddInstantEffect(effect);

        return weapon;
    }

    private LaserWeapon CreateLaserWeaponFromText(GameObject owner, List<string> paramsText, List<string> instantEffectsText, List<string> longEffectsText)
    {
        LaserWeapon weapon = (LaserWeapon)ScriptableObject.CreateInstance("LaserWeapon");

        //Parameters
        weapon.CooldownTime = float.Parse(paramsText[1]);
        weapon.OverheatTime = float.Parse(paramsText[2]);
        weapon.Intensity = float.Parse(paramsText[3]);
        weapon.MaxLaserBeamLength = float.Parse(paramsText[4]);
        weapon.ShootingPoint = owner.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        weapon.LaserBeam = Instantiate(LaserBeamPrefab).GetComponent<LineRenderer>();
        weapon.LaserBeamParticlesPrefab = LaserBeamParticlesPrefab;
        weapon.DummyMonoBehaviour = this;

        //Instant effects
        List<InstantEffect> instantEffects = CreateInstantEffectsFromText(instantEffectsText);
        foreach (InstantEffect effect in instantEffects)
            weapon.AddInstantEffect(effect);

        return weapon;
    }

    private ReloadableShootingWeapon CreateReloadableShootingWeaponFromText(GameObject owner, List<string> paramsText, List<string> instantEffectsText, List<string> longEffectsText)
    {
        ReloadableShootingWeapon weapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");

        //Parameters
        weapon.BulletPrefab = BulletPrefab;
        weapon.ShootingPoint = owner.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        weapon.defaultBulletSpeed = float.Parse(paramsText[1]);
        weapon.ResetBulletSpeed();
        weapon.defaultNumberOfBullets = int.Parse(paramsText[2]);
        weapon.ReloadAllBullets();
        weapon.defaultReloadTime = float.Parse(paramsText[3]);
        weapon.ResetReloadTime();
        weapon.DummyMonoBehaviourForStartingCoroutines = this;

        //Instant effects
        List<InstantEffect> instantEffects = CreateInstantEffectsFromText(instantEffectsText);
        foreach (InstantEffect effect in instantEffects)
            weapon.AddInstantEffect(effect);

        //Long effects
        List<LongEffect> longEffects = CreateLongEffectsFromText(longEffectsText);
        foreach (LongEffect effect in longEffects)
            weapon.AddLongEffect(effect);

        return weapon;
    }

    private ShootingWeapon CreateShootingWeaponFromText(GameObject owner, List<string> paramsText, List<string> instantEffectsText, List<string> longEffectsText)
    {
        ShootingWeapon weapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");

        //Parameters
        weapon.BulletPrefab = BulletPrefab;
        weapon.ShootingPoint = owner.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        weapon.defaultBulletSpeed = float.Parse(paramsText[1]);
        weapon.ResetBulletSpeed();
        weapon.defaultNumberOfBullets = int.Parse(paramsText[2]);
        weapon.ReloadAllBullets();

        //Instant effects
        List<InstantEffect> instantEffects = CreateInstantEffectsFromText(instantEffectsText);
        foreach (InstantEffect effect in instantEffects)
            weapon.AddInstantEffect(effect);

        //Long effects
        List<LongEffect> longEffects = CreateLongEffectsFromText(longEffectsText);
        foreach (LongEffect effect in longEffects)
            weapon.AddLongEffect(effect);

        return weapon;
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
