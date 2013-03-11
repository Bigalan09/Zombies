using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.gamestates;

namespace Zombies.managers
{
    class GameStateManager
    {
        Stack<GameState> stateStack;

        internal Stack<GameState> StateStack
        {
            get { return stateStack; }
            set { stateStack = value; }
        }

        public GameStateManager()
        {
            stateStack = new Stack<GameState>();
        }

        public void Push(GameState gameState)
        {
            gameState.Initilize();
            gameState.Owner = this;
            stateStack.Push(gameState);
            Game1.Instance.GameState = gameState;
        }

        public GameState Pop()
        {
            if (stateStack.Count > 0)
            {
                return stateStack.Pop();
            }
            return null;
        }

        public void Think(GameTime gameTime)
        {
            if (stateStack.Count > 0)
                stateStack.Peek().Think(gameTime);
        }

        public void Draw()
        {
            if (stateStack.Count > 0)
                stateStack.Peek().Draw();
        }
    }
}
