using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states.zombie
{
    class ZombieState : BeingState
    {
        public Zombie Zombie
        {
            get { return (Zombie)base.Owner; }
            set { base.Owner = value; }
        }

        public override void Idle()
        {
            base.Idle();
            Zombie.CurrentState = new ZombieIdleState();
        }

        public override void Dying()
        {
            base.Dying();
        }

        public override void Turn(Vector2 direction)
        {
            Being.FaceVector.Normalize();
            direction.Normalize();

            if (direction.Length() != 0)
                Being.FaceVector = Vector2.SmoothStep(Being.FaceVector, direction, 0.2f);
        }

        protected override bool LocalCollisionCriterias(PhysicalEntity physicalEntity)
        {
            if (physicalEntity is Player || physicalEntity is StaticEntity)
                return base.LocalCollisionCriterias(physicalEntity);
            return false;
        }
    }
}
