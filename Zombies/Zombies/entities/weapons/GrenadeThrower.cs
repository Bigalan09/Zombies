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
            Cooldown = 50;
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

            Grenade g = new Grenade(Owner.CenterPosition + dir * 60.0f, dir, Owner.FaceVector.Length() / 30.0f);
            //BlackHole g = new BlackHole(Owner.Position, Owner.FaceVector, 5.0f);
            CreateEntity(g);
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
        }
    }
}
