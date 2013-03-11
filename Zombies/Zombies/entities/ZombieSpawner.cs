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
        private int quantity;

        public ZombieSpawner()
        {
            ActiveThinkDelay = 500;
            quantity = 10;

        }

        public override void Initialize()
        {
            base.Initialize();
            for (int i = 0; i < quantity; i++)
            {
                Zombie zombie = new Zombie(new Vector2(Game1.Instance.Random.Next(50, 1000), Game1.Instance.Random.Next(50, 1000)));
                zombie.Move();
                CreateEntity(zombie);
            }
        }

        protected override void Act(GameTime gameTime)
        {
            ArrayList count = new ArrayList();
            Game1.Instance.GameWorld.EntityManager.FetchAll(typeof(Zombie), count);
            int currentCount = count.Count;
            if (currentCount > 15)
                return;

            for (int i = 0; i < quantity; i++)
            {
                Zombie zombie = new Zombie(new Vector2(Game1.Instance.Random.Next(50, 1000), Game1.Instance.Random.Next(50, 1000)));
                zombie.Move();
                CreateEntity(zombie);
            }

        }

        public override bool isActiveThink()
        {
            return true;
        }

        public override bool isInActiveThink()
        {
            return false;
        }
    }
}
