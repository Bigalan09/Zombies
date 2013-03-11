using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.entities.items
{
    class HealthPack : StaticEntity, Usable
    {

        public HealthPack(Vector2 pos)
        {
            Position = pos;
            ActiveThinkDelay = 1000;
            TexturePath = ("health");
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            Alive = false;
        }

        public void Use(Player player)
        {
            player.Health += 10;
            Alive = false;
        }
    }
}
