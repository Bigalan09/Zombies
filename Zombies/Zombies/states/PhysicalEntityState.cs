using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states
{
    class PhysicalEntityState : GraphicalEntityState
    {
        public PhysicalEntity PhysicalEntity
        {
            get { return (PhysicalEntity)base.Owner; }
            set { base.Owner = value; }
        }

        public virtual void GetPushed(Vector2 force)
        {
            force = force / ((PhysicalEntity)Owner).Mass;
            ((PhysicalEntity)Owner).MovementVector += force;
        }

        public virtual void GetPushed(PhysicalEntity entity)
        {
            PhysicalEntity.MovementVector = (1f * entity.Mass * (entity.MovementVector - PhysicalEntity.MovementVector) + PhysicalEntity.MovementVector * PhysicalEntity.Mass + entity.MovementVector * entity.Mass) / (PhysicalEntity.Mass + entity.Mass);
        }

        protected virtual bool LocalCollisionCriterias(PhysicalEntity physicalEntity)
        {
            if (this.PhysicalEntity == physicalEntity)
                return false;

            if (!GraphicalEntity.IntersectsNext(this.PhysicalEntity, physicalEntity))
                return false;

            return true;
        }

        protected virtual bool GlobalCollisionCriterias()
        {
            if (this.PhysicalEntity.MovementVector.Length() == 0)
                return false;

            return true;
        }

        private void HandleCollision(PhysicalEntity physicalEntity)
        {
            float tempMy = this.PhysicalEntity.MovementVector.Y;
            this.PhysicalEntity.MovementVector *= new Vector2(1, 0);
            if (PhysicalEntity.Intersects(this.PhysicalEntity, physicalEntity))
                this.PhysicalEntity.MovementVector = new Vector2(0, tempMy);
            if (PhysicalEntity.Intersects(this.PhysicalEntity, physicalEntity))
                this.PhysicalEntity.MovementVector = new Vector2(0, 0);
        }

        public void CollideAndMove(List<PhysicalEntity> list)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (LocalCollisionCriterias(list[j]))
                    HandleCollision(list[j]);
            }

            PhysicalEntity.Move();
        }
    }
}
