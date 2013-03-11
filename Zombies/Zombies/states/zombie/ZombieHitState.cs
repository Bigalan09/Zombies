﻿using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies.states.zombie
{
    class ZombieHitState : ZombieState
    {
        private float hitTime;

        public override void EnteringState()
        {
            hitTime = 20.0f;
            Zombie.MovementVector = Vector2.Zero;
        }

        public override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            hitTime -= 1.0f;

            if (hitTime <= 0)
            {
                Vector2 temp = new Vector2(Owner.FaceVector.X, Owner.FaceVector.Y);
                temp.Normalize();
                ArrayList targetList = new ArrayList();
                Owner.EntitiesInRadius(10, Owner.CenterPosition + temp * 10, targetList);

                for (int i = 0; i < targetList.Count; i++)
                {
                    if (targetList[i] is Player)
                    {
                        ((Being)targetList[i]).Health -= 2;
                    }
                }

                Zombie.CurrentState = new ZombieWalkState();
            }
        }

        public override void Walk(Vector2 direction)
        {
            //Zombie.CurrentState = new ZombieWalkState();
        }
    }
}
