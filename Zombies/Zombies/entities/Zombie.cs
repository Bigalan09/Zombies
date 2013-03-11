using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.entities
{
    class Zombie : Being
    {

        public Zombie(Vector2 position)
            : base(position)
        {
            Health = 10.0f;
            TexturePath = ("player");
            Speed = 5.0f;
            Mass = 2.0f;
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            this.DrawLayer = 1200;
            Friction = 0.03f;
        }

        public Zombie()
            : base()
        {
            Health = 10.0f;
            TexturePath = ("player");
            Speed = 5.0f;
            Mass = 2.0f;
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            this.DrawLayer = 1200;
            Friction = 0.03f;
        }

    }
}
