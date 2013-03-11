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
            Damage = 10;
            ReloadTime = 50;
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

            Grenade g = new Grenade(Owner.CenterPosition + dir * 60.0f, dir, Owner.FaceVector.Length() / 25.0f);
            //BlackHole g = new BlackHole(Owner.Position, Owner.FaceVector, 5.0f);
            //Game1.Instance.GameWorld.EntityManager.AddEntity(g);
            CreateEntity(g);
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
        }
    }
}
