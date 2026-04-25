using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry_2D;
using OpenTK.Graphics.OpenGL;

namespace Geometry_3D
{
    public class GTriangle
    {
        public BP_Plane[] BPSs { get; } = new BP_Plane[3];
        public GTriangle(Triangle triangle, Dot SpeedVetor)
        {
            BPSs[0] = new BP_Plane(triangle.Vertex[0], triangle.Vertex[1] - triangle.Vertex[0], SpeedVetor);
            BPSs[1] = new BP_Plane(triangle.Vertex[1], triangle.Vertex[2] - triangle.Vertex[1], SpeedVetor);
            BPSs[2] = new BP_Plane(triangle.Vertex[2], triangle.Vertex[0] - triangle.Vertex[2], SpeedVetor);
        }
    }
}
