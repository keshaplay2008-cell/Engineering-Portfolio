using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry_2D;

namespace Geometry_3D
{
    public class GiperDot
    {
        public float x, y, t;
        public GiperDot(float x, float y, float t)
        {
            this.x = x;
            this.y = y;
            this.t = t;
        }
        public GiperDot(Dot dot, float t)
        {
            this.x = dot.x;
            this.y = dot.y;
            this.t = t;
        }
        public Dot ToDot2D() => new Dot(x, y);
        public override string ToString() => $"{x}, {y}, {t}";
    }
}
