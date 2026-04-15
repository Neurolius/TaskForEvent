using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace TaskForEvent.Objects
{
    internal class Target : BaseObject
    {
        public Action<Player>? OnPlayerOverlap;
        public event Action<Target>? OnSizeZero;
        public int score = 1;
        public float size = 50;
        float shrinkSize = 0.5f;

        public Target(float x, float y, float angle) : base(x,y,angle) { }

        public override void Render(Graphics g)
        {
            using var brush = new SolidBrush(Color.Green);
            g.FillEllipse(brush, -size / 2, -size / 2, size, size);
        }

        public void updateSize()
        {
            if (size <= 0)
                return;

            size -= shrinkSize;

            if (size <= 0)
            {
                size = 0;
                OnSizeZero?.Invoke(this);
            }
        }

        public void Respawn(float x, float y, float newSize=50)
        {
            X = x;
            Y = y;
            size = newSize;
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-size / 2, -size / 2, size, size);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            OnPlayerOverlap?.Invoke(obj as Player);
        }
    }
}
