using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.particleEffects
{
    class Particle : GraphicalEntity
    {
        private float deAcceleration;
        private float frameLock;

        public float FrameLock
        {
            get { return frameLock; }
            set { frameLock = value; }
        }

        public float DeAcceleration
        {
            get { return deAcceleration; }
            set { deAcceleration = value; }
        }

        public Particle(Vector2 position, Vector2 velocity)
        {
            this.Position = position;
            this.MovementVector = velocity;
            this.DrawLayer = Game1.Instance.Random.Next(500, 999);
        }

        public override void Move()
        {
            this.Position += this.MovementVector * GetTime();
        }

        //public bool intersect(Particle p)
        // {
        // if(p.po
        //  }

        public Particle()
        {

        }


    }
}
