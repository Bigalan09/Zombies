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
        private static int max_zombies = 50;
        private static int zombies_spawned = 0;
        private static bool continuous = false;

        public ZombieSpawner()
        {
            ActiveThinkDelay = Game1.Instance.Random.Next(4000, 5000);
            InActiveThinkDelay = Game1.Instance.Random.Next(4000, 5000);
            quantity = 1;
        }

        public override void Initialize()
        {
            base.Initialize();
            Spawn();
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            ActiveThinkDelay = Game1.Instance.Random.Next(10, 100);
            InActiveThinkDelay = Game1.Instance.Random.Next(10, 100);

            ArrayList zombies = new ArrayList();
            Game1.Instance.GameWorld.EntityManager.FetchAll(typeof(Zombie), zombies);

            if (zombies.Count >= max_zombies)
            {
                continuous = true;
                return;
            }
            if (continuous)
            {
                ActiveThinkDelay = Game1.Instance.Random.Next(200, 500);
                InActiveThinkDelay = Game1.Instance.Random.Next(200, 500);
                quantity = 1;
                Spawn();
                return;
            }
            if (zombies.Count == 0 || zombies_spawned >= quantity)
            {
                ActiveThinkDelay = Game1.Instance.Random.Next(4000, 5000);
                InActiveThinkDelay = Game1.Instance.Random.Next(4000, 5000);
                quantity += quantity;
                Zombie.speed += 0.1f;
                Spawn();
                zombies_spawned = 0;
            }
            if (zombies.Count < quantity)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < 1; i++)
            {
                
                double angle = Game1.Instance.Random.NextDouble() * Math.PI * 2;
                double radius = Math.Sqrt(Game1.Instance.Random.NextDouble() + 0.25) * 600;
                float x = (float)(Game1.Instance.GameWorld.Player.Position.X - (radius * Math.Cos(angle)));
                float y = (float)(Game1.Instance.GameWorld.Player.Position.Y - (radius * Math.Sin(angle)));
                Zombie zombie = new Zombie(new Vector2(x, y));
                if (Game1.Instance.Random.NextDouble() < 0.005)
                {
                    zombie = new BigZombie(new Vector2(x, y));
                }
                CreateEntity(zombie);
                zombies_spawned++;
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
