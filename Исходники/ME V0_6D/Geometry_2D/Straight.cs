using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Geometry_2D
{
    public class Straight
    {
        public float k { get; }
        public float n { get; }
        public Straight(float k, float n)
        {
            this.k = k;
            this.n = n;
        }
        public Straight(Dot d1, Dot d2)
        {
            if(d1.x != d2.x)
            {
                k = (d2.y - d1.y) / (d2.x - d1.x);
                n = d1.y - d1.x * k;
            }
            else
            {
                n = d1.x;
                k = float.PositiveInfinity;
            }
        }
    }
    public class Line : Straight
    {
        public Dot Beg { get; }
        public Dot End { get; }
        public bool ReVersX { get; }
        public bool ReVersY { get; }
        public Line(Dot D1, Dot D2): base(D1,D2)
        {
            Beg = D1;
            End = D2;
            ReVersX = End.x < Beg.x;
            ReVersY = End.y < Beg.y;
        }
        public bool Belong(Dot dot)
        {
            float ModK = MathF.Abs(k);
            if (ModK >= 1)
            {
                if (ReVersY)
                {
                    return End.y < dot.y && dot.y < Beg.y;
                }
                else
                {
                    return Beg.y < dot.y && dot.y < End.y;
                }
            }
            else
            {
                if (ReVersX)
                {
                    return End.x < dot.x && dot.x < Beg.x;
                }              
                else           
                {              
                    return Beg.x < dot.x && dot.x < End.x;
                }
            }
        }
        public void Draw()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(Beg.x, Beg.y);
            GL.Vertex2(End.x, End.y);
            GL.End();
        }
        public void Draw(float Size, Color color)
        {
            GL.Color3(color);
            GL.LineWidth(Size);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(Beg.x, Beg.y);
            GL.Vertex2(End.x, End.y);
            GL.End();
        }
    }
}
