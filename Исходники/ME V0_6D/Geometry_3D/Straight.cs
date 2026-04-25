using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry_3D
{
    public class GStraight
    {
        public float kx { get; }
        public float ky { get; }
        public float kt { get; }
        public float nx { get; }
        public float ny { get; }
        public float nt { get; }
        public GStraight(float kx, float ky, float kt, float nx, float ny, float nt)
        {
            this.kx = kx;
            this.ky = ky;
            this.kt = kt;
            this.nx = nx;
            this.ny = ny;
            this.nt = nt;
        }
        public GStraight(GiperDot GD1, GiperDot GD2)
        {
            nx = GD1.x;
            ny = GD1.y;
            nt = GD1.t;
            kx = GD2.x - GD1.x;
            ky = GD2.y - GD1.y;
            kt = GD2.t - GD1.t;
        }
        public bool Belong(GiperDot D)
        {
            if (kx != 0 || ky !=0)
            {
                float T;
                if (kx < ky)
                {
                    T = (D.x - nx) / kx;
                }
                else
                {
                    T = (D.y - ny) / ny;
                }
                return 0 <= T && T <= 1;
            }
            else
            {
                float T = (D.t - nt)/kt;
                return 0 <= T && T <= 1;
            }
        }
    }
}
