using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.entities.weapons
{
    class Weapon : GraphicalEntity
    {

        private PhysicalEntity owner;
        private float reloadTime;
        private int clipSize;
        private float cooldown;
        private float counter;
        private float damage;

        public float Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public float Counter
        {
            get { return counter; }
            set { counter = value; }
        }
        private bool allowFire;


        public bool AllowFire
        {
            get { return allowFire; }
            set { allowFire = value; }
        }

        public float Cooldown
        {
            get { return cooldown; }
            set { cooldown = value; }
        }

        public float ReloadTime
        {
            get { return reloadTime; }
            set { reloadTime = value; }
        }

        public int ClipSize
        {
            get { return clipSize; }
            set { clipSize = value; }
        }

        public PhysicalEntity Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
            this.Position = Owner.Position;
            counter -= 1 * GetTime();

            if (counter <= 0)
                AllowFire = true;
        }

        public virtual void Fire()
        {
            if (!AllowFire)
                return;

            counter = cooldown;
        }

        public virtual void Fire(Vector2 direction)
        {
            if (!AllowFire)
                return;

            counter = cooldown;
        }

        public override bool isActiveThink()
        {
            return true;
        }

        public override bool isInActiveThink()
        {
            return true;
        }

        public virtual void Reload() { }

    }
}
