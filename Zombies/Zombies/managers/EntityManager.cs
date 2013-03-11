using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;
using Zombies.states;

namespace Zombies.managers
{
    class EntityManager
    {
        private Hashtable entities = new Hashtable();
        private Hashtable newEntities = new Hashtable();
        private Rectangle selectRect;

        public Rectangle SelectRect
        {
            get { return selectRect; }
            set { selectRect = value; }
        }


        //temporary
        private float time;

        public float Time
        {
            get { return time; }
            set { time = value; }
        }

        private QuadTree quadTree;

        private List<GraphicalEntity> results;

        internal List<GraphicalEntity> Results
        {
            get { return results; }
            set { results = value; }
        }

        internal QuadTree QuadTree
        {
            get { return quadTree; }
            set { quadTree = value; }
        }

        public EntityManager()
        {
            float gridSize = 262144.0f / 2;
            quadTree = new QuadTree(new Vector2(-gridSize / 2, -gridSize / 2), gridSize, null, QuadType.Parent);
            Random r = Game1.Instance.Random;
            Time = 1.0f;
            selectRect = new Rectangle(400, 400, 400, 400);
            results = new List<GraphicalEntity>();
        }

        public Hashtable Entities
        {
            get { return entities; }
            set { entities = value; }
        }

        public void AddEntity(Entity entity)
        {
            //Events
            entity.entitiesInLine += new Entity.onEntitiesInLine(EntitiesInLine);
            entity.entitiesInRadius += new Entity.onEntitiesInRadius(EntitiesInRadius);
            entity.fetchAll += new Entity.onFetchAll(FetchAll);
            entity.createEntity += new Entity.onCreate(AddEntity);

            if (!newEntities.ContainsKey(entity.Id))
                newEntities.Add(entity.Id, entity);

            if (entity is GraphicalEntity)
                quadTree.Insert((GraphicalEntity)entity);

            entity.Initialize();
        }

        public void Think(GameTime gameTime)
        {
            results.Clear();
            quadTree.Select(selectRect, ref results);

            foreach (GraphicalEntity g in results)
            {
                g.Think(gameTime);
            }
        }

        public void CollideAndMove()
        {
            List<PhysicalEntity> temp = new List<PhysicalEntity>();

            foreach (GraphicalEntity g in results)
            {
                if (g is PhysicalEntity && !(g is Usable))
                    temp.Add((PhysicalEntity)g);
            }

            for (int i = 0; i < temp.Count; i++)
            {
                ((PhysicalEntityState)temp[i].CurrentState).CollideAndMove(temp);
            }
        }

        /*
        public void CollideAndMove()
        {
            List<GraphicalEntity> temp = new List<GraphicalEntity>();
            List<PhysicalEntity> toCheck = new List<PhysicalEntity>();
            Rectangle rect = new Rectangle();

            foreach (GraphicalEntity g in results)
            {
                if (g is PhysicalEntity)
                {
                    rect.X = (int)(g.CenterPosition.X - 150.0f);
                    rect.Y = (int)(g.CenterPosition.Y - 150.0f);
                    rect.Width = 300;
                    rect.Height = 300;

                    temp.Clear();
                    toCheck.Clear();

                    quadTree.Select(rect, ref temp);

                    foreach (GraphicalEntity g2 in temp)
                        if (g2 is PhysicalEntity)
                            toCheck.Add((PhysicalEntity)g2);

                    ((PhysicalEntityState)g.CurrentState).CollideAndMove(toCheck);
                }
            }
        }
        */

        public void AddListOfEntities(List<Entity> list)
        {
            foreach (Entity e in list)
            {
                this.AddEntity(e);
            }
        }

        public void AddListOfEntities(List<GraphicalEntity> list)
        {
            foreach (GraphicalEntity e in list)
            {
                this.AddEntity(e);
            }
        }

        public void UpdateTreePositions()
        {
            foreach (GraphicalEntity g in results)
                g.UpdateTreePosition();
        }

        public Entity GetEntity(Object id)
        {
            return (Entity)entities[id];
        }

        public void AddEntities()
        {
            foreach (DictionaryEntry entry in newEntities)
            {
                entities.Add(entry.Key, entry.Value);
            }

            newEntities.Clear();
        }

        public void RemoveEntities()
        {
            object[] keys = new object[entities.Count];
            entities.Keys.CopyTo(keys, 0);

            for (int index = keys.Length - 1; index >= 0; --index)
            {
                if (!((Entity)entities[keys[index]]).Alive)
                    entities.Remove(keys[index]);
            }

            // Quadtree
            /*
            foreach (GraphicalEntity g in results)
            {
                if (g.Alive == false)
                    g.QuadTree.Remove(g);
            }
             */
        }

        public void EntitiesInLine(Line line, ArrayList result)
        {
            ArrayList l = new ArrayList();

            foreach (GraphicalEntity g in results)
            {
                if (Line.IsCut(new Line(g.Position, g.Position + g.Bounds), line) ||
                    Line.IsCut(new Line(new Vector2(g.Position.X, g.Position.Y + g.Bounds.Y), new Vector2(g.Position.X + g.Bounds.X, g.Position.Y)), line))
                {
                    l.Add(g);
                }
            }

            result.AddRange(l);
        }

        public void EntitiesInRadius(float radius, Vector2 position, ArrayList result)
        {
            ArrayList l = new ArrayList();

            foreach (GraphicalEntity g in results)
            {
                if ((g.Position + g.Bounds / 2 - position).Length() <= radius)
                    l.Add(g);
            }

            result.AddRange(l);
        }

        public ArrayList EntitiesInBox(PhysicalEntity box)
        {
            ArrayList l = new ArrayList();

            foreach (GraphicalEntity g in results)
            {
                if (PhysicalEntity.RectangularIntersects(g, box))
                    l.Add(g);
            }

            return l;
        }


        public void FetchAll(Type type, ArrayList result)
        {
            ArrayList list = new ArrayList();

            foreach (GraphicalEntity g in results)
            {
                if (g.GetType().Equals(type))
                    list.Add(g);
            }

            result.AddRange(list);
        }

        public void RemoveAll(Type type)
        {
            object[] keys = new object[this.Entities.Count];
            this.Entities.Keys.CopyTo(keys, 0);

            for (int index = keys.Length - 1; index >= 0; --index)
            {
                if (entities[keys[index]].GetType().Equals(type))
                    entities.Remove(keys[index]);
            }
        }
    }
}
