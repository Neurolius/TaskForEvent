using System.DirectoryServices.ActiveDirectory;
using TaskForEvent.Objects;

namespace TaskForEvent
{
    public partial class Form1 : Form
    {
        MyRectangle myRect;

        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(0, 0, 0);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            myRect.Render(g);
        }
    }
}
