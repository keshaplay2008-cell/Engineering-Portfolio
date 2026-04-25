using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry_2D;
using Geometry_3D;

namespace Collision
{
    public static class ColAction
    {
        public static bool CollisBox(GlobalOrientedHitBox GOHB1, GlobalOrientedHitBox GOHB2)
        {
            bool RetX;
            if (GOHB1.Width >= GOHB2.Width)
            {
                RetX = (GOHB1.Left < GOHB2.Left && GOHB2.Left < GOHB1.Right) || (GOHB1.Left < GOHB2.Right && GOHB2.Right < GOHB1.Right);
            }
            else
            {
                RetX = (GOHB2.Left < GOHB1.Left && GOHB1.Left < GOHB2.Right) || (GOHB2.Left < GOHB1.Right && GOHB1.Right < GOHB2.Right);
            }
            bool RetY;
            if (GOHB1.Height >= GOHB2.Height)
            {
                RetY = (GOHB1.Top < GOHB2.Top && GOHB2.Top < GOHB1.Bottom) || (GOHB1.Top < GOHB2.Bottom && GOHB2.Bottom < GOHB1.Bottom);
            }
            else
            {
                RetY = (GOHB2.Top < GOHB1.Top && GOHB1.Top < GOHB2.Bottom) || (GOHB2.Top < GOHB1.Bottom && GOHB1.Bottom < GOHB2.Bottom);
            }
            return RetX && RetY;
        }
        public static GiperDot FOP(Collider C1, Collider C2, out bool Is1t2, out int sideI)
        {
            if (CollisBox(C1.hitBox, C2.hitBox))
            {
                GTriangle GT1 = new GTriangle(C1.triangles[0], C1.SpeedVector);
                GTriangle GT2 = new GTriangle(C2.triangles[0], C2.SpeedVector);
                bool boolout;
                int intout;
                GiperDot RecGD = GiperAction.FOP(GT1, GT2, out Is1t2, out sideI);
                int l = 1;
                for (int i = 0; i < C1.triangles.Length; i++)
                {
                    while (l < C2.triangles.Length)
                    {
                        GT1 = new GTriangle(C1.triangles[i], C1.SpeedVector);
                        GT2 = new GTriangle(C2.triangles[l], C2.SpeedVector);
                        GiperDot FOPI = GiperAction.FOP(GT1, GT2, out boolout, out intout);
                        if (FOPI.t < RecGD.t)
                        {
                            RecGD = FOPI;
                            Is1t2 = boolout;
                            sideI = intout;

                        }
                        //RecGD = GiperAction.Min(RecGD, GiperAction.FOP(GT1, GT2, out Is1t2, out sideI));
                        l++;
                    }
                    l = 0;
                }

                return RecGD;
            }
            else
            {
                Is1t2 = false;
                sideI = -1;
                return new GiperDot(float.NaN, float.NaN, 1);
            }
        }
        public static GiperDot FOPNH(Collider C1, Collider C2, out bool Is1t2, out int sideI)
        {
            GTriangle GT1 = new GTriangle(C1.triangles[0], C1.SpeedVector);
            GTriangle GT2 = new GTriangle(C2.triangles[0], C2.SpeedVector);
            bool boolout;
            int intout;
            GiperDot RecGD = GiperAction.FOP(GT1, GT2, out Is1t2, out sideI);
            int l = 1;
            for (int i = 0; i < C1.triangles.Length; i++)
            {
                while (l < C2.triangles.Length)
                {
                    GT1 = new GTriangle(C1.triangles[i], C1.SpeedVector);
                    GT2 = new GTriangle(C2.triangles[l], C2.SpeedVector);
                    GiperDot FOPI = GiperAction.FOP(GT1, GT2, out boolout, out intout);
                    if (FOPI.t < RecGD.t)
                    {
                        RecGD = FOPI;
                        Is1t2 = boolout;
                        sideI = intout;
                    }
                    //RecGD = GiperAction.Min(RecGD, GiperAction.FOP(GT1, GT2, out Is1t2, out sideI));
                    l++;
                }
                l = 0;
            }
            return RecGD;
        }
        /*public static ColCache[] FOK(Dictionary<int, ColHis> Touchs)
        {
            ColCache[] Ret = new ColCache[Touchs.Count];
            /*for (int i = 0; i < Ret.Length; i++)
            {
                Ret[i] = new ColCache(-1, new ColDot(float.NaN, float.NaN, 1, false, -1));
            }*//*
            bool IsEnd = false;
            while (!IsEnd) 
            {
                IsEnd = true;

                foreach (var el_1 in Touchs)
                {
                    foreach (var el_2 in Touchs)
                    {
                        el_1.Value.Rec();
                        el_2.Value.Rec();
                        if (el_2.Value.RecIndex == el_1.Key && el_1.Value.RecIndex == el_2.Key)
                        {
                            Ret[el_1.Key] = new ColCache(el_2.Key, el_1.Value.TouchDots[el_1.Value.RecIndex]);
                            Ret[el_2.Key] = new ColCache(el_1.Key, el_2.Value.TouchDots[el_2.Value.RecIndex]);
                            foreach (var el3 in Touchs)
                            {
                                el3.Value.Remove(el_1.Key);
                                el3.Value.Remove(el_2.Key);
                                el3.Value.Rec();
                            }
                            Touchs.Remove(el_1.Key);
                            Touchs.Remove(el_2.Key);
                            IsEnd = false;
                        }
                    }
                }
            }
            return Ret;
        }*/
        public static List<ColEvent> FOK(Dictionary<int, ColHis> Touchs)
        {
            List<ColEvent> Ret = new List<ColEvent>();
            /*for (int i = 0; i < Ret.Length; i++)
            {
                Ret[i] = new ColCache(-1, new ColDot(float.NaN, float.NaN, 1, false, -1));
            }*/
            bool IsEnd = false;
            while (!IsEnd) 
            {
                IsEnd = true;

                foreach (var el_1 in Touchs)
                {
                    foreach (var el_2 in Touchs)
                    {
                        el_1.Value.Rec();
                        el_2.Value.Rec();
                        if (el_2.Value.RecIndex == el_1.Key && el_1.Value.RecIndex == el_2.Key)
                        {
                            Ret.Add(new ColEvent(el_1.Key, el_2.Key, Touchs[el_1.Key].TouchDots[el_2.Key]));
                            foreach (var el3 in Touchs)
                            {
                                el3.Value.Remove(el_1.Key);
                                el3.Value.Remove(el_2.Key);
                                el3.Value.Rec();
                            }
                            Touchs.Remove(el_1.Key);
                            Touchs.Remove(el_2.Key);
                            IsEnd = false;
                        }
                    }
                }
            }
            return Ret;
        }
    }
}
