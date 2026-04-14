using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForEvent.Objects
{
    internal class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;

        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        public virtual void Render(Graphics g)
        {
           
        }

    }
}
