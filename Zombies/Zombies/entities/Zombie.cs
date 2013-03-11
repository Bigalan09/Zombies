using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;
using Zombies.states.zombie;
using Zombies.strategy;

namespace Zombies.entities
{
    class Zombie : Being
    {
        private PhysicalEntity target;

        public PhysicalEntity Target
        {
            get { return target; }
            set { target = value; }
        }

        public Zombie(Vector2 position)
            : base(position)
        {
            Health = 20.0f;
            TexturePath = ("zombie");
            Speed = 1.0f;
            Mass = 8.0f;
            ActiveThinkDelay = 20;
            InActiveThinkDelay = 200;
            DrawLayer = Game1.Instance.Random.Next(1000, 1200);
            Friction = 0.3f;
            this.CurrentState = new ZombieIdleState();
            this.CurrentStrategy = new ZombieIdleStrategy();
        }

        public Zombie()
            : base()
        {
            Health = 10.0f;
            TexturePath = ("zombie");
            Speed = 1.0f;
            Mass = 8.0f;
            ActiveThinkDelay = 20;
            InActiveThinkDelay = 200;
            DrawLayer = Game1.Instance.Random.Next(1000, 1200);
            Friction = 0.3f;
            this.CurrentState = new ZombieIdleState();
            this.CurrentStrategy = new ZombieIdleStrategy();
        }

        public override GraphicalEntity New()
        {
            Zombie z = new Zombie();
            z.CurrentStrategy = new ZombieIdleStrategy();
            z.CurrentState = new ZombieWalkState();
            return z;
        }

        public override bool isActiveThink()
        {
            return true;
        }

        public override Entity Clone()
        {
            Entity c = base.Clone();
            return c;
        }

        public override float Scale
        {
            get
            {
                return base.Scale;
            }
            set
            {
                base.Scale = value;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
        }
    }
}
