using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    /* FRectangle
     * A struct that has similar functionality to the
     * Rectangle in XNA but the X and Y members are floats
     * instead of integers. Retains some methods but not
     * all since it's not entirely necessary.
     */
    public struct FRectangle
    {
        public float X;
        public float Y;
        public int Width;
        public int Height;

        public FRectangle(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Left
        {
            get { return X; }
        }

        public float Right
        {
            get { return (X + Width); }
        }

        public float Top
        {
            get { return Y; }
        }

        public float Bottom
        {
            get { return (Y + Height); }
        }

        public bool Intersects(FRectangle rec)
        {
            return rec.Top < Bottom
                && rec.Left < Right
                && Left < rec.Right
                && Top < rec.Bottom;
        }

        public static FRectangle Intersection(FRectangle rec1, FRectangle rec2)
        {
            FRectangle intersection;

            if(rec1.Intersects(rec2))
            {
                intersection = new FRectangle(0f, 0f, 0, 0);
            }
            else
            {
                float top = Math.Max(rec1.Y, rec2.Y);
                float bottom = Math.Min(rec1.Y + rec1.Width, rec2.Y + rec2.Height);
                float left = Math.Max(rec1.X, rec2.X);
                float right = Math.Min(rec1.X + rec1.Width, rec2.X + rec2.Width);
                intersection = new FRectangle(left, top, (int) Math.Round(right - left), (int) Math.Round(bottom - top));

            }

            return intersection;
        }


    }
}
