﻿using UnityEngine;
using System.Collections;
using Assets.Classes.Weapons;
using Assets.Classes.Effects;

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
        //Player weapon init
        //ArealWeapon playerPrimaryWeapon = (ArealWeapon)ScriptableObject.CreateInstance("ArealWeapon");
        //playerPrimaryWeapon.ReloadTime = 5f;
        //playerPrimaryWeapon.Intensity = 0.1f;
        //playerPrimaryWeapon.Duration = 10f;
        //playerPrimaryWeapon.EffectingAreaPrefab = EffectingAreaPrefab;
        //playerPrimaryWeapon.Owner = Player;
        //playerPrimaryWeapon.DummyMonobehaviour = this;
        //playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -2f });

        LaserWeapon playerPrimaryWeapon = (LaserWeapon)ScriptableObject.CreateInstance("LaserWeapon");
        playerPrimaryWeapon.CooldownTime = 10f;
        playerPrimaryWeapon.OverheatTime = 5f;
        playerPrimaryWeapon.Intensity = 0.1f;
        playerPrimaryWeapon.MaxLaserBeamLength = 30f;
        playerPrimaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        playerPrimaryWeapon.LaserBeam = Instantiate(LaserBeamPrefab).GetComponent<LineRenderer>();
        playerPrimaryWeapon.LaserBeamParticlesPrefab = LaserBeamParticlesPrefab;
        playerPrimaryWeapon.DummyMonoBehaviour = this;
        playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -2f });
        //playerPrimaryWeapon.AddInstantEffect(new DestroyEffect());

        //ReloadableShootingWeapon playerPrimaryWeapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");
        //playerPrimaryWeapon.BulletPrefab = BulletPrefab;
        //playerPrimaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        //playerPrimaryWeapon.defaultBulletSpeed = 10f;
        //playerPrimaryWeapon.ResetBulletSpeed();
        //playerPrimaryWeapon.defaultNumberOfBullets = 3;
        //playerPrimaryWeapon.ReloadAllBullets();
        //playerPrimaryWeapon.defaultReloadTime = 3f;
        //playerPrimaryWeapon.ResetReloadTime();
        //playerPrimaryWeapon.DummyMonoBehaviourForStartingCoroutines = this;
        //playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });

        ShootingWeapon playerSecondaryWeapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");
        playerSecondaryWeapon.BulletPrefab = BulletPrefab;
        playerSecondaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        playerSecondaryWeapon.defaultBulletSpeed = 10f;
        playerSecondaryWeapon.ResetBulletSpeed();
        playerSecondaryWeapon.defaultNumberOfBullets = 5;
        playerSecondaryWeapon.ReloadAllBullets();
        playerSecondaryWeapon.AddInstantEffect(new DestroyEffect());

        Player.GetComponent<WeaponHolder>().SetPrimaryWeapon(playerPrimaryWeapon);
        Player.GetComponent<WeaponHolder>().SetSecondaryWeapon(playerSecondaryWeapon);

        //Enemy weapon init
        //ShootingWeapon enemyPrimaryWeapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");
        //enemyPrimaryWeapon.BulletPrefab = BulletPrefab;
        //enemyPrimaryWeapon.ShootingPoint = Enemy.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        //enemyPrimaryWeapon.defaultBulletSpeed = 10f;
        //enemyPrimaryWeapon.ResetBulletSpeed();
        //enemyPrimaryWeapon.defaultNumberOfBullets = 1;
        //enemyPrimaryWeapon.ReloadAllBullets();
        //enemyPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -99f });
        //enemyPrimaryWeapon.AddLongEffect(new MovementEffect() { movementSpeedMultiplier = 1, rotationSpeedMultiplier = 1, duration = 5 });
        //enemyPrimaryWeapon.AddLongEffect(new DamageModifierEffect()
        //{
        //    damageMultiplier = 0,
        //    duration = 10,
        //    shieldGoodPrefab = ShieldGoodPrefab,
        //    shieldBadPrefab = ShieldBadPrefab
        //});
        ReloadableShootingWeapon enemyPrimaryWeapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");
        enemyPrimaryWeapon.BulletPrefab = BulletPrefab;
        enemyPrimaryWeapon.ShootingPoint = Enemy.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        enemyPrimaryWeapon.defaultBulletSpeed = 10f;
        enemyPrimaryWeapon.ResetBulletSpeed();
        enemyPrimaryWeapon.defaultNumberOfBullets = 3;
        enemyPrimaryWeapon.ReloadAllBullets();
        enemyPrimaryWeapon.defaultReloadTime = 3f;
        enemyPrimaryWeapon.ResetReloadTime();
        enemyPrimaryWeapon.DummyMonoBehaviourForStartingCoroutines = this;
        enemyPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });

        ReloadableShootingWeapon enemySecondaryWeapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");
        enemySecondaryWeapon.BulletPrefab = BulletPrefab;
        enemySecondaryWeapon.ShootingPoint = Enemy.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        enemySecondaryWeapon.defaultBulletSpeed = 10f;
        enemySecondaryWeapon.ResetBulletSpeed();
        enemySecondaryWeapon.defaultNumberOfBullets = 1;
        enemySecondaryWeapon.ReloadAllBullets();
        enemySecondaryWeapon.defaultReloadTime = 15f;
        enemySecondaryWeapon.ResetReloadTime();
        enemySecondaryWeapon.DummyMonoBehaviourForStartingCoroutines = this;
        enemySecondaryWeapon.AddInstantEffect(new DestroyEffect());
        //enemySecondaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });
        //enemySecondaryWeapon.AddLongEffect(new HealthLongEffect()
        //{
        //    healthModifier = -10f,
        //    intensity = 0.5f,
        //    duration = 20f,
        //    dummyMonoBehaviourForStartingCoroutines = this
        //});

        Enemy.GetComponent<WeaponHolder>().SetPrimaryWeapon(enemyPrimaryWeapon);
        Enemy.GetComponent<WeaponHolder>().SetSecondaryWeapon(enemySecondaryWeapon);
    }
}
