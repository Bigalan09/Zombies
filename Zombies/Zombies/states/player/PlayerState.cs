using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states.player
{
    class PlayerState : BeingState
    {
        public PlayerState()
        {

        }

        public override void Act(GameTime gameTime)
        {
            Use();
            base.Act(gameTime);
        }

        public Player Player
        {
            get { return (Player)base.Owner; }
            set { base.Owner = value; }
        }

        public override void Attack()
        {
            Owner.CurrentState = new PlayerFireState();
        }

        public override void SecondaryAttack()
        {
            Player.SecondaryWeapon.Fire();
        }

        public override void Use()
        {
            base.Use();

            ArrayList list = new ArrayList();
            Owner.EntitiesInRadius(25, Player.Position, list);

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Usable)
                    ((Usable)list[i]).Use(Player);
            }
        }

        public override void Walk(Vector2 direction)
        {
            base.Walk(direction);
            Owner.CurrentState = new PlayerWalkState();
        }

        public override void Idle()
        {
            base.Idle();
            Owner.CurrentState = new PlayerIdleState();
        }

        public override void Turn(Vector2 direction)
        {
            base.Turn(direction);

            Player.Aim.CenterPosition = direction + Player.CenterPosition;
        }

        public override void GetPushed(Vector2 force)
        {

        }

        public override void GetPushed(PhysicalEntity entity)
        {

        }
    }
}
