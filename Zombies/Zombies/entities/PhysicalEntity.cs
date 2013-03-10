using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;

namespace Zombies.entities
{
    [Serializable()]
    class PhysicalEntity : GraphicalEntity
    {
        private float mass = 1;
        private float friction = 0.03f;

        public float Friction
        {
            get { return friction; }
            set { friction = value; }
        }

        public float Mass
        {
            get { return mass; }
            set { mass = value; }
        }


        public PhysicalEntity(Vector2 position)
            : base(position)
        {
            this.CurrentState = new PhysicalEntityState();
            this.DrawLayer = Game1.Instance.Random.Next(1300, 1499);
        }

        public PhysicalEntity(Vector2 position, String texture)
            : base(position)
        {
            this.CurrentState = new PhysicalEntityState();
            this.TexturePath = texture;
            this.DrawLayer = Game1.Instance.Random.Next(1300, 1499);
        }


        public PhysicalEntity()
        {
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);

        }

        public bool Intersects(PhysicalEntity e1)
        {
            if ((e1.position.X + e1.Bounds.X > this.position.X ||
                this.position.X + this.Bounds.X > e1.position.X) &&
                (e1.position.Y + e1.Bounds.Y > this.position.Y ||
                this.position.Y + this.Bounds.Y > e1.position.Y))
                return false;
            return true;
        }

        public static bool Intersects(PhysicalEntity e1, PhysicalEntity e2)
        {
            if ((e1.MovementVector - e2.MovementVector).Length() < 0.001f)
                return false;

            Vector2 n1 = e1.GetNextPosition();
            Vector2 n2 = e2.position;
            Rectangle r1 = new Rectangle((int)n1.X, (int)n1.Y, (int)e1.Bounds.X, (int)e1.Bounds.Y);
            Rectangle r2 = new Rectangle((int)n2.X, (int)n2.Y, (int)e2.Bounds.X, (int)e2.Bounds.Y);

            return r1.Intersects(r2);
        }

        public void Use()
        {
        }

        public override void Move()
        {
            base.Move();
            this.MovementVector *= (1.0f - GetTime() * friction);
        }

        public override Entity Clone()
        {
            Entity c = base.Clone();
            c.CurrentState = new PhysicalEntityState();
            return c;
        }

    }
}
