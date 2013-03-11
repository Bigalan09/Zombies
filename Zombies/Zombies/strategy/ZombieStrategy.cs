﻿using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;
using Zombies.states.zombie;

namespace Zombies.strategy
{
    class ZombieStrategy : Strategy
    {
        private Vector2 target;
        public static Random random = new Random(DateTime.Now.Millisecond);
        private float offset;
        private ArrayList players = new ArrayList();

        public ZombieStrategy()
        {
            offset = (0.5f - (float)random.NextDouble()) * 100;
        }


        public override void Act(GameTime gameTime)
        {
            Vector2 temp = new Vector2();
            if (players == null || players.Count == 0)
                Owner.FetchAll(typeof(Player), players);

            float minDistance = 3000.0f;

            target = Vector2.Zero;
            foreach (Player p in players)
            {
                Vector2 vector = p.Position - ((Zombie)Owner).Position;

                if (vector.Length() < minDistance)
                {
                    temp.X = -vector.Y;
                    temp.Y = vector.X;
                    temp.Normalize();
                    temp = Vector2.Multiply(temp, offset);
                    minDistance = vector.Length();
                    if (vector.Length() < 35)
                        target = p.CenterPosition - ((Zombie)Owner).CenterPosition;
                    else
                        target = p.CenterPosition - ((Zombie)Owner).CenterPosition + temp + p.MovementVector * 5;
                }
            }

            if (target == Vector2.Zero)
            {
                ((ZombieState)Owner.CurrentState).Idle();
                return;
            }

            // Vector2 t = ((Zombie)Owner).Target.Position + ((Zombie)Owner).Target.Bounds/2 - (((Zombie)Owner).Position + ((Zombie)Owner).Bounds/2);

            if (target.Length() <= 45.0f)
                ((ZombieState)Owner.CurrentState).Attack();
            else
            {
                ((ZombieState)Owner.CurrentState).Walk(target);
            }

            ((ZombieState)Owner.CurrentState).Turn(target);
        }
    }
}
