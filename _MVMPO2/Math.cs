using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _MVMPO2
{
    public class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vertex(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class Rectangle
    {
        public List<Vertex> Vertices { get; set; }

        public Rectangle(List<Vertex> vertices)
        {
            if (vertices.Count != 4)
                throw new ArgumentException("Прямоугольник должен иметь 4 вершины.");
            Vertices = vertices;
        }
    }

    public class GeometryParser
    {
        private readonly string _input;
        private int _position;

        public GeometryParser(string input)
        {
            _input = input;
            _position = 0;
        }

        public List<Rectangle> ParseGeometryFigures()
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            while (_position < _input.Length)
            {
                SkipWhitespace();
                rectangles.Add(ParseRectangle());
            }
            return rectangles;
        }

        private Rectangle ParseRectangle()
        {
            List<Vertex> vertices = new List<Vertex>();
            Expect('{');

            for (int i = 0; i < 4; i++)
            {
                vertices.Add(ParseVertex());
                SkipWhitespace();
            }

            Expect('}');
            return new Rectangle(vertices);
        }

        private Vertex ParseVertex()
        {
            Expect('{');
            double x = ParseCoordinate();
            Expect(',');
            double y = ParseCoordinate();
            Expect('}');
            return new Vertex(x, y);
        }

        private double ParseCoordinate()
        {
            SkipWhitespace();
            bool isNegative = false;

            if (_input[_position] == '-' || _input[_position] == '+')
            {
                isNegative = _input[_position] == '-';
                _position++;
            }

            string number = ParseNumberWithDot();
            double result = double.Parse(number, CultureInfo.InvariantCulture);
            return isNegative ? -result : result;
        }

        private string ParseNumberWithDot()
        {
            string result = "";

            while (_position < _input.Length && (char.IsDigit(_input[_position]) || _input[_position] == '.'))
            {
                result += _input[_position];
                _position++;
            }

            return result;
        }

        private void SkipWhitespace()
        {
            while (_position < _input.Length && char.IsWhiteSpace(_input[_position]))
            {
                _position++;
            }
        }

        private void Expect(char expected)
        {
            SkipWhitespace();
            if (_position >= _input.Length || _input[_position] != expected)
                throw new Exception($"Я ожидал '{expected}', но ты мне дал '{_input[_position]}'");
            _position++;
        }
    }
}
