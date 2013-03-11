using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.states.zombie
{
    class ZombieIdleState : ZombieState
    {
        public override void EnteringState()
        {
        }

        public override void Walk(Vector2 direction)
        {
            Zombie.CurrentState = new ZombieWalkState();
        }

        public override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            Zombie.MovementVector = Vector2.Zero;
        }
    }
}
