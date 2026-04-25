using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Geometry_2D
{
    public class Triangle
    {
        public Dot[] Vertex { get; } = new Dot[3];
        public Line[] Sids { get; } = new Line[3];
        public Dot center 
        { 
            get
            {
                Dot c = Vertex[0];
                for (int i = 1; i < Vertex.Length; i++)
                {
                    c = (Vertex[i] + c * i) / (i + 1);
                }
                return c;
            }
        }
        public Triangle(Dot V1, Dot V2, Dot V3)
        {
            Vertex[0] = V1;
            Vertex[1] = V2;
            Vertex[2] = V3;
            Sids[0] = new Line(V1, V2);
            Sids[1] = new Line(V2, V3);
            Sids[2] = new Line(V3, V1);
        }
        public void Move(Dot Speed)
        {
            for (int i = 0; i < Vertex.Length; i++)
            {
                Vertex[i] += Speed;
            }
        }
        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(Vertex[0].x, Vertex[0].y);
            GL.Vertex2(Vertex[1].x, Vertex[1].y);
            GL.Vertex2(Vertex[2].x, Vertex[2].y);
            GL.End();
        }
        public void Draw(Color color)
        {
            GL.Color3(color);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(Vertex[0].x, Vertex[0].y);
            GL.Vertex2(Vertex[1].x, Vertex[1].y);
            GL.Vertex2(Vertex[2].x, Vertex[2].y);
            GL.End();
        }
        public void DrawBorder(float Size,Color color)
        {
            for (int i = 0; i < Sids.Length; i++)
            {
                Sids[i].Draw(Size,color);
            }
        }
        public void DrawVertex(float Size, Color color)
        {
            for (int i = 0; i < Vertex.Length; i++)
            {
                Vertex[i].Draw(Size, color);
            }
        }
        public Triangle Scaling(Dot Focus, float ScalX)
        {
            Dot[] dots = new Dot[Vertex.Length];
            for (int i = 0; i < Vertex.Length; i++)
            {
                dots[i] = Focus + (Vertex[i] - Focus) * ScalX;
            }
            return new Triangle(dots[0], dots[1], dots[2]);
        }
    }
}
