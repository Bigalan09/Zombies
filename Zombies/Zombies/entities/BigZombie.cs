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
    class BigZombie : Zombie
    {
        public BigZombie(Vector2 position)
            : base(position)
        {
            Init();
        }

        public BigZombie()
            : base()
        {
            Init();
        }

        public override GraphicalEntity New()
        {
            BigZombie z = new BigZombie();
            if (speed >= 4f) speed = 4f;
            z.Speed = 2.5f;
            z.CurrentStrategy = new ZombieStrategy();
            z.CurrentState = new ChaseState();
            return z;
        }

        private void Init()
        {
            Health = 750.0f;
            TexturePath = ("zombie");
            if (speed >= 3f) speed = 3f;
            Speed = 2.5f;
            Mass = 15.0f;
            ActiveThinkDelay = 20;
            InActiveThinkDelay = 100;
            DrawLayer = Game1.Instance.Random.Next(1000, 1200);
            Friction = 0.2f;
            Scale = 1.75f;
            this.CurrentState = new ChaseState();
            this.CurrentStrategy = new ZombieStrategy();
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
            if (Game1.Instance.Random.NextDouble() < 0.5)
            {
                HealthPack hp = new HealthPack(Position);
                CreateEntity(hp);
            }
            int score = (int)((speed * 200) + (-Health * 20)) * 10;
            score = (score + 50) / 100 * 500;
            Game1.Instance.GameWorld.Score += score;
        }
    }
}
