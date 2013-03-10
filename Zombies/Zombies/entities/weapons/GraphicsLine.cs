using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.entities.weapons
{
    class GraphicsLine : GraphicalEntity
    {
        private Line line;
        private int lifeTime;
        private bool permanent;

        public GraphicsLine(Vector2 start, Vector2 stop)
        {
            this.Position = start;
            lifeTime = 5;
            permanent = false;
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            this.line = new Line(start, stop);
            this.DrawLayer = 1200;
            this.TexturePath = ("trail");
        }


        public GraphicsLine(Vector2 start, Vector2 stop, bool permanent)
        {
            this.Position = start;
            lifeTime = 10;
            this.permanent = permanent;
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            this.line = new Line(start, stop);
            this.DrawLayer = 1200;
            this.TexturePath = ("trail");
        }


        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            if (permanent)
                return;
            lifeTime -= 1;
            if (lifeTime <= 0)
                this.Alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Vector2 temp = line.Stop - line.Start;
            float len = temp.Length();
            temp.Normalize();

            for (float i = 0; i < len; i += 1.0f)
            {
                spriteBatch.Draw(this.Texture, camera.RelativePosition(line.Start + Vector2.Multiply(temp, i)), Color.White);
            }

            spriteBatch.Draw(this.Texture, camera.RelativePosition(line.Stop), Color.White);
        }

        public override bool isActiveThink()
        {
            return true;
        }

        public override bool isInActiveThink()
        {
            return true;
        }
    }
}
