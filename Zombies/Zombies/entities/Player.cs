using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities.weapons;

namespace Zombies.entities
{
    class Player : Being
    {
        private Cursor aim;
        private Weapon primaryWeapon = null;

        public Weapon PrimaryWeapon
        {
            get { return primaryWeapon; }
            set
            {
                primaryWeapon = value;
                primaryWeapon.Owner = this;
            }
        }

        private Weapon secondaryWeapon = null;

        public Weapon SecondaryWeapon
        {
            get { return secondaryWeapon; }
            set
            {
                secondaryWeapon = value;
                secondaryWeapon.Owner = this;
                //CreateEntity(SecondaryWeapon);
            }
        }

        private PlayerIndex playerIndex;

        public Player(Vector2 position)
            : base(position)
        {
            Health = 50.0f;
            TexturePath = ("player");
            Speed = 8.0f;
            Mass = 2.0f;
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            this.DrawLayer = 1000;
            Friction = 0.3f;
            aim = new Cursor(this);
            this.PrimaryWeapon = new Pistol(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            CreateEntity(aim);
            CreateEntity(primaryWeapon);
            CreateEntity(secondaryWeapon);
        }

        public Player()
            : base()
        {
            Health = 100.0f;
            TexturePath = ("player");
            Speed = 8.0f;
            Mass = 2.0f;
            this.ActiveThinkDelay = 10;
            this.InActiveThinkDelay = 10;
            this.DrawLayer = 1000;
            Friction = 0.3f;
            aim = new Cursor(this);
            aim.Owner = this;
            CreateEntity(aim);
            this.PrimaryWeapon = new Pistol(this);
        }

        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
            set { playerIndex = value; }
        }

        public Cursor Aim
        {
            get { return aim; }
            set { aim = value; }
        }

        protected override void Act(GameTime gameTime)
        {
            base.Act(gameTime);
        }

        public override void OnDeath()
        {
            base.OnDeath();
            aim.Alive = false;
        }

        public override bool isActiveThink()
        {
            return true;
        }

        public override bool isInActiveThink()
        {
            return true;
        }
    }
}
