using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;

namespace Zombies.entities.weapons
{
    class Pistol : Weapon
    {
        private float knockEffect;
        private int numberOfBullets;

        public Pistol(PhysicalEntity owner)
        {
            Owner = owner;
            Cooldown = 8;
            Damage = 22;
            knockEffect = 25;
            ReloadTime = 8;
            ClipSize = 10;
            numberOfBullets = 1;
            AllowFire = true;
        }

        public override void Fire(Vector2 direction)
        {
            base.Fire(direction);

            if (!AllowFire)
                return;

            AllowFire = false;

            Vector2 temp;
            for (int k = -numberOfBullets / 2; k <= numberOfBullets / 2; k++)
            {
                temp = Vector2.Multiply(Vector2.Normalize(direction + k * new Vector2(direction.Y, -direction.X)), direction.Length()) + Owner.Position + Owner.Bounds / 2;
                Line traceLine = new Line(Owner.Position + Owner.Bounds / 2, temp);

                ArrayList targetList = new ArrayList();
                EntitiesInLine(traceLine, targetList);

                for (int i = 0; i < targetList.Count; i++)
                {
                    if (this.Owner != targetList[i])
                    {
                        if (targetList[i] is PhysicalEntity)
                        {
                            if (targetList[i] is Being)
                            {
                                ((BeingState)((Being)targetList[i]).CurrentState).GetHit(direction, this);
                            }

                            ((PhysicalEntityState)((PhysicalEntity)targetList[i]).CurrentState).GetPushed(Vector2.Normalize(direction) * knockEffect);
                        }
                    }
                }
                CreateEntity(new GraphicsLine(Owner.Position + Owner.Bounds / 2, temp));

            }
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
        }
    }
}
