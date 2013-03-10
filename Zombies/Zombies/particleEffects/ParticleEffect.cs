using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.particleEffects
{
    class ParticleEffect : GraphicalEntity
    {
        private ArrayList particles;
        private int capacity;

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        public ArrayList Particles
        {
            get { return particles; }
            set { particles = value; }
        }

        public ParticleEffect(Vector2 position)
            : base(position)
        {
            this.DrawLayer = Game1.Instance.Random.Next(500, 999);
            this.ActiveThinkDelay = 0;
            this.Particles = new ArrayList();
        }

        public override void Initialize()
        {
            base.Initialize();
            Particles = new ArrayList(Capacity);
            for (int i = 0; i < Particles.Capacity; i++)
            {
                Particles.Add(new Particle());
            }
        }

        public virtual void FunctionOnParticles()
        {
            RemoveParticles();
        }
        public void RemoveParticles()
        {
            for (int i = Particles.Count - 1; i > 0; i--)
            {
                if (!((Particle)Particles[i]).Alive)
                    Particles.RemoveAt(i);
            }
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            FunctionOnParticles();
        }

        public override bool isActiveThink()
        {
            return true;
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (Particle p in Particles)
            {
                p.Draw(spriteBatch, camera);
            }
        }
    }
}
