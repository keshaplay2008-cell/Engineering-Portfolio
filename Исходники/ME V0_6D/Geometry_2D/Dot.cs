using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Geometry_2D
{
    public class Dot
    {
        public float x, y;
        public Dot(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2 ToOpenTK() => new Vector2(x, y);
        public void Draw()
        {
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(x, y);
            GL.End();
        }
        public void Draw(float Size, Color color)
        {
            GL.PointSize(Size);
            GL.Color3(color);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(x, y);
            GL.End();
        }
        public static Dot operator +(Dot d1, Dot d2) => new Dot(d1.x + d2.x, d1.y + d2.y);
        public static Dot operator -(Dot d1, Dot d2) => new Dot(d1.x - d2.x, d1.y - d2.y);
        public static Dot operator *(Dot d1, float f) => new Dot(d1.x * f, d1.y * f);
        public static Dot operator /(Dot d1, float f) => new Dot(d1.x / f, d1.y / f);
        public static float operator *(Dot d1, Dot d2) => d1.x * d2.x + d1.y * d2.y;
        public static bool operator== (Dot d1, Dot d2) => d1.x == d2.x && d1.y == d2.y;
        public static bool operator!= (Dot d1, Dot d2) => d1.x != d2.x || d1.y != d2.y;

        public float GetLong() => MathF.Sqrt(x * x + y * y);
        public float GetSquareLong() => x * x + y * y;

        public override string ToString() => $"{x}, {y}";
    }
}
