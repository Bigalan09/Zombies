using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.entities.weapons
{
    class GrenadeThrower : Weapon
    {

        public GrenadeThrower()
        {
            Cooldown = 100;
            Damage = 90;
            ReloadTime = 100;
            ClipSize = 5;
            AllowFire = true;
        }

        public override void Fire()
        {
            base.Fire();

            if (!AllowFire)
                return;
            AllowFire = false;

            Vector2 dir = Owner.FaceVector;
            dir.Normalize();

            Grenade g = new Grenade(Owner.CenterPosition + dir * 60.0f, dir, Owner.FaceVector.Length() / 25.0f, Damage);
            CreateEntity(g);
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
        }
    }
}
