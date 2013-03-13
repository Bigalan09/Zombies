using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.entities
{
    class ZombieSpawner : Entity
    {
        private static int quantity;
        public ZombieSpawner()
        {
            ActiveThinkDelay = Game1.Instance.Random.Next(500, 2000);
            InActiveThinkDelay = Game1.Instance.Random.Next(500, 2000);
            quantity = 2;
        }

        public override void Initialize()
        {
            base.Initialize();
            Spawn();
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            ActiveThinkDelay = Game1.Instance.Random.Next(500, 2000);
            InActiveThinkDelay = Game1.Instance.Random.Next(500, 2000);

            ArrayList zombies = new ArrayList();
            Game1.Instance.GameWorld.EntityManager.FetchAll(typeof(Zombie), zombies);
            if (zombies.Count >= 100)
                return;

            Spawn();
            Zombie.speed += 0.01f;
        }

        private void Spawn()
        {
            for (int i = 0; i < quantity; i++)
            {
                
                double angle = Game1.Instance.Random.NextDouble() * Math.PI * 2;
                double radius = Math.Sqrt(Game1.Instance.Random.NextDouble() + 0.25) * 600;
                float x = (float)(Game1.Instance.GameWorld.Player.Position.X - (radius * Math.Cos(angle)));
                float y = (float)(Game1.Instance.GameWorld.Player.Position.Y - (radius * Math.Sin(angle)));
                Zombie zombie = new Zombie(new Vector2(x, y));
                if (Game1.Instance.Random.NextDouble() < 0.011)
                {
                    zombie = new BigZombie(new Vector2(x, y));
                }
                CreateEntity(zombie);
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
