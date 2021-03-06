﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;

namespace Zombies.entities
{
    class BackgroundTile : GraphicalEntity
    {
        public BackgroundTile(int x, int y)
        {
            TexturePath = ("background");
            Position = new Vector2(Texture.Width * x, Texture.Height * y);
            this.DrawLayer = Game1.Instance.Random.Next(40, 60);
            this.ActiveThinkDelay = 100;
            this.InActiveThinkDelay = 100;
            this.CurrentState = new StaticEntityState();
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);
        }

    }
}
