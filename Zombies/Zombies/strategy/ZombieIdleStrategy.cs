using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Zombies.entities;
using Zombies.entities.weapons;
using Zombies.states.zombie;

namespace Zombies.strategy
{
    [Serializable()]
    class ZombieIdleStrategy : Strategy
    {
        private Vector2 direction;
        [NonSerialized()]
        private Timer timer;
        private Random r;
        private ArrayList players;
        private bool walking;

        public ZombieIdleStrategy()
        {
            this.timer = new Timer();
            r = Game1.Instance.Random;
            players = new ArrayList();
            walking = true;
            timer.Interval = r.Next(1, 3000);
            timer.Start();
            timer.Elapsed += new ElapsedEventHandler(NewDirection);
            direction = new Vector2(0.5f - (float)r.NextDouble(), 0.5f - (float)r.NextDouble());
        }

        public override void Initialize()
        {
            base.Initialize();

            ((Being)Owner).gotHit += new Being.onHit(getHit);
        }

        private void getHit(Being b, Weapon weapon)
        {
            callFriends();
        }

        public void callFriends()
        {
            ArrayList l = new ArrayList();
            Owner.EntitiesInRadius(100.0f, ((Being)Owner).Position, l);

            foreach (Entity e in l)
            {
                if (e is Zombie)
                {
                    //((Zombie)e).Speed = speed;
                    ((Zombie)e).CurrentStrategy = new ZombieStrategy();
                }
            }
        }

        private void NewDirection(object source, ElapsedEventArgs e)
        {
            timer.Interval = 2000;

            if (walking)
            {
                direction = new Vector2(0.5f - (float)r.NextDouble(), 0.5f - (float)r.NextDouble());
            }
        }

        int staretime = 0;

        public override void Act(GameTime gameTime)
        {
            bool playerSeen = false;
            foreach (Player p in players)
            {
                if ((p.Position - ((Zombie)Owner).Position).Length() < 300.0f)
                {
                    playerSeen = true;
                }
            }

            if (!playerSeen)
                staretime = 0;
            else
                staretime++;

            if (staretime >= 60)
                callFriends();

            walking = !playerSeen;

            if (walking)
                ((ZombieState)Owner.CurrentState).Walk(direction);

            if (players == null || players.Count == 0)
                Owner.FetchAll(typeof(Player), players);

            ((ZombieState)Owner.CurrentState).Turn(direction);

        }
    }
}
