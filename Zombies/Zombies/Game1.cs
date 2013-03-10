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

        public static Game1 Instance = null;
        
        private ResourceManager resourceManager = null;
        private GameStateManager gameStateManager = null;
        private InputManager inputManager = null;

        private Random random = new Random();
        private GameState gameState;

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

        internal GameWorld GameWorld
        {
            get { return (GameWorld)gameState; }
            set { gameState = value; }
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
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 10;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 10;
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
        }
    }
}
