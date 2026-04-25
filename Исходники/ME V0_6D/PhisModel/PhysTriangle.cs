using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collision;
using Geometry_2D;

namespace PhisModel
{
    public class PhysTriangle : ColTriangle
    {
        public float S;
        public float Density;
        public float Masses;
        public PhysTriangle(Dot V1, Dot V2, Dot V3, float Density) : base(V1,V2,V3)
        {
            S = MathF.Abs((V1.x * V2.y + V2.x * V3.y + V3.x * V1.y) - (V1.y * V2.x + V2.y * V3.x + V3.y * V1.x)) / 2;
            this.Density = Density;
            Masses = S * Density;
        }
    }
}
