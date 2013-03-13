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
        private static Vector2 bounds = Vector2.Zero;

        public static Vector2 Bounds
        {
            get { return Game1.bounds; }
            set { Game1.bounds = value; }
        }
        private static Vector2 centerOrigin = Vector2.Zero;

        public static Vector2 CenterOrigin
        {
            get { return Game1.centerOrigin; }
            set { Game1.centerOrigin = value; }
        }
        
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
            //new Vector2(1920 / 2, 1200 / 2)
            Bounds = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            CenterOrigin = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
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
            GameStateManager.Push(new StartState());
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
        public void DrawStringCentered(SpriteFont spriteFont, String text, Single y, Color color)
        {
            Vector2 textBounds = spriteFont.MeasureString(text);
            Single centerX = spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.5f - textBounds.X * 0.5f;

            spriteBatch.DrawString(spriteFont, text, new Vector2(centerX, y), color);
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
            else if (GameState is GameOverState)
            {
                spriteBatch.Begin();
                DrawStringCentered(font1, "GAME OVER!", (graphics.PreferredBackBufferHeight / 2) - 50, Color.White);
                DrawStringCentered(font, "Score: " + GameWorld.Score, (graphics.PreferredBackBufferHeight / 2) + 50, Color.White);
                DrawStringCentered(font, "Press Space to Restart!", (graphics.PreferredBackBufferHeight / 2) + 85, Color.White);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                DrawStringCentered(font1, "Zombiezzz!", (graphics.PreferredBackBufferHeight / 2) - 75, Color.White);
                DrawStringCentered(font, "Controls: W, A, S, D for movement - Left Mouse click for Primary Weapon - Right Mouse click for Secondary Weapon", (graphics.PreferredBackBufferHeight / 2) + 40, Color.White);
                DrawStringCentered(font, "Press Space to Begin!", (graphics.PreferredBackBufferHeight / 2) + 85, Color.White);
                spriteBatch.End();
            }
        }
    }
}
