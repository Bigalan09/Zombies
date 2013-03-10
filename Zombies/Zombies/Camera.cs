﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zombies.entities;

namespace Zombies
{
    class Camera
    {
        SpriteBatch spriteBatch;
        Vector2 position;
        ArrayList owners = new ArrayList();
        public Matrix transform;
        public float zoom;

        private Vector2 goalPosition;
        private Vector2 offset;

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public Camera()
        {
            position = new Vector2();
            goalPosition = new Vector2();
            transform = Matrix.CreateScale(new Vector3(Game1.Instance.GraphicsDevice.Viewport.Width / Game1.Instance.Graphics.PreferredBackBufferWidth, Game1.Instance.GraphicsDevice.Viewport.Height / Game1.Instance.Graphics.PreferredBackBufferHeight, 0)) *
                Matrix.CreateTranslation(new Vector3
                    (0,
                    0,
                    0));
            zoom = 1.0f;

        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 GoalPosition
        {
            get { return goalPosition; }
            set { goalPosition = value; }
        }

        public ArrayList Owners
        {
            get { return owners; }
            set { owners = value; }
        }

        public Camera(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

        }

        public Vector2 RelativePosition(Vector2 position)
        {
            return (position - this.position) * zoom;
        }

        public void RandomOffset(float val)
        {
            Offset = new Vector2((0.5f - (float)Game1.Instance.Random.NextDouble()) * val,
                                 (0.5f - (float)Game1.Instance.Random.NextDouble()) * val);
        }

        public void Update()
        {
            foreach (Object id in owners)
            {
                GraphicalEntity g = (GraphicalEntity)Game1.Instance.GameWorld.EntityManager.GetEntity(id);
                if (g != null)
                    position = g.Position - new Vector2(1920 / 2, 1200 / 2) / zoom;
            }
            /*float x = 0;
            float y = 0;
            int count = 0;

            foreach (Object id in owners)
            {
                GraphicalEntity g = (GraphicalEntity)AwesomeZombie.Instance.GameWorld.EntityManager.GetEntity(id);
                if (g != null && g.Alive)
                {
                    x += g.Position.X;
                    y += g.Position.Y;
                    if (g is PhysicalEntity)
                    {
                        x += ((PhysicalEntity)g).MovementVector.X * 50*1/zoom;
                        y += ((PhysicalEntity)g).MovementVector.Y * 50*1/zoom;
                    }
                    count++;
                }
            }

            if (count != 0)
            {
                this.goalPosition = new Vector2(x / ((float)count), y / ((float)count)) - new Vector2(AwesomeZombie.Instance.GraphicsDevice.Viewport.Width / 2, AwesomeZombie.Instance.GraphicsDevice.Viewport.Height / 2)/zoom;

                Vector2 temp = goalPosition * zoom + offset - position;
                float speed = temp.Length();
                offset *= 0.8f;
                if (speed > 1)
                {
                    temp.Normalize();
                    temp *= speed / 20.0f;
                    position += temp;
                }
                else
                    position = goalPosition ;*/


        }
    }
}
