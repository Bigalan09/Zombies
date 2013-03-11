using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.states.zombie
{
    class ZombieWalkState : ZombieState
    {
        private Vector2 direction;

        public override void EnteringState()
        {
        }

        public override void Walk(Vector2 direction)
        {
            this.direction = direction;
            direction.Normalize();

            if (Zombie.MovementVector.Length() <= Zombie.Speed + 0.02f)
            {
                Zombie.MovementVector = direction * Zombie.Speed;
            }
        }

        public override void Attack()
        {
            Owner.CurrentState = new ZombieHitState();
        }
    }
}
