using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities.weapons;
using Zombies.states;

namespace Zombies.entities
{
    [Serializable()]
    class Being : PhysicalEntity
    {
        public delegate void onHit(Being being, Weapon weapon);
        public event onHit gotHit;

        private float health;
        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Being()
            : base()
        {
            DrawLayer = 5;
        }

        public Being(Vector2 position)
            : base(position)
        {
            DrawLayer = 5;
        }

        public float Health
        {
            get { return health; }
            set { health = (value > 1000) ? 1000 : value; }
        }

        public void GetHit(Weapon weapon)
        {
            if (gotHit != null)
                gotHit(this, weapon);
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);

            if (health <= 0)
            {
                ((BeingState)CurrentState).Dying();
            }
            if (Position.X > Game1.Bounds.X)
            {
                Position = new Vector2(0, Position.Y);
            }
            if (Position.X < 0)
            {
                Position = new Vector2(Game1.Bounds.X, Position.Y);
            }

            if (Position.Y > Game1.Bounds.Y)
            {
                Position = new Vector2(Position.X, 0);
            }
            if (Position.Y < 0)
            {
                Position = new Vector2(Position.X, Game1.Bounds.Y);
            }
        }
    }
}
