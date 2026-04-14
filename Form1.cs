using System.DirectoryServices.ActiveDirectory;
using TaskForEvent.Objects;

namespace TaskForEvent
{
    public partial class Form1 : Form
    {
        MyRectangle myRect;
        List<BaseObject> objects = new();

        public Form1()
        {
            InitializeComponent();
            objects.Add(new MyRectangle(50,50,0));
            objects.Add(new MyRectangle(100,100,45));
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }
    }
}
