using UnityEngine;
using System.Collections;
using Assets.Classes.Weapons;

namespace Assets.Classes.Weapons
{
    public class WeaponHolder : MonoBehaviour
    {
        private Weapon primaryWeapon;
        private Weapon secondaryWeapon;

        public void SetPrimaryWeapon(Weapon newWeapon)
        {
            if (newWeapon != null)
                primaryWeapon = newWeapon;
        }

        public void SetSecondaryWeapon(Weapon newWeapon)
        {
            if (newWeapon != null)
                secondaryWeapon = newWeapon;
        }

        public void FirePrimaryWeapon()
        {
            primaryWeapon.Fire();
        }

        public void FireSecondaryWeapon()
        {
            secondaryWeapon.Fire();
        }
    }
}
