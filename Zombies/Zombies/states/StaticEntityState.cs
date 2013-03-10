using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states
{
    class StaticEntityState : PhysicalEntityState
    {
        public override void GetPushed(Microsoft.Xna.Framework.Vector2 force)
        {
        }

        public override void GetPushed(PhysicalEntity entity)
        {

        }

        protected override bool GlobalCollisionCriterias()
        {
            return false;
        }
    }
}
