using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Geometry_2D;
using Geometry_3D;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using OpenTK.Windowing.Desktop;

namespace Collision
{
    public class Collider
    {
        public Triangle[] triangles { get; }
        public Dot SpeedVector { get; set; } = new Dot(0, 0);
        public Dot center { get
            {
                Dot c = Vertexes[0];
                for (int i = 1; i < Vertexes.Length; i++)
                {
                    c = (Vertexes[i] + c * i) / (i + 1);
                }
                return c;
            }
        }
        public GlobalOrientedHitBox hitBox { get; set; }
        public ColCache ColCache { get; set; }
        public Dot[] Vertexes { get;  }

        public bool IsUs;
        public Collider(Triangle[] triangles, Dot[] Vertexs)
        {
            this.triangles = triangles;
            this.Vertexes = Vertexs;
        }
        public void ReHit()
        {
            float MinX = Vertexes[0].x, MaxX = Vertexes[0].x, MinY = Vertexes[0].y, MaxY = Vertexes[0].y;
            foreach(var el in Vertexes)
            {
                MinX = MathF.Min(MinX, el.x);
                MaxX = MathF.Max(MaxX, el.x);
                MinY = MathF.Min(MinY, el.y);
                MaxY = MathF.Max(MaxY, el.y);
            }
            hitBox = new GlobalOrientedHitBox(MinX, MaxX, MinY, MaxY);
        }
        public void GiperBox()
        {
            hitBox = (SpeedVector.x >= 0 && SpeedVector.y >= 0) ? new GlobalOrientedHitBox(hitBox.Left, hitBox.Right + SpeedVector.x, hitBox.Top, hitBox.Bottom + SpeedVector.y) :
                (SpeedVector.x >= 0) ? new GlobalOrientedHitBox(hitBox.Left, hitBox.Right + SpeedVector.x, hitBox.Top - SpeedVector.y, hitBox.Bottom) :
                (SpeedVector.y >= 0) ? new GlobalOrientedHitBox(hitBox.Left - SpeedVector.x, hitBox.Right, hitBox.Top, hitBox.Bottom + SpeedVector.y) :
                new GlobalOrientedHitBox(hitBox.Left - SpeedVector.x, hitBox.Right, hitBox.Top - SpeedVector.y, hitBox.Bottom);
        }
        public void Move(Dot Speed)
        {
            for(int i =0;i<Vertexes.Length;i++) 
            {
                Vertexes[i] += Speed;
            }
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i].Move(Speed);
            }
        }
        public void Draw(Color color)
        {
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i].Draw(color);
            }
        }
        public void DrawVertex(float Size, Color color)
        {
            for (int i = 0; i < Vertexes.Length; i++)
            {
                Vertexes[i].Draw(Size, color);
            }
        }
        public Collider Scaling(Dot Focus, float ScalX)
        {
            Dot[] dots = new Dot[Vertexes.Length];
            Triangle[] triangles = this.triangles;
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i].Scaling(Focus, ScalX);
            }
            for (int i = 0; i < Vertexes.Length; i++)
            {
                dots[i] = Focus + (Vertexes[i] - Focus) * ScalX;
            }
            return new Collider(triangles, dots);
        }
    }
    public class ColTriangle : Collider
    {
        public ColTriangle(Dot V1, Dot V2, Dot V3) : base(new Triangle[] { new Triangle(V1, V2, V3) }, new Dot[] { V1, V2, V3 })
        {
            float MinX = MathF.Min(MathF.Min(V1.x, V2.x), V3.x);
            float MaxX = MathF.Max(MathF.Max(V1.x, V2.x), V3.x);
            float MinY = MathF.Min(MathF.Min(V1.y, V2.y), V3.y);
            float MaxY = MathF.Max(MathF.Max(V1.y, V2.y), V3.y);
            hitBox = new GlobalOrientedHitBox(MinX, MaxX, MinY, MaxY);
        }
    }
    public class GlobalOrientedHitBox
    {
        public float Left { get; }
        public float Right { get; }
        public float Top { get; }
        public float Bottom { get; }
        public float Width { get; }
        public float Height { get; }
        public GlobalOrientedHitBox(float Left, float Right, float Top, float Bottom)
        {
            this.Left = Left;
            this.Right = Right;
            this.Top = Top;
            this.Bottom = Bottom;
            Width = MathF.Abs(Left - Right);
            Height = MathF.Abs(Top - Bottom);
        }
        public override string ToString() => $"{Left}, {Right}, {Bottom}, {Top}";
    }
}
