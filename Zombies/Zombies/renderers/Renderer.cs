using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.renderers
{
    class Renderer
    {
        public Renderer()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void Clear(Color color)
        {
        }

        public virtual void Draw(Camera camera, List<GraphicalEntity> entitiesToDraw)
        {
        }
    }
}
