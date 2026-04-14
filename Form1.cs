using System.DirectoryServices.ActiveDirectory;

namespace TaskForEvent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            g.DrawRectangle(new Pen(Color.Red), 200, 100, 50, 30);
            g.FillRectangle(new SolidBrush(Color.Yellow), 200, 100, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), 200, 100, 50, 30);
        }
    }
}
