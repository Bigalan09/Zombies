using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities.items;
using Zombies.states;
using Zombies.states.zombie;
using Zombies.strategy;

namespace Zombies.entities
{
    class Zombie : Being
    {
        private PhysicalEntity target;
        public static float speed = 0.8f;

        public PhysicalEntity Target
        {
            get { return target; }
            set { target = value; }
        }

        public Zombie(Vector2 position)
            : base(position)
        {
            Health = 50.0f;
            TexturePath = ("zombie");
            if (speed >= 6f) speed = 6f;
            Speed = speed;
            Mass = 8.0f;
            ActiveThinkDelay = 20;
            InActiveThinkDelay = 100;
            DrawLayer = Game1.Instance.Random.Next(1000, 1200);
            Friction = 0.3f;
            this.CurrentState = new ZombieIdleState();
            this.CurrentStrategy = new ZombieStrategy();
        }

        public Zombie()
            : base()
        {
            Health = 50.0f;
            TexturePath = ("zombie");
            if (speed >= 6f) speed = 6f;
            Speed = speed;
            Mass = 8.0f;
            ActiveThinkDelay = 20;
            InActiveThinkDelay = 100;
            DrawLayer = Game1.Instance.Random.Next(1000, 1200);
            Friction = 0.3f;
            this.CurrentState = new ZombieIdleState();
            this.CurrentStrategy = new ZombieStrategy();
        }

        public override GraphicalEntity New()
        {
            Zombie z = new Zombie();
            if (speed >= 6f) speed = 6f;
            z.Speed = speed;
            z.CurrentStrategy = new ZombieStrategy();
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

        public override void OnDeath()
        {
            base.OnDeath();
            if (Game1.Instance.Random.NextDouble() < 0.2)
            {
                HealthPack hp = new HealthPack(Position);
                CreateEntity(hp);
            }
            int score = (int)((speed * 100) + (-Health * 10));
            score = (score + 50) / 100 * 100;
            Game1.Instance.GameWorld.Score += score;
        }
    }
}
