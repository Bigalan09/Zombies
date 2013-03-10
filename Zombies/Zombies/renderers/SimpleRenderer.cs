using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.renderers
{
    class SimpleRenderer : Renderer
    {
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphics;

        public SimpleRenderer()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            graphics = Game1.Instance.Graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphics);
        }

        public override void Draw(Camera camera, List<GraphicalEntity> entitiesToDraw)
        {
            entitiesToDraw.Sort();

            spriteBatch.Begin();

            foreach (GraphicalEntity g in entitiesToDraw)
                g.Draw(spriteBatch, camera);

            spriteBatch.End();
        }
    }
}
