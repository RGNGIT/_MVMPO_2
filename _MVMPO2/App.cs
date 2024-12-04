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
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }

        private bool IsRectangle(Rectangle rectangle)
        {
            if (rectangle.Vertices.Count != 4)
                return false;

            var v = rectangle.Vertices;

            double d1 = DistanceSquared(v[0], v[1]);
            double d2 = DistanceSquared(v[1], v[2]);
            double d3 = DistanceSquared(v[2], v[3]);
            double d4 = DistanceSquared(v[3], v[0]);

            double diag1 = DistanceSquared(v[0], v[2]);
            double diag2 = DistanceSquared(v[1], v[3]);

            return d1 == d3 && d2 == d4 && diag1 == diag2 && ArePerpendicular(v[0], v[1], v[2]) && ArePerpendicular(v[1], v[2], v[3]);
        }

        private double DistanceSquared(Vertex a, Vertex b)
        {
            return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
        }

        private bool ArePerpendicular(Vertex a, Vertex b, Vertex c)
        {
            double dx1 = b.X - a.X;
            double dy1 = b.Y - a.Y;
            double dx2 = c.X - b.X;
            double dy2 = c.Y - b.Y;
            return dx1 * dx2 + dy1 * dy2 == 0;
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            try
            {
                string input = textBoxToParse.Text;

                GeometryParser parser = new GeometryParser(input);
                List<Rectangle> rectangles = parser.ParseGeometryFigures();

                List<string> lines = input.Split('\n').ToList();

                for (int i = 0; i < lines.Count; i++)
                    lines[i] = lines[i] + ' ' + (IsRectangle(rectangles[i]) ? "// Прямоугольник" : "// Произвольная фигура");

                textBoxToParse.Text = string.Join("\n", lines);

                Draw draw = new Draw(rectangles);
                draw.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Синтакс еррор");
            }
        }
    }
}
