using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states.zombie
{
    class ChaseState : ZombieState
    {
        public override void Act(GameTime gameTime)
        {
            base.Act(gameTime);

            // Chase closest
            ArrayList players = new ArrayList();
            Owner.FetchAll(typeof(Player), players);

            float minDistance = float.MaxValue;

            foreach (Player p in players)
            {
                float currentDistance = (p.Position - Zombie.Position).Length();

                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    Zombie.Target = p;
                }
            }

            if (Zombie.Target == null)
                return;

            Vector2 t = Zombie.Target.Position - Zombie.Position;
            t.Normalize();

            t = Vector2.Multiply(t, 2);
            Zombie.FaceVector = t;
            Zombie.MovementVector = t;
        }
    }
}
