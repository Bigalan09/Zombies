using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.entities
{
    class Cursor : GraphicalEntity
    {
        private PhysicalEntity owner;

        internal PhysicalEntity Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public Cursor(PhysicalEntity owner)
            : base()
        {
            this.Owner = owner;
            TexturePath = ("cursor");
            DrawLayer = 1000;

            InActiveThinkDelay = 0;
            ActiveThinkDelay = 0;
        }

        public Cursor()
            : base()
        {
            TexturePath = ("cursor");
            DrawLayer = 1000;

            InActiveThinkDelay = 0;
            ActiveThinkDelay = 0;
        }

        public override bool isActiveThink()
        {
            return base.isActiveThink();
        }

        public override bool isInActiveThink()
        {
            return base.isInActiveThink();
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);

            //Vector2 p = Position;
            //p.X = Mouse.GetState().X + c.Position.X;
            //p.Y = Mouse.GetState().Y + c.Position.Y;
            //Position = p;
        }
    }
}
