using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states.player
{
    class PlayerFireState : PlayerState
    {
        public override void EnteringState()
        {
            if (((Player)Owner).PrimaryWeapon != null)
                Player.TexturePath = "player_fire";
        }

        public override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            //Player.MovementVector = Vector2.Zero;
        }

        public override void Walk(Vector2 direction)
        {
            Owner.CurrentState = new PlayerWalkFireState();
        }

        public override void Attack()
        {
            if (((Player)Owner).PrimaryWeapon != null)
                ((Player)Owner).PrimaryWeapon.Fire(Player.FaceVector);
        }

        public override void StopFire()
        {
            Idle();
        }


    }
}
