using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.managers;

namespace Zombies.gamestates
{
    abstract class GameState
    {
        protected bool initialized = false;
        GameStateManager owner;

        internal GameStateManager Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public GameState()
        {

        }

        public virtual void Initilize()
        {

        }

        public virtual void Think(GameTime gameTime)
        {
        }

        public virtual void Draw() { }
    }
}
