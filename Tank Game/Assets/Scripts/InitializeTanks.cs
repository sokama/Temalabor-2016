using UnityEngine;
using System.Collections;
using Assets.Classes.Weapons;
using Assets.Classes.Effects;

public class InitializeTanks : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;

    public GameObject BulletPrefab;

    void Awake()
    {
        //Player weapon init
        ReloadableShootingWeapon playerPrimaryWeapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");
        playerPrimaryWeapon.BulletPrefab = BulletPrefab;
        playerPrimaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        playerPrimaryWeapon.defaultBulletSpeed = 10f;
        playerPrimaryWeapon.ResetBulletSpeed();
        playerPrimaryWeapon.defaultNumberOfBullets = 3;
        playerPrimaryWeapon.ReloadAllBullets();
        playerPrimaryWeapon.defaultReloadTime = 3f;
        playerPrimaryWeapon.ResetReloadTime();
        playerPrimaryWeapon.DummyMonoBehaviourForStartingCoroutines = this;
        playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });

        ShootingWeapon playerSecondaryWeapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");
        playerSecondaryWeapon.BulletPrefab = BulletPrefab;
        playerSecondaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        playerSecondaryWeapon.defaultBulletSpeed = 10f;
        playerSecondaryWeapon.ResetBulletSpeed();
        playerSecondaryWeapon.defaultNumberOfBullets = 3;
        playerSecondaryWeapon.ReloadAllBullets();
        playerSecondaryWeapon.AddInstantEffect(new DestroyEffect());

        Player.GetComponent<WeaponHolder>().SetPrimaryWeapon(playerPrimaryWeapon);
        Player.GetComponent<WeaponHolder>().SetSecondaryWeapon(playerSecondaryWeapon);

        //Enemy weapon init
        ShootingWeapon enemyPrimaryWeapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");
        enemyPrimaryWeapon.BulletPrefab = BulletPrefab;
        enemyPrimaryWeapon.ShootingPoint = Enemy.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        enemyPrimaryWeapon.defaultBulletSpeed = 10f;
        enemyPrimaryWeapon.ResetBulletSpeed();
        enemyPrimaryWeapon.defaultNumberOfBullets = 1;
        enemyPrimaryWeapon.ReloadAllBullets();
        enemyPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });
        enemyPrimaryWeapon.AddLongEffect(new MovementEffect() { movementSpeedMultiplier = 0, rotationSpeedMultiplier = 0, duration = 5 });
        enemyPrimaryWeapon.AddLongEffect(new DamageModifierEffect() { damageMultiplier = 5, duration = 10 });

        ReloadableShootingWeapon enemySecondaryWeapon = (ReloadableShootingWeapon)ScriptableObject.CreateInstance("ReloadableShootingWeapon");
        enemySecondaryWeapon.BulletPrefab = BulletPrefab;
        enemySecondaryWeapon.ShootingPoint = Enemy.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        enemySecondaryWeapon.defaultBulletSpeed = 10f;
        enemySecondaryWeapon.ResetBulletSpeed();
        enemySecondaryWeapon.defaultNumberOfBullets = 1;
        enemySecondaryWeapon.ReloadAllBullets();
        enemySecondaryWeapon.defaultReloadTime = 10f;
        enemySecondaryWeapon.ResetReloadTime();
        enemySecondaryWeapon.DummyMonoBehaviourForStartingCoroutines = this;
        //enemySecondaryWeapon.AddInstantEffect(new DestroyEffect());
        enemySecondaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });

        Enemy.GetComponent<WeaponHolder>().SetPrimaryWeapon(enemyPrimaryWeapon);
        Enemy.GetComponent<WeaponHolder>().SetSecondaryWeapon(enemySecondaryWeapon);
    }
}
