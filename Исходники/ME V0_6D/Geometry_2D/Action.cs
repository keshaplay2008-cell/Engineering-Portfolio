using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry_2D
{
    public static class Action
    {
        public static Dot Intersection(Straight S1, Straight S2)
        {
            if(float.IsInfinity(S1.k) || float.IsInfinity(S2.k))
            {
                if(float.IsInfinity(S1.k) && float.IsInfinity(S2.k))
                {
                    return new Dot(float.NaN, float.NaN);
                }
                else
                {
                    if (float.IsInfinity(S1.k))
                    {
                        return new Dot(S1.n, S1.n * S2.k + S2.n);
                    }
                    else
                    {
                        return new Dot(S2.n, S2.n * S1.k + S1.n);
                    }
                }
            }
            else
            {
                if (S1.k == S2.k)
                {
                    return new Dot(0, float.NaN);
                }
                else
                {
                    float x = -(S2.n - S1.n) / (S2.k - S1.k);
                    return new Dot(x, S1.n + S1.k * x);
                }
            }
            
            
        }
    }
}
