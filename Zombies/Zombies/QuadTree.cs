using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombies.entities;

namespace Zombies
{
    enum QuadType
    {
        Parent,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    class QuadTree
    {
        private int maxCapacity = 20;
        private float minGridSize = 2.0f;
        private float gridSize;

        private List<GraphicalEntity> entities;

        internal List<GraphicalEntity> Entities
        {
            get { return entities; }
            set { entities = value; }
        }
        private Vector2 position;

        private QuadTree parent;
        private QuadTree topLeft;
        private QuadTree topRight;
        private QuadTree bottomLeft;
        private QuadTree bottomRight;
        private QuadType quadType;
        private Rectangle boundRect;

        private Texture2D tex = Game1.Instance.ResourceManager.RequestTexture("treeFrame");

        private void createTopLeft()
        {
            if (topLeft == null)
                topLeft = new QuadTree(position, gridSize / 2, this, QuadType.TopLeft);
        }

        private void createTopRight()
        {
            if (topRight == null)
                topRight = new QuadTree(position + new Vector2(gridSize / 2, 0), gridSize / 2, this, QuadType.TopRight);
        }

        private void createBottomLeft()
        {
            if (bottomLeft == null)
                bottomLeft = new QuadTree(position + new Vector2(0, gridSize / 2), gridSize / 2, this, QuadType.BottomLeft);
        }

        private void createBottomRight()
        {
            if (bottomRight == null)
                bottomRight = new QuadTree(position + new Vector2(gridSize / 2, gridSize / 2), gridSize / 2, this, QuadType.BottomRight);
        }

        public QuadTree(Vector2 position, float gridSize, QuadTree parent, QuadType quadType)
        {
            this.topLeft = null;
            this.topRight = null;
            this.bottomLeft = null;
            this.bottomRight = null;
            this.position = position;
            this.gridSize = gridSize;
            this.parent = parent;
            this.entities = new List<GraphicalEntity>();
            this.quadType = quadType;
            this.boundRect = new Rectangle((int)position.X, (int)position.Y, (int)gridSize, (int)gridSize);
        }

        public QuadTree SelectLeaf(Vector2 position)
        {
            if (topLeft != null && topLeft.boundRect.Contains(new Point((int)position.X, (int)position.Y)))
                return topLeft.SelectLeaf(position);
            else if (topRight != null && topRight.boundRect.Contains(new Point((int)position.X, (int)position.Y)))
                return topRight.SelectLeaf(position);
            else if (bottomLeft != null && bottomLeft.boundRect.Contains(new Point((int)position.X, (int)position.Y)))
                return bottomLeft.SelectLeaf(position);
            else if (bottomRight != null && bottomRight.boundRect.Contains(new Point((int)position.X, (int)position.Y)))
                return bottomRight.SelectLeaf(position);
            else
                return this;
        }

        public void SelectAllLeafs(ref List<QuadTree> result)
        {
            if (HasChildren())
            {
                if (topLeft != null)
                    topLeft.SelectAllLeafs(ref result);
                if (topRight != null)
                    topRight.SelectAllLeafs(ref result);
                if (bottomLeft != null)
                    bottomLeft.SelectAllLeafs(ref result);
                if (bottomRight != null)
                    bottomRight.SelectAllLeafs(ref result);
            }
            else
            {
                result.Add(this);
            }
        }

        public void SelectLeafs(Rectangle rect, ref List<QuadTree> result)
        {
            if (rect.Contains(boundRect))
            {
                SelectAllLeafs(ref result);
            }
            else if (rect.Intersects(boundRect))
            {
                result.Add(this);

                if (topLeft != null)
                    topLeft.SelectLeafs(rect, ref result);

                if (topRight != null)
                    topRight.SelectLeafs(rect, ref result);

                if (bottomLeft != null)
                    bottomLeft.SelectLeafs(rect, ref result);

                if (bottomRight != null)
                    bottomRight.SelectLeafs(rect, ref result);
            }
        }

        public void DrawFrame(SpriteBatch spriteBatch, Camera camera)
        {
            float x, y;

            // TOP
            y = position.Y;
            for (x = position.X; x < position.X + gridSize; x += 10.0f)
            {
                spriteBatch.Draw(tex, camera.RelativePosition(new Vector2(x, y)), Color.White);
            }

            // BOTTOM
            y = position.Y + gridSize;
            for (x = position.X; x < position.X + gridSize; x += 10.0f)
            {
                spriteBatch.Draw(tex, camera.RelativePosition(new Vector2(x, y)), Color.White);
            }

            // LEFT
            x = position.X;
            for (y = position.Y; y < position.Y + gridSize; y += 10.0f)
            {
                spriteBatch.Draw(tex, camera.RelativePosition(new Vector2(x, y)), Color.White);
            }

            // RIGHT
            x = position.X + gridSize;
            for (y = position.Y; y < position.Y + gridSize; y += 10.0f)
            {
                spriteBatch.Draw(tex, camera.RelativePosition(new Vector2(x, y)), Color.White);
            }

            if (topLeft != null)
                topLeft.DrawFrame(spriteBatch, camera);
            if (topRight != null)
                topRight.DrawFrame(spriteBatch, camera);
            if (bottomLeft != null)
                bottomLeft.DrawFrame(spriteBatch, camera);
            if (bottomRight != null)
                bottomRight.DrawFrame(spriteBatch, camera);
        }

        public void DrawIntersected(Rectangle rect, SpriteBatch spriteBatch, Camera camera)
        {
            if (rect.Contains(boundRect))
            {
                DrawFrame(spriteBatch, camera);
            }
            else if (rect.Intersects(boundRect))
            {
                if (topLeft != null)
                    topLeft.DrawIntersected(rect, spriteBatch, camera);

                if (topRight != null)
                    topRight.DrawIntersected(rect, spriteBatch, camera);

                if (bottomLeft != null)
                    bottomLeft.DrawIntersected(rect, spriteBatch, camera);

                if (bottomRight != null)
                    bottomRight.DrawIntersected(rect, spriteBatch, camera);
            }
        }

        public void SelectAll(ref List<GraphicalEntity> result)
        {
            result.AddRange(entities);

            if (HasChildren())
            {
                if (topLeft != null)
                    topLeft.SelectAll(ref result);
                if (topRight != null)
                    topRight.SelectAll(ref result);
                if (bottomLeft != null)
                    bottomLeft.SelectAll(ref result);
                if (bottomRight != null)
                    bottomRight.SelectAll(ref result);
            }
        }

        public void Select(Rectangle rect, ref List<GraphicalEntity> result)
        {
            if (rect.Contains(boundRect))
            {
                SelectAll(ref result);
            }
            else if (rect.Intersects(boundRect))
            {
                foreach (GraphicalEntity entity in entities)
                {
                    if (entity.Position.X >= rect.X && entity.Position.X <= rect.X + rect.Width &&
                        entity.Position.Y >= rect.Y && entity.Position.Y <= rect.Y + rect.Height)
                    {
                        result.Add(entity);
                    }
                }

                if (topLeft != null)
                    topLeft.Select(rect, ref result);

                if (topRight != null)
                    topRight.Select(rect, ref result);

                if (bottomLeft != null)
                    bottomLeft.Select(rect, ref result);

                if (bottomRight != null)
                    bottomRight.Select(rect, ref result);
            }
        }

        private void RemoveSelf()
        {
            if (quadType == QuadType.Parent)
                return;
            else if (quadType == QuadType.TopLeft)
                parent.topLeft = null;
            else if (quadType == QuadType.TopRight)
                parent.topRight = null;
            else if (quadType == QuadType.BottomLeft)
                parent.bottomLeft = null;
            else
                parent.bottomRight = null;
        }

        public void InsertFromTop(GraphicalEntity entity)
        {
            if (parent != null)
                parent.InsertFromTop(entity);
            else
                Insert(entity);
        }

        public int CountEntities()
        {
            int count = entities.Count;

            if (topLeft != null)
                count += topLeft.CountEntities();

            if (topRight != null)
                count += topRight.CountEntities();

            if (bottomLeft != null)
                count += bottomLeft.CountEntities();

            if (bottomRight != null)
                count += bottomRight.CountEntities();

            return count;
        }

        public void TryToMerge()
        {
            if (CountEntities() <= maxCapacity)
            {
                SelectAll(ref entities);

                foreach (GraphicalEntity g in entities)
                    g.QuadTree = this;

                RemoveChildren();
            }
        }

        public void RemoveChildren()
        {
            if (topLeft != null)
            {
                if (topLeft.HasChildren())
                    topLeft.RemoveChildren();
                else
                    topLeft = null;
            }

            if (topRight != null)
            {
                if (topRight.HasChildren())
                    topRight.RemoveChildren();
                else
                    topRight = null;
            }

            if (bottomLeft != null)
            {
                if (bottomLeft.HasChildren())
                    bottomLeft.RemoveChildren();
                else
                    bottomLeft = null;
            }

            if (bottomRight != null)
            {
                if (bottomRight.HasChildren())
                    bottomRight.RemoveChildren();
                else
                    bottomRight = null;
            }
        }

        public bool Remove(GraphicalEntity entity)
        {
            bool r = entities.Remove(entity);

            if (!r)
                return false;

            if (quadType != QuadType.Parent)
                parent.TryToMerge();

            return true;
        }

        public bool Inside(GraphicalEntity entity)
        {
            return (entity.Position.X >= boundRect.X &&
                entity.Position.X <= boundRect.X + gridSize &&
                entity.Position.Y >= boundRect.Y &&
                entity.Position.Y <= boundRect.Y + gridSize);
        }

        private void InsertChild(GraphicalEntity entity)
        {
            Vector2 tmp = entity.Position - position;

            // Top left
            if (tmp.X <= gridSize / 2 && tmp.Y <= gridSize / 2)
            {
                createTopLeft();
                topLeft.Insert(entity);
            }
            // Top right
            else if (tmp.X > gridSize / 2 && tmp.Y <= gridSize / 2)
            {
                createTopRight();
                topRight.Insert(entity);
            }
            // Bottom left
            else if (tmp.X <= gridSize / 2 && tmp.Y > gridSize / 2)
            {
                createBottomLeft();
                bottomLeft.Insert(entity);
            }
            // Bottom right
            else
            {
                createBottomRight();
                bottomRight.Insert(entity);
            }
        }

        public bool HasChildren()
        {
            return (topLeft != null || topRight != null || bottomLeft != null || bottomRight != null);
        }

        private void BreakNode()
        {
            if (gridSize > minGridSize)
            {
                foreach (GraphicalEntity entity in entities)
                    InsertChild(entity);

                entities.Clear();
            }
        }

        public void Insert(GraphicalEntity entity)
        {
            if (HasChildren())
                InsertChild(entity);
            else
            {
                Vector2 tmp = entity.Position - position;

                if (Inside(entity))
                {
                    entities.Add(entity);
                    entity.QuadTree = this;

                    //Console.WriteLine(position.X + "-" + position.Y + "\t" + gridSize + "\t\tAdding entity at " + entity.Position.X + "-" + entity.Position.Y);

                    if (entities.Count > maxCapacity)
                        BreakNode();
                }
                // else
                //Console.WriteLine(position.X + "-" + position.Y + "\t" + gridSize + "\t\tEntity outside at " + entity.Position.X + "-" + entity.Position.Y);
            }
        }
    }
}
