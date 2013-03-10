using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.particleEffects
{
    class ParticleExplosion : ParticleEffect
    {
        private Random rand;

        public ParticleExplosion(Vector2 position)
            : base(position)
        {
            rand = new Random(Game1.Instance.Random.Next(5));
            Capacity = 100;

        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (Particle p in Particles)
            {
                p.Alive = true;
                p.MovementVector = rand.Next(40, 60) * (new Vector2(0.5f - (float)rand.NextDouble(), 0.5f - (float)rand.NextDouble()));
                p.Position = this.Position;
                p.Rotation = (float)rand.NextDouble();
                p.Scale = (16.5f - (float)rand.Next(4)) / 10;
                p.TexturePath = ("explosion");
            }
        }

        public override void FunctionOnParticles()
        {
            base.FunctionOnParticles();
            int i = Particles.Count;
            bool stillAlive = false;

            foreach (Particle p in Particles)
            {
                if (p.Alive)
                    stillAlive = p.Alive;

                p.Move();
                p.Scale *= (1.0f - 0.08f * GetTime());

                if (p.Scale < 0.1f)
                    p.Alive = false;
            }

            this.Alive = stillAlive;
        }
    }
}
