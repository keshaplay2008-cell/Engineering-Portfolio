using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry_3D
{
    public static class GiperAction
    {
        public static bool Belong(GiperDot Int, BP_Plane G, GStraight S) {
            bool BS = 0 < Int.t && Int.t < 1;
            bool BG = G.Shodow.Belong(Int.ToDot2D());
            return BS && BG; 
        }
        public static GiperDot Min(GiperDot d1, GiperDot d2) => (d1.t < d2.t) ? d1 : d2;
        public static GiperDot Intersection(GStraight gStraight, BP_Plane _Plane)
        {
            float T = -(gStraight.nx + gStraight.ny * _Plane.u + gStraight.nt * _Plane.v + _Plane.d) / (gStraight.kx + gStraight.ky * _Plane.u + gStraight.kt * _Plane.v);
            return new GiperDot(gStraight.nx + gStraight.kx * T, gStraight.ny + gStraight.ky * T, gStraight.nt + gStraight.kt * T);
        }
        public static GiperDot Intersection(BP_Plane Plane_1, BP_Plane Plane_2, out bool Is1t2)
        {
            GiperDot[] gs = new GiperDot[2];
            bool[] boolout = new bool[2];
            int i = 0;
            GiperDot Inter;


            Inter = Intersection(Plane_2.G1, Plane_1);
            if(Belong(Inter, Plane_1, Plane_2.G1))
            {
                boolout[i] = false;
                gs[i] = Inter;
                i++;
            }

            Inter = Intersection(Plane_2.G2, Plane_1);
            if (Belong(Inter, Plane_1, Plane_2.G2))
            {
                boolout[i] = false;
                gs[i] = Inter;
                i++;
            }
            if (i == 2)
            {
                Is1t2 = false;
                return Min(gs[0], gs[1]);
            }


            Inter = Intersection(Plane_1.G1, Plane_2);
            if (Belong(Inter, Plane_2, Plane_1.G1))
            {
                boolout[i] = true;
                gs[i] = Inter;
                i++;
            }
            if (i == 2)
            {
                Is1t2 = (gs[0].t < gs[1].t) ? boolout[0] : boolout[1];
                return Min(gs[0], gs[1]);
            }

            Inter = Intersection(Plane_1.G2, Plane_2);
            if (Belong(Inter, Plane_2, Plane_1.G2))
            {
                boolout[i] = true;
                gs[i] = Inter;
                i++;
            }

            if (i == 2)
            {
                Is1t2 = (gs[0].t < gs[1].t) ? boolout[0] : boolout[1];
                return Min(gs[0], gs[1]);
            }
            else if(i == 1)
            {
                Is1t2 = boolout[0];
                return gs[0];
            }
            else
            {
                Is1t2 = false;
                return new GiperDot(float.NaN, float.NaN, 1);
            }
        }
        public static GiperDot FOP(GTriangle GT1, GTriangle GT2, out bool Is1t2, out int sideI)
        {
            bool boolout;
            GiperDot RecGD = Intersection(GT1.BPSs[0], GT2.BPSs[0], out boolout);
            Is1t2 = boolout;
            sideI = 0;
            GiperDot Inter = RecGD;
            
            sideI = 0;
            int l = 1;
            for (int i = 0; i < 3; i++)
            {
                while (l < 3)
                {
                    
                    Inter = Intersection(GT1.BPSs[i], GT2.BPSs[l], out boolout);
                    if (Inter.t < RecGD.t)
                    {
                        RecGD = Inter;
                        Is1t2 = boolout;
                        sideI = (Is1t2) ? l : i;
                    }
                    //RecGD = Min(RecGD, Intersection(GT1.BPSs[i], GT2.BPSs[l], out Is1t2));
                    l++;
                }
                l = 0;
            }

            return RecGD;
        }
    }
}
