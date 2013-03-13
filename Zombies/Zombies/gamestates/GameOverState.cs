using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;
using Zombies.entities.weapons;
using Zombies.managers;
using Zombies.renderers;
using Zombies.states.player;
using Zombies.strategy;

namespace Zombies.gamestates
{
    class GameOverState : GameState
    {
        private SimpleRenderer menuRenderer = new SimpleRenderer();
        private Camera camera;

        private GraphicalEntity cursor;
        private Background background = null;

        public GameOverState()
        {
        }

        private void MoveCursor()
        {
            this.cursor.Position = Game1.Instance.InputManager.MousePosition();
        }

        public override void Initilize()
        {
            base.Initilize();
            camera = new Camera();
            cursor = new Cursor();
            background = new Background();
            background.Initialize();
        }


        public override void Think(GameTime gameTime)
        {
            base.Think(gameTime);
            if (Game1.Instance.InputManager.KeyDown(Keys.Space))
            {
                Game1.Instance.GameWorld = new GameWorld();
                Game1.Instance.GameStateManager.Push(Game1.Instance.GameWorld);
            }
        }

        public override void Draw()
        {
            List<GraphicalEntity> toDraw = new List<GraphicalEntity>();
            toDraw.Add(background);

            Game1.Instance.GraphicsDevice.Clear(Color.Pink);
            menuRenderer.Draw(camera, toDraw);
        }
    }
}
