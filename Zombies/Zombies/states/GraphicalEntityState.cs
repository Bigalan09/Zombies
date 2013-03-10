using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states
{
    class GraphicalEntityState : EntityState
    {

        public override void Act(GameTime gameTime)
        {

        }

        public GraphicalEntity Owner
        {
            get { return (GraphicalEntity)base.Owner; }
            set { base.Owner = value; }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {

        }
    }
}
