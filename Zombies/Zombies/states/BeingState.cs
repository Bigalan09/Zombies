using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;
using Zombies.entities.weapons;

namespace Zombies.states
{
    class BeingState : PhysicalEntityState
    {
        public Being Being
        {
            get { return (Being)base.Owner; }
            set { base.Owner = value; }
        }

        public override void Act(GameTime gameTime)
        {

        }

        public virtual void Attack() { }

        public virtual void SecondaryAttack() { }

        public virtual void StopFire() { }

        public virtual void Use() { }

        public virtual void Turn(Vector2 direction)
        {
            if (direction.Length() != 0)
                Being.FaceVector = direction;
        }

        public virtual void Walk(Vector2 direction) { }

        public virtual void Idle() { }

        public virtual void Dying()
        {
            Being.Alive = false;
        }

        public virtual void GetHit(Weapon weapon)
        {
            Being.Health -= weapon.Damage;

            ((Being)Owner).GetHit(weapon);
        }

        public virtual void GetHit(Vector2 direction, Weapon weapon)
        {
            Being.Health -= weapon.Damage;
            ((Being)Owner).GetHit(weapon);
        }

        public virtual void GetHit(float damage)
        {
            Being.Health -= damage;
            ((Being)Owner).GetHit(null);
        }

        public virtual void GetHit(Vector2 direction, float damage)
        {
            Being.Health -= damage;

            ((Being)Owner).GetHit(null);
        }

        public virtual void StopWalk() { }



    }
}
