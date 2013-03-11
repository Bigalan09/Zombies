using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;
using Zombies.entities.weapons;
using Zombies.gamestates;
using Zombies.managers;
using Zombies.renderers;
using Zombies.states.player;
using Zombies.strategy;

namespace Zombies.gamestates
{
    class GameWorld : GameState
    {
        private Camera camera;
        private ArrayList cameraEntities = new ArrayList();
        private EntityManager entityManager = new EntityManager();

        private SimpleRenderer hudRenderer = new SimpleRenderer();
        private int score = 0;

        private Camera hudCamera = new Camera();
        private Player player = null;

        internal Player Player
        {
            get { return player; }
        }

        internal int Score
        {
            set { score = value; }
            get { return score; }
        }
        private ZombieSpawner spawner;

        internal EntityManager EntityManager
        {
            get { return entityManager; }
            set { entityManager = value; }
        }

        internal Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public GameWorld()
        {
            spawner = new ZombieSpawner();
        }

        public override void Think(GameTime gameTime)
        {
            base.Think(gameTime);

            //Select area for graphical entities
            EntityManager.SelectRect = new Rectangle((int)(camera.Position.X - 500 - 1920 / camera.zoom), (int)(camera.Position.Y - 1200 / camera.zoom - 500), (int)(500 + 1920 * 3 / camera.zoom), (int)(500 + 1200 * 3 / camera.zoom));
            EntityManager.Think(gameTime);

            EntityManager.CollideAndMove();

            EntityManager.UpdateTreePositions();
            EntityManager.AddEntities();
            EntityManager.RemoveEntities();

            int count = cameraEntities.Count;
            bool first = true;
            foreach (GraphicalEntity ent in cameraEntities)
            {
                if (ent != null && ent.Alive)
                {
                    if (first)
                    {
                        Camera.Position = (ent.CenterPosition - new Vector2(1920 / 2, 1200 / 2));
                        first = false;
                    }
                    else
                    {
                        Camera.Position += (ent.CenterPosition - new Vector2(1920 / 2, 1200 / 2));
                    }
                }
            }
            spawner.Think(gameTime);

            Camera.Position /= count;
        }

        public override void Initilize()
        {
            base.Initilize();
            camera = new Camera();

            Background b = new Background();

            player = new Player(new Vector2(920, 600));
            player.CurrentState = new PlayerIdleState();
            player.CurrentStrategy = new PlayerKeyboardStrategy();
            player.PlayerIndex = PlayerIndex.Two;
            player.SecondaryWeapon = new GrenadeThrower();


            this.EntityManager.AddEntity(b);
            this.EntityManager.AddEntity(player);
            this.EntityManager.AddEntity(spawner);

            cameraEntities.Add(player);
            cameraEntities.Add(player.Aim);
        }

        public override void Draw()
        {
            List<GraphicalEntity> toDraw = new List<GraphicalEntity>();

            toDraw.AddRange(EntityManager.Results);
            EntityManager.QuadTree.Select(new Rectangle((int)camera.Position.X, (int)camera.Position.Y, 1920, 1200), ref toDraw);

            Game1.Instance.GraphicsDevice.Clear(new Color(60, 70, 40));

            hudRenderer.Draw(hudCamera, toDraw);
        }
    }
}
