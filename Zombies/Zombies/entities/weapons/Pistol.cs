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
            Cooldown = 20;
            Damage = 4;
            knockEffect = 100;
            ReloadTime = 5;
            ClipSize = 5;
            numberOfBullets = 1;
            AllowFire = true;
        }

        public override void Fire(Vector2 direction)
        {
            base.Fire(direction);

            if (!AllowFire)
                return;

            AllowFire = false;

            //LightFlash f = new LightFlash(Owner.Position); //, new Color(168, 140, 135, 224), 100
            //CreateEntity(f);

            Vector2 temp;

            //Game1.Instance.SoundManager.PlayEffect("silencer" + Game1.Instance.Random.Next(1, 3).ToString(), Owner.Position, GetTime());
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
                                ((BeingState)((Being)targetList[i]).CurrentState).GetHit(direction, this);

                            ((PhysicalEntityState)((PhysicalEntity)targetList[i]).CurrentState).GetPushed(Vector2.Normalize(direction) * knockEffect);
                        }
                    }
                }
                Vector2 pos = (Owner.Position + (Owner.Bounds / 2));
                float x = (float)(pos.X + 5 * Math.Cos(Owner.Rotation));
                float y = (float)(pos.Y + 5 * Math.Sin(Owner.Rotation));
                pos = new Vector2(x, y);
                CreateEntity(new GraphicsLine(pos, temp));

            }
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
        }
    }
}
