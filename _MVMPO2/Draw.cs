using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _MVMPO2
{
    public partial class Draw : Form
    {
        private List<Rectangle> rectangles = new List<Rectangle>();

        public Draw(List<Rectangle> rectangles)
        {
            InitializeComponent();
            this.rectangles = rectangles;
        }

        private void DrawRectangle(Graphics g, Rectangle rectangle)
        {
            Pen pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Red);

            for (int i = 0; i < rectangle.Vertices.Count; i++)
            {
                Vertex v1 = rectangle.Vertices[i];
                Vertex v2 = rectangle.Vertices[(i + 1) % rectangle.Vertices.Count];

                g.DrawLine(pen, (float)v1.X, (float)v1.Y, (float)v2.X, (float)v2.Y);
                g.FillEllipse(brush, (float)v1.X - 3, (float)v1.Y - 3, 6, 6);
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            foreach (var rectangle in rectangles)
                DrawRectangle(e.Graphics, rectangle);
        }
    }
}
