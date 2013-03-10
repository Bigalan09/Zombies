using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.states;
using Zombies.strategy;

namespace Zombies.entities
{
    [Serializable()]
    class Entity
    {

        public delegate void onEntitiesInLine(Line line, ArrayList result);
        public delegate void onEntitiesInRadius(float radius, Vector2 position, ArrayList result);
        public delegate void onFetchAll(Type type, ArrayList result);

        public delegate void onCreate(Entity ent);

        public event onEntitiesInLine entitiesInLine;
        public event onEntitiesInRadius entitiesInRadius;
        public event onFetchAll fetchAll;
        public event onCreate createEntity;

        static int idCount = 0;
        [NonSerialized()]
        private EntityState currentState;
        [NonSerialized()]
        private Strategy currentStrategy;
        private String id;
        private bool alive;
        private int activeThinkDelay = 10;
        private int inActiveThinkDelay = 10000;
        private int sinceLastThink;

        public void EntitiesInLine(Line line, ArrayList result)
        {
            if (entitiesInLine != null)
                entitiesInLine(line, result);
        }

        public void EntitiesInRadius(float radius, Vector2 position, ArrayList result)
        {
            if (entitiesInRadius != null)
                entitiesInRadius(radius, position, result);
        }

        public void FetchAll(Type type, ArrayList result)
        {
            if (fetchAll != null)
                fetchAll(type, result);
        }

        public void CreateEntity(Entity ent)
        {
            if (createEntity != null)
                createEntity(ent);
        }

        public int SinceLastThink
        {
            get { return sinceLastThink; }
            set { sinceLastThink = value; }
        }

        public int ActiveThinkDelay
        {
            get { return activeThinkDelay; }
            set { activeThinkDelay = value; }
        }

        public int InActiveThinkDelay
        {
            get { return inActiveThinkDelay; }
            set { inActiveThinkDelay = value; }
        }

        public Strategy CurrentStrategy
        {
            get { return currentStrategy; }
            set
            {
                currentStrategy = value;
                currentStrategy.Owner = this;
                currentStrategy.Initialize();
            }
        }

        public bool Alive
        {
            get { return alive; }
            set
            {
                alive = value;
                if (!alive)
                    OnDeath();
            }
        }

        public Entity()
        {
            alive = true;
            id = "entity" + idCount;
            idCount++;

            if (isActiveThink())
                sinceLastThink = Game1.Instance.Random.Next(activeThinkDelay);
            else
                sinceLastThink = Game1.Instance.Random.Next(inActiveThinkDelay);
        }

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public EntityState CurrentState
        {
            get { return currentState; }
            set
            {
                if (currentState != null)
                    currentState.LeavingState();
                currentState = value;
                currentState.Owner = this;
                currentState.EnteringState();

            }
        }

        protected virtual void Act(GameTime gameTime)
        {
            if (currentStrategy != null)
                currentStrategy.Act(gameTime);
            if (currentState != null)
                currentState.Act(gameTime);
        }

        public virtual void OnDeath()
        {
        }

        public virtual void SetNextThink(GameTime gameTime)
        {

        }

        public void Think(GameTime gameTime)
        {
            if (Math.Abs(gameTime.ElapsedGameTime.Milliseconds) == 0)
                sinceLastThink += 16;
            else
                sinceLastThink += gameTime.ElapsedGameTime.Milliseconds;

            if (isActiveThink())
            {
                if (sinceLastThink >= activeThinkDelay)
                {
                    sinceLastThink = 0;
                    Act(gameTime);
                }
            }
            else if (isInActiveThink())
            {
                if (sinceLastThink >= inActiveThinkDelay)
                {
                    sinceLastThink = 0;
                    Act(gameTime);
                }
            }
        }

        public virtual bool isActiveThink()
        {
            return true;
        }

        public virtual bool isInActiveThink()
        {
            return true;
        }

        public virtual void ClientThink(GameTime gameTime)
        {
        }

        public virtual void Initialize()
        {
        }

        public float GetTime()
        {
            return 1.0f;
        }

        protected String getNewID()
        {
            idCount++;
            return "entity" + idCount;
        }
        public virtual Entity Clone()
        {
            Entity clone = (Entity)this.MemberwiseClone();
            clone.Id = getNewID();
            return clone;

        }

    }
}
