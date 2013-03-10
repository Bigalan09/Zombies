using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states.player
{
    class PlayerWalkFireState : PlayerState
    {
        private Vector2 legSpeed;

        public override void EnteringState()
        {
            if (((Player)Owner).PrimaryWeapon != null)
                Player.TexturePath = ("player_fire");

            legSpeed = new Vector2();
        }

        public override void Act(GameTime gameTime)
        {
            base.Act(gameTime);

        }
        public override void Walk(Vector2 direction)
        {
            direction.Normalize();

            if (Player.MovementVector.Length() <= Player.Speed / 2 + 0.02f)
            {
                Player.MovementVector += direction * 1.0f;
                if (Player.MovementVector.Length() > Player.Speed / 2)
                    Player.MovementVector = Player.Speed / 2 * Vector2.Normalize(Player.MovementVector);
                legSpeed = Player.MovementVector;
            }
            else
            {
                legSpeed = Vector2.Normalize(Player.MovementVector);
                legSpeed *= Player.Speed / 2;
            }
        }

        public override void Attack()
        {
            if (((Player)Owner).PrimaryWeapon != null)
                ((Player)Owner).PrimaryWeapon.Fire(Player.FaceVector);
        }

        public override void StopFire()
        {
            Owner.CurrentState = new PlayerWalkState();
        }

        public override void StopWalk()
        {
            Player.MovementVector -= legSpeed;
            Owner.CurrentState = new PlayerFireState();
        }

    }
}
