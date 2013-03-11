using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;

namespace Zombies.entities.weapons
{
    class Grenade : PhysicalEntity, Explosive
    {
        private static int grenadeDelay = 2000;

        private float counter;
        private float rotationSpeed;
        private float damage = 10;

        public Grenade()
        {
            TexturePath = ("grenade");
            counter = 0;
            rotationSpeed = 0.1f;
            FaceVector = new Vector2(1, 0);
        }

        public Grenade(Vector2 position, Vector2 direction, float force, float damage)
            : base(position)
        {
            this.damage = damage;
            FaceVector = new Vector2(1, 0);
            TexturePath = ("grenade");
            counter = 0;
            rotationSpeed = 0.5f;
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            direction.Normalize();
            MovementVector = direction * force;
            CurrentState = new StaticEntityState();
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            rotationSpeed *= 0.98f;
            MovementVector *= 0.97f;
            Vector2 temp = new Vector2();
            temp.X = -FaceVector.Y;
            temp.Y = FaceVector.X;
            FaceVector = Vector2.SmoothStep(FaceVector, temp, rotationSpeed * GetTime());
            counter += 16 * GetTime();

            if (counter >= grenadeDelay)
                BlowUp();

            counter++;
        }

        public void BlowUp()
        {
            if (Alive)
            {
                this.Alive = false;
                Explosion explosion = new Explosion(150.0f, damage, 1.3f, CenterPosition);
                CreateEntity(explosion);
                explosion.Explode();
            }
        }

        public override bool isActiveThink()
        {
            return true;
        }

        public override bool isInActiveThink()
        {
            return true;
        }
    }
}
