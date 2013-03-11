using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zombies.managers;
using Zombies.gamestates;

namespace Zombies
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont font;
        private SpriteFont font1;
        public static Game1 Instance = null;
        
        private ResourceManager resourceManager = null;
        private GameStateManager gameStateManager = null;

        internal GameStateManager GameStateManager
        {
            get { return gameStateManager; }
        }
        private InputManager inputManager = null;

        private Random random = new Random();
        private GameState gameState;
        private GameWorld gameWorld;

        internal GameWorld GameWorld
        {
            get { return gameWorld; }
            set { gameWorld = value; }
        }

        internal ResourceManager ResourceManager
        {
            get { return resourceManager; }
        }

        internal InputManager InputManager
        {
            get { return inputManager; }
        }

        public Random Random
        {
            get { return random; }
            set { random = value; }
        }

        internal GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }

        public Game1()
        {
            Content.RootDirectory = "Content";
            Instance = this;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferMultiSampling = true;
            graphics.IsFullScreen = true;
            graphics.PreparingDeviceSettings += OnPreparingDeviceSettings;
        }

        private void OnPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }

        protected override void Initialize()
        {
            resourceManager = new ResourceManager();
            gameStateManager = new GameStateManager();
            inputManager = new InputManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("arial");
            font1 = Content.Load<SpriteFont>("arial1");
            GameWorld = new GameWorld();
            gameStateManager.Push(GameWorld);
        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (inputManager.KeyDown(Keys.Escape))
                this.Exit();

            gameStateManager.Think(gameTime);
            inputManager.SetLastState();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            gameStateManager.Draw();

            if (GameState is GameWorld)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Health: " + GameWorld.Player.Health, new Vector2(25, 25), Color.White);
                spriteBatch.DrawString(font, "Score: " + GameWorld.Score, new Vector2(25, 50), Color.White);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font1, "GAME OVER!", new Vector2((graphics.PreferredBackBufferWidth / 2) - 150, (graphics.PreferredBackBufferHeight / 2) - 50), Color.White);
                spriteBatch.DrawString(font, "Score: " + GameWorld.Score, new Vector2((graphics.PreferredBackBufferWidth / 2) - 50, (graphics.PreferredBackBufferHeight / 2) + 50), Color.White);
                spriteBatch.End();
            }
        }
    }
}
