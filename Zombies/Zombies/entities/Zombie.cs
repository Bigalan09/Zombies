using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;

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
            this.CurrentState = new BeingState();
        }

        public Zombie()
            : base()
        {
            Health = 100.0f;
            TexturePath = ("player");
            Speed = 5.0f;
            Mass = 2.0f;
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            this.DrawLayer = 1200;
            Friction = 0.3f;
            this.CurrentState = new BeingState();
        }

    }
}
