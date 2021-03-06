﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.gamestates;
using Zombies.states;

namespace Zombies.entities
{
    class Background : GraphicalEntity
    {
        private ArrayList tiles = new ArrayList();
        public Background()
        {
            this.ActiveThinkDelay = 100;
            this.InActiveThinkDelay = 100;
            this.CurrentState = new StaticEntityState();
        }

        public override void Initialize()
        {
            base.Initialize();
            int rows = 6;
            int cols = 11;
            
            BackgroundTile tile;

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    tile = new BackgroundTile(x, y);
                    Game1.Instance.GameWorld.EntityManager.AddEntity(tile);
                    tiles.Add(tile);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);
            //if (Game1.Instance.GameState is GameOverState)
            //{
                foreach (BackgroundTile tile in tiles)
                {
                    tile.Draw(spriteBatch, camera);
                }
           // }
        }
    }
}
