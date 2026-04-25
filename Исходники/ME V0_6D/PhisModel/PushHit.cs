using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry_2D;
using Geometry_3D;
using Collision;
using PhisAxeomes;

namespace PhisModel
{
    public static class PushHit
    {
        public static void PushHitFunction(PhysTriangle T1, PhysTriangle T2, out Dot RetSpeed1, out Dot RetSpeed2)
        {
            PhysTriangle PushEr;
            PhysTriangle PushSelfEr;
            
            if (T1.ColCache.TouchDot.Is1t2)
            {
                PushEr = T1;
                PushSelfEr = T2;
            }
            else
            {
                PushEr = T2;
                PushSelfEr = T1;
            }
            //Dot DeltaSpeed = PushEr.SpeedVector - PushSelfEr.SpeedVector;

            Line Side = PushSelfEr.triangles[0].Sids[PushEr.ColCache.TouchDot.sidsI];
            Dot Delta = Side.End - Side.Beg;
            int Chetvert = (Delta.x >= 0 && Delta.y >= 0) ? 1 : (Delta.x >= 0) ? 2 : (Delta.y >= 0) ? 3 : 4;
            Chetvert++;
            float k = Delta.y / Delta.x;
            float u = (Chetvert == 5 || Chetvert == 3) ? -MathF.Sqrt((1 + k * k) / (1 + 1 / (k * k))) * Delta.x : MathF.Sqrt((1 + k * k) / (1 + 1 / (k * k))) * Delta.x;
            float v = -u / k;
            Dot Normal = new Dot(u, v);
            Normal /= -Normal.GetLong();
            float Speed1 = PushEr.SpeedVector * Normal;
            float Speed2 = PushSelfEr.SpeedVector * Normal;

            Dot SlipSpeed1 = PushEr.SpeedVector - Normal * Speed1;
            Dot SlipSpeed2 = PushSelfEr.SpeedVector - Normal * Speed2;

            Functions.RootOfLaws(new LawsCache(Speed1, PushEr.Masses), new LawsCache(Speed2, PushSelfEr.Masses),
                out float v1r1, out float v1r2, out float v2r1, out float v2r2
                );
            float x1 = (PushEr.center - PushEr.ColCache.TouchDot.ToDot2D()) * Normal;
            float x2 = (PushSelfEr.center - PushSelfEr.ColCache.TouchDot.ToDot2D()) * Normal;

            float dV1 = v1r1 - v2r1;
            float dV2 = v1r2 - v2r2;
            float dX = x1 - x2;

            float q1 = MathF.Abs(dV1 + dX);
            float q2 = MathF.Abs(dV2 + dX);

            float V1, V2;
            if (q1 <= q2)
            {
                V1 = v1r2; V2 = v2r2;
            }
            else
            {
                V1 = v1r1; V2 = v2r1;
            }
            Dot VecSpeed1 = Normal * V1 + SlipSpeed1; 
            Dot VecSpeed2 = Normal * V2 + SlipSpeed2;

            if (T1.ColCache.TouchDot.Is1t2)
            {
                RetSpeed1 = VecSpeed1;
                RetSpeed2 = VecSpeed2;
            }
            else
            {
                RetSpeed1 = VecSpeed2;
                RetSpeed2 = VecSpeed1;
            }
        }
    }
}
