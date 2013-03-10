using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.states.player
{
    class PlayerIdleState : PlayerState
    {
        public override void EnteringState()
        {
            Player.TexturePath = ("player");
        }

        public override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            if (Player.MovementVector.Length() <= 1.0f)
                Player.MovementVector = Vector2.Zero;
        }
    }
}
