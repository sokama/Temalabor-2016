using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes.Weapons
{
    public class ShootingWeapon : Weapon
    {
        public float bulletSpeed = 10f;
        public int numberOfBullets = 3;

        public override void Fire()
        {
            throw new NotImplementedException();
        }

        protected override void Hit()
        {
            throw new NotImplementedException();
        }
    }
}
