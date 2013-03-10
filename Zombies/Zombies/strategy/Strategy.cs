using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.strategy
{
    [Serializable()]
    abstract class Strategy
    {
        private Entity owner;

        public Entity Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public virtual void Initialize()
        {
        }

        public virtual Strategy Clone()
        {
            return (Strategy)this.MemberwiseClone();
        }

        public abstract void Act(GameTime gameTime);
    }
}
