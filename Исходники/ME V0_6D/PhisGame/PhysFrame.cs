using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collision;
using Geometry_2D;
using Geometry_3D;
using PhisModel;

namespace PhisGame
{
    public class PhysFrame
    {
        public static Dictionary<int,ColHis> ColHises = new Dictionary<int, ColHis>();
        public static List<ColEvent> Collisions;
        public static Collider[] Colliders = new Collider[0];
        public PhysFrame(Collider[] colliders)
        {
            Colliders = colliders;
            Collisions = new List<ColEvent>();
            ColHises = new Dictionary<int, ColHis>();
            for (int i = 0; i < colliders.Length; i++) { ColHises.Add(i, new ColHis());}
        }
        /*public void OnPhysFrame(float DeltaTime)
        {
            for (int i = 0; i < Colliders.Length; i++)
                Colliders[i].ReHit();
            for (int i = 0; i < Colliders.Length - 1; i++)
            {
                for (int l = i + 1; l < Colliders.Length; l++)
                {
                    GiperDot GI = ColAction.FOPNH(Colliders[i], Colliders[l], out bool Is1t2, out int sideI);
                    ColHises[i].Add(l, new ColDot(GI.x, GI.y, GI.t, Is1t2, sideI));
                    ColHises[l].Add(i, new ColDot(GI.x, GI.y, GI.t, Is1t2, sideI));
                }
            }
            Collisions = ColAction.FOK(ColHises);
            for (int i = 0; i < Collisions.Length; i++)
            {
                
                int l = Collisions[i].index;
                Dot Speed = Colliders[i].SpeedVector;
                Dot Speed2 = Colliders[l].SpeedVector;
                PhysTriangle T1 = new PhysTriangle(Colliders[i].triangles[0].Vertex[0], Colliders[i].triangles[0].Vertex[1], Colliders[i].triangles[0].Vertex[2], 1f);
                PhysTriangle T2 = new PhysTriangle(Colliders[l].triangles[0].Vertex[0], Colliders[l].triangles[0].Vertex[1], Colliders[l].triangles[0].Vertex[2], 1f);



                T1.SpeedVector = Speed;
                T2.SpeedVector = Speed2;

                GiperDot GI = ColAction.FOP(T1, T2, out bool Is1t2, out int sideI);


                Colliders[i].Move(Speed * Collisions[i].TouchDot.t);
                Colliders[l].Move(Speed2 * Collisions[l].TouchDot.t);
                if (!float.IsNaN(GI.x))
                {
                    T1.ColCache = Collisions[i];
                    T2.ColCache = Collisions[l];
                    PushHit.PushHitFunction(T1, T2, out Speed, out Speed2);
                    Colliders[i].SpeedVector = Speed;
                }
                if (T1.hitBox.Left <= -1) T1.SpeedVector.x = MathF.Abs(T1.SpeedVector.x);
                if (T1.hitBox.Right >= 1) T1.SpeedVector.x = -MathF.Abs(T1.SpeedVector.x);
                if (T1.hitBox.Top <= -1) T1.SpeedVector.y = MathF.Abs(T1.SpeedVector.y);
                if (T1.hitBox.Bottom >= 1) T1.SpeedVector.y = -MathF.Abs(T1.SpeedVector.y);
            }
        }*/
        public void OnPhysFrame(float DeltaTime)
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
                Colliders[i].SpeedVector*=DeltaTime;
                Colliders[i].ReHit();
                Colliders[i].GiperBox();
            }
                
            for (int i = 0; i < Colliders.Length - 1; i++)
            {
                for (int l = i + 1; l < Colliders.Length; l++)
                {
                    GiperDot GI = ColAction.FOP(Colliders[i], Colliders[l], out bool Is1t2, out int sideI);
                    if (float.IsNormal(GI.x))
                    {
                        ColHises[i].Add(l, new ColDot(GI.x, GI.y, GI.t, Is1t2, sideI));
                        ColHises[l].Add(i, new ColDot(GI.x, GI.y, GI.t, !Is1t2, sideI));
                    }
                }
            }
            Collisions = ColAction.FOK(ColHises);
            foreach(var el in Collisions)
            {
                Dot Speed = Colliders[el.i].SpeedVector;
                Dot Speed2 = Colliders[el.l].SpeedVector;
                var T1 = new PhysTriangle(Colliders[el.i].triangles[0].Vertex[0], Colliders[el.i].triangles[0].Vertex[1], Colliders[el.i].triangles[0].Vertex[2], 1f);
                var T2 = new PhysTriangle(Colliders[el.l].triangles[0].Vertex[0], Colliders[el.l].triangles[0].Vertex[1], Colliders[el.l].triangles[0].Vertex[2], 1f);

                T1.SpeedVector = Speed;
                T2.SpeedVector = Speed2;

                Colliders[el.i].Move(Speed * el.TouchDot.t);
                Colliders[el.l].Move(Speed2 * el.TouchDot.t);


                if (!float.IsNaN(el.TouchDot.x))
                {
                    /*T1.ColCache = new ColCache(1, el.TouchDot);
                    T2.ColCache = new ColCache(0, el.TouchDot);
                    PushHit.PushHitFunction(T1, T2, out Speed, out Speed2);*/
                    T1.SpeedVector = Speed * (-1);
                    T2.SpeedVector = Speed2 * (-1);
                }
                if (T1.hitBox.Left <= -1) T1.SpeedVector.x = MathF.Abs(T1.SpeedVector.x);
                if (T1.hitBox.Right >= 1) T1.SpeedVector.x = -MathF.Abs(T1.SpeedVector.x);
                if (T1.hitBox.Top <= -1) T1.SpeedVector.y = MathF.Abs(T1.SpeedVector.y);
                if (T1.hitBox.Bottom >= 1) T1.SpeedVector.y = -MathF.Abs(T1.SpeedVector.y);


                if (T2.hitBox.Left <= -1) T2.SpeedVector.x = MathF.Abs(T2.SpeedVector.x);
                if (T2.hitBox.Right >= 1) T2.SpeedVector.x = -MathF.Abs(T2.SpeedVector.x);
                if (T2.hitBox.Top <= -1) T2.SpeedVector.y = MathF.Abs(T2.SpeedVector.y);
                if (T2.hitBox.Bottom >= 1) T2.SpeedVector.y = -MathF.Abs(T2.SpeedVector.y);
                Colliders[el.i].SpeedVector = T1.SpeedVector;
                Colliders[el.l].SpeedVector = T2.SpeedVector;
                Colliders[el.i].IsUs = true;
                Colliders[el.l].IsUs = true;
            }
            for (int i = 0; i < Colliders.Length; i++)
            {
                if (!Colliders[i].IsUs)
                {
                    Colliders[i].Move(Colliders[i].SpeedVector);

                    Colliders[i].ReHit();
                    Colliders[i].GiperBox();

                    if (Colliders[i].hitBox.Left <= -1) Colliders[i].SpeedVector.x = MathF.Abs(Colliders[i].SpeedVector.x);
                    if (Colliders[i].hitBox.Right >= 1) Colliders[i].SpeedVector.x = -MathF.Abs(Colliders[i].SpeedVector.x);
                    if (Colliders[i].hitBox.Top <= -1) Colliders[i].SpeedVector.y = MathF.Abs(Colliders[i].SpeedVector.y);
                    if (Colliders[i].hitBox.Bottom >= 1) Colliders[i].SpeedVector.y = -MathF.Abs(Colliders[i].SpeedVector.y);
                }
                Colliders[i].SpeedVector /= DeltaTime;
            }
        }
    }
}
