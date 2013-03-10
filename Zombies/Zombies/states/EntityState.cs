using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states
{
    abstract class EntityState
    {
        private Entity owner;

        public Entity Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public abstract void Act(GameTime gameTime);
        public virtual void EnteringState() { }
        public virtual void LeavingState() { }
        public float GetTime() { return 1.0f; }
    }
}
