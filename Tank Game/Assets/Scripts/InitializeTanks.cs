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
        ShootingWeapon playerPrimaryWeapon = new ShootingWeapon()
        {
            BulletPrefab = this.BulletPrefab,
            ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint"),
            defaultBulletSpeed = 10f,
            defaultNumberOfBullets = 3
        };
        playerPrimaryWeapon.AddInstantEffect(new HealthInstantEffect() { healthModifier = 10f });

        ShootingWeapon playerSecondaryWeapon = new ShootingWeapon()
        {
            BulletPrefab = this.BulletPrefab,
            ShootingPoint = Player.transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint"),
            defaultBulletSpeed = 10f,
            defaultNumberOfBullets = 3
        };
        playerSecondaryWeapon.AddInstantEffect(new DestroyEffect());

        Player.GetComponent<WeaponHolder>().SetPrimaryWeapon(playerPrimaryWeapon);
        Player.GetComponent<WeaponHolder>().SetSecondaryWeapon(playerSecondaryWeapon);
    }
}
