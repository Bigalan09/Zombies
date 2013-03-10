using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.states.player
{
    class PlayerWalkState : PlayerState
    {
        private Vector2 direction;
        private Vector2 legSpeed;

        public override void EnteringState()
        {
            Player.TexturePath = ("player");
            legSpeed = new Vector2();
        }

        public override void Walk(Vector2 direction)
        {
            this.direction = direction;
            direction.Normalize();

            if (Player.MovementVector.Length() <= Player.Speed + 0.02f)
            {
                Player.MovementVector += direction * 1.0f;
                if (Player.MovementVector.Length() > Player.Speed)
                    Player.MovementVector = Player.Speed * Vector2.Normalize(Player.MovementVector);
                legSpeed = Player.MovementVector;
            }
            else
            {
                legSpeed = Vector2.Normalize(Player.MovementVector);
                legSpeed *= Player.Speed;
            }
        }

        public override void Attack()
        {
            Owner.CurrentState = new PlayerWalkFireState();
        }

        public override void StopWalk()
        {
            Player.MovementVector -= legSpeed;
            Idle();
        }

    }
}
