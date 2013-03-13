using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.entities.items
{
    class HealthPack : StaticEntity, Usable
    {
        private int delay = 200;
        private int count = 1;

        private int amount = 10;

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public HealthPack(Vector2 pos)
        {
            Position = pos;
            delay = Game1.Instance.Random.Next(100, 500);
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            TexturePath = ("health");
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            if (count >= delay)
                remove();
            count++;
        }

        private void remove()
        {
            if (Alive)
            {
                this.Alive = false;
            }
        }

        public void Use(Player player)
        {
            if (player.Health == 100)
                Game1.Instance.GameWorld.Score += 10 * amount;
            player.Health += amount;
            player.Health = (player.Health > 100) ? 100 : player.Health;
            remove();
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
