using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;

namespace Zombies.entities
{
    [Serializable()]
    class StaticEntity : PhysicalEntity
    {
        public StaticEntity(Vector2 position)
            : base(position)
        {
            CurrentState = new StaticEntityState();
            this.DrawLayer = Game1.Instance.Random.Next(1300, 1499);
            this.MovementVector = new Vector2(0, 0);
        }


        public StaticEntity()
            : base()
        {
            CurrentState = new StaticEntityState();
            this.DrawLayer = Game1.Instance.Random.Next(1300, 1499);
            this.MovementVector = new Vector2(0, 0);
        }

        public override GraphicalEntity New()
        {
            return new StaticEntity();
        }

        public StaticEntity(Vector2 position, String texture)
            : base(position)
        {
            this.CurrentState = new StaticEntityState();
            this.TexturePath = (texture);
            this.DrawLayer = Game1.Instance.Random.Next(1300, 1499);
            this.MovementVector = new Vector2(0, 0);
        }

        public override bool isActiveThink()
        {
            return false;
        }

        public override bool isInActiveThink()
        {
            return false;
        }
    }
}
