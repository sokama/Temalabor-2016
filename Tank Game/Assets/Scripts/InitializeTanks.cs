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
        ShootingWeapon playerPrimaryWeapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");
        playerPrimaryWeapon.BulletPrefab = BulletPrefab;
        playerPrimaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        playerPrimaryWeapon.defaultBulletSpeed = 10f;
        playerPrimaryWeapon.defaultNumberOfBullets = 3;
        playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = -10f });

        ShootingWeapon playerSecondaryWeapon = (ShootingWeapon)ScriptableObject.CreateInstance("ShootingWeapon");
        playerSecondaryWeapon.BulletPrefab = BulletPrefab;
        playerSecondaryWeapon.ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        playerSecondaryWeapon.defaultBulletSpeed = 10f;
        playerSecondaryWeapon.defaultNumberOfBullets = 3;
        playerSecondaryWeapon.AddInstantEffect(new DestroyEffect());

        Player.GetComponent<WeaponHolder>().SetPrimaryWeapon(playerPrimaryWeapon);
        Player.GetComponent<WeaponHolder>().SetSecondaryWeapon(playerSecondaryWeapon);
    }
}
