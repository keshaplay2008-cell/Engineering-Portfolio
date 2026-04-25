using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhisAxeomes
{
    public static class Functions
    {
        public static void RootOfLaws(LawsCache LC1, LawsCache LC2, out float V1R1, out float V1R2, out float V2R1, out float V2R2)
        {
            float P = LC1.m * LC1.v + LC2.m * LC2.v;
            float E = (LC1.m * LC1.v * LC1.v + LC2.m * LC2.v * LC2.v) / 2;
            float v1r1, v1r2, v2r1, v2r2;
            float ms = LC1.m + LC2.m;

            float a = LC1.m * ms;
            float b2 = -LC1.m * P;
            float c = P * P - 2 * LC2.m * E;
            float SD4 = MathF.Sqrt(b2 * b2 - a * c);

            v1r1 = (-b2 + SD4) / a;
            v1r2 = (-b2 - SD4) / a;

            v2r1 = (P - LC1.m * v1r1) / LC2.m;
            v2r2 = (P - LC1.m * v1r2) / LC2.m;


            V1R1 = v1r1;
            V1R2 = v1r2;
            V2R1 = v2r1;
            V2R2 = v2r2;
        }
    }
}
