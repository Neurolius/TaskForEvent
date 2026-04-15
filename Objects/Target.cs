using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace TaskForEvent.Objects
{
    internal class Target : BaseObject
    {
        public Action<Player> OnPlayerOverlap;
        public int score = 1;

        public Target(float x, float y, float angle) : base(x,y,angle) { }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -50, -50, 25, 25);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-50, -50, 25, 25);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Player)
            {
                OnPlayerOverlap(obj as Player);
            }
        }
    }
}
