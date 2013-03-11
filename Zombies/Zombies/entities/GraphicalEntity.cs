using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zombies.states;

namespace Zombies.entities
{
    [Serializable()]
    class GraphicalEntity : Entity, IComparable
    {
        // private Texture2D texture;
        [NonSerialized()]
        private Texture2D texture;
        protected Vector2 position;
        private bool visible = true;
        private float scale = 1.0f;
        private Vector2 bounds;
        private Vector2 faceVector;
        private int drawLayer = 5;
        private Vector2 movementVector = new Vector2(0, 0);
        private string texturePath;
        [NonSerialized()]
        private QuadTree quadTree;
        private float rotation;
        private bool rotateByFace = true;

        public bool RotateByFace
        {
            get { return rotateByFace; }
            set { rotateByFace = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        internal QuadTree QuadTree
        {
            get { return quadTree; }
            set { quadTree = value; }
        }

        private Vector2 previousMovementVector = new Vector2(0, 0);
        public Vector2 PreviousMovementVector
        {
            get { return previousMovementVector; }
            set { previousMovementVector = value; }
        }

        private Vector2 appliedForces = new Vector2(0, 0);

        public Vector2 AppliedForces
        {
            get { return appliedForces; }
            set { appliedForces = value; }
        }


        public Vector2 MovementVector
        {
            get { return movementVector; }
            set { movementVector = value; }
        }

        public int DrawLayer
        {
            get { return drawLayer; }
            set { drawLayer = value; }
        }

        public Vector2 FaceVector
        {
            get { return faceVector; }
            set { faceVector = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }


        public GraphicalEntity(Vector2 position)
            : base()
        {
            Position = position;
        }

        public GraphicalEntity()
            : base()
        {
            this.ActiveThinkDelay = 10 + Game1.Instance.Random.Next(10);
            this.InActiveThinkDelay = 10 + Game1.Instance.Random.Next(50);
            position = Vector2.Zero;
            this.DrawLayer = Game1.Instance.Random.Next(0, 100);
            this.texturePath = null;
            Scale = 1;
        }

        public GraphicalEntity(Vector2 position, string path)
            : base()
        {
            Scale = 1;
            this.Position = position;
            this.DrawLayer = 4;
            this.quadTree = null;
            this.CurrentState = new GraphicalEntityState();
            this.ActiveThinkDelay = 10000 + Game1.Instance.Random.Next(5000);
            this.InActiveThinkDelay = 10000 + Game1.Instance.Random.Next(5000);
            this.texturePath = path;
            this.DrawLayer = Game1.Instance.Random.Next(100, 299);
        }

        public int CompareTo(object obj)
        {
            GraphicalEntity g = (GraphicalEntity)obj;
            return drawLayer.CompareTo(g.DrawLayer);
        }

        public Vector2 Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public Vector2 GetNextPosition()
        {
            return Position + MovementVector * this.GetTime();
        }

        public virtual float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                if (Texture != null)
                {
                    //  Bounds = n Pytaghorassats!
                    bounds.X = this.texture.Width;
                    bounds.Y = this.texture.Height;
                    bounds = bounds * scale;
                }
            }
        }


        public Texture2D Texture
        {
            get
            {
                if (this.texture != null)
                    return texture;
                else
                    this.texture = Game1.Instance.ResourceManager.RequestTexture(this.texturePath);
                return texture;
            }
        }

        public string TexturePath
        {
            get { return this.texturePath; }
            set
            {
                this.texturePath = value;
                this.texture = Game1.Instance.ResourceManager.RequestTexture(value);
                if (value != null)
                {
                    bounds.X = this.texture.Width;
                    bounds.Y = this.texture.Height - 25;
                    bounds = bounds * scale;
                }
                else
                    bounds = new Vector2(0, 0);
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 CenterPosition
        {
            get { return this.position + this.bounds / 2; }
            set { this.Position = value - this.bounds / 2; }
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (Texture == null || !Alive)
                return;
            if (faceVector != null && rotateByFace)
                rotation = ((float)Math.Atan2(faceVector.Y, faceVector.X)) + (float)Math.PI / 2;
            GraphicalEntity ent = this;
            spriteBatch.Draw(Texture,
                             camera.RelativePosition(CenterPosition),
                             null,
                             Color.White,
                             rotation,
                             (Bounds / Scale) / 2,
                             Scale * camera.zoom,
                             SpriteEffects.None,
                             0);
        }

        public void UpdateTreePosition()
        {
            if (!Alive)
            {
                quadTree.Remove(this);
            }
            else if (!quadTree.Inside(this))
            {
                quadTree.Remove(this);
                quadTree.InsertFromTop(this);
            }
        }

        public override bool isActiveThink()
        {
            return false;
        }

        public override bool isInActiveThink()
        {
            return false;
        }

        public virtual void Move()
        {
            Position = this.GetNextPosition();
        }

        public static bool Intersects(GraphicalEntity e1, Rectangle r2)
        {
            Vector2 n1 = e1.Position;
            Rectangle r1 = new Rectangle((int)n1.X, (int)n1.Y, (int)e1.Bounds.X, (int)e1.Bounds.Y);
            return r1.Intersects(r2);
        }



        public static bool RectangularIntersects(GraphicalEntity e1, GraphicalEntity e2)
        {
            Vector2 n1 = e1.Position;
            Vector2 n2 = e2.position;
            Rectangle r1 = new Rectangle((int)n1.X, (int)n1.Y, (int)e1.Bounds.X, (int)e1.Bounds.Y);
            Rectangle r2 = new Rectangle((int)n2.X, (int)n2.Y, (int)e2.Bounds.X, (int)e2.Bounds.Y);

            return r1.Intersects(r2);
        }

        public static bool CircularIntersects(GraphicalEntity e1, GraphicalEntity e2)
        {
            if (Vector2.Distance(e1.CenterPosition, e2.CenterPosition) <= (Math.Max(e1.Bounds.X, e1.Bounds.Y) + Math.Max(e2.Bounds.Y, e2.bounds.X)))
                return true;
            return false;
        }

        public bool CircularIntersects(GraphicalEntity e)
        {
            if (Vector2.Distance(this.CenterPosition, e.CenterPosition) <= this.bounds.X + e.Bounds.X)
                return true;
            return false;
        }

        public static bool IntersectsNext(GraphicalEntity e1, GraphicalEntity e2)
        {
            Vector2 n1 = e1.GetNextPosition();
            Vector2 n2 = e2.Position;
            Rectangle r1 = new Rectangle((int)n1.X, (int)n1.Y, (int)e1.Bounds.X, (int)e1.Bounds.Y);
            Rectangle r2 = new Rectangle((int)n2.X, (int)n2.Y, (int)e2.Bounds.X, (int)e2.Bounds.Y);

            return r1.Intersects(r2);
        }

        public static bool IntersectsNextNext(GraphicalEntity e1, GraphicalEntity e2)
        {
            Vector2 n1 = e1.GetNextPosition();
            Vector2 n2 = e2.GetNextPosition();
            Rectangle r1 = new Rectangle((int)n1.X, (int)n1.Y, (int)e1.Bounds.X, (int)e1.Bounds.Y);
            Rectangle r2 = new Rectangle((int)n2.X, (int)n2.Y, (int)e2.Bounds.X, (int)e2.Bounds.Y);

            return r1.Intersects(r2);
        }

        public virtual GraphicalEntity New()
        {
            return new GraphicalEntity();
        }

        public static bool PositionIntersect(GraphicalEntity e, Vector2 pos)
        {
            if (e.position.X < pos.X && e.position.Y < pos.Y)
            {
                if (e.bounds.X + e.position.X > pos.X && e.bounds.Y + e.position.Y > pos.Y)
                    return true;
            }
            return false;
        }
    }
}
