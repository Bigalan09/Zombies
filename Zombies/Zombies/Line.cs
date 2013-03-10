using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies
{
    class Line
    {
        private float xMin;
        private float xMax;
        private float k;
        private float m;
        private Vector2 start, stop;

        public Vector2 Stop
        {
            get { return stop; }
            set { stop = value; }
        }

        public Vector2 Start
        {
            get { return start; }
            set { start = value; }
        }

        public Line()
        {
        }

        public Line(Vector2 a, Vector2 b)
        {
            k = (a.Y - b.Y) / (a.X - b.X);
            m = a.Y - (k * a.X);
            xMax = Math.Max(a.X, b.X);
            xMin = Math.Min(a.X, b.X);

            start = a;
            stop = b;
        }

        public static bool IsCut(Line a, Line b)
        {
            float x;

            if (a.k == b.k)
                return false;

            x = (b.m - a.m) / (a.k - b.k);

            if ((a.xMin < x) && (x < a.xMax) &&
                (b.xMin < x) && (x < b.xMax))
                return true;

            return false;
        }

        public static Vector2 Intersects(Line a, Line b)
        {
            float x;
            x = (b.m - a.m) / (a.k - b.k);
            return new Vector2(x, a.k * x + a.m);
        }

        public static bool IsCut(Line a, Rectangle b)
        {
            return (IsCut(a, new Line(new Vector2(b.X, b.Y), new Vector2(b.Right, b.Bottom))) ||
                    IsCut(a, new Line(new Vector2(b.X, b.Bottom), new Vector2(b.Right, b.Y))));
        }


    }
}
