using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;

namespace Zombies.entities
{
    class Background : GraphicalEntity
    {
        public Background()
        {
            int rows = 10;
            int cols = 10;
            this.DrawLayer = 900;
            this.ActiveThinkDelay = 100;
            this.InActiveThinkDelay = 100;
            this.CurrentState = new StaticEntityState();

            BackgroundTile tile;

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    tile = new BackgroundTile(x, y);
                    tile.Alive = true;
                    CreateEntity(tile);
                }
            }
        }

    }
}
