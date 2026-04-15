using System.DirectoryServices.ActiveDirectory;
using TaskForEvent.Objects;

namespace TaskForEvent
{
    public partial class Form1 : Form
    {
        MyRectangle myRect;
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        List<Target> targets = new();
        int globalScore = 0;

        public Form1()
        {
            InitializeComponent();

            player = new Player(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);
            targets = new List<Target>();
            targets.Add(new Target(pictureBox1.Width / 2, pictureBox1.Height / 2 + 50, 0));
            targets.Add(new Target(pictureBox1.Width / 2, pictureBox1.Height / 2, 0));

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            var rnd = new Random();
            foreach (var target in targets)
            {
                target.OnPlayerOverlap += (p) =>
                {
                    if (p is Player)
                    {
                        target.X = rnd.Next(20, pictureBox1.Width - 20);
                        target.Y = rnd.Next(20, pictureBox1.Height - 20);
                        target.size = rnd.Next(50, 80);
                        globalScore+=target.score;
                        Score.Text = "Счет: " + globalScore.ToString();
                    }
                };

                target.OnSizeZero += (t) =>
                {
                    t.Respawn(
                        rnd.Next(20, pictureBox1.Width - 20),
                        rnd.Next(20, pictureBox1.Height - 20),
                        rnd.Next(50,80)
                    );
                };
            }

            marker = new Marker(pictureBox1.Width / 2 + 50, pictureBox1.Height / 2 + 50, 0);
            

            objects.Add(player);
            objects.Add(marker);
            objects.AddRange(targets);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            updatePlayer();

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var target in targets)
                target.updateSize();

            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
