using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry_2D;
using Geometry_3D;

namespace Collision
{
    public class ColEvent
    {
        public int i { get; }
        public int l { get; }
        public ColDot TouchDot { get; }
        public ColEvent(int i, int l, ColDot TouchDot)
        {
            this.i = i;
            this.l = l;
            this.TouchDot = TouchDot;
        }
    }
    public class ColCache
    {
        public int index { get; }
        public ColDot TouchDot { get; set; }


        public ColCache(int index, ColDot TouchDot)
        {
            this.index = index;
            this.TouchDot = TouchDot;
        }
    }
    public class ColDot : GiperDot
    {
        public bool Is1t2;
        public int sidsI;
        public ColDot(float x, float y, float t, bool Is1t2, int sidsI):base(x,y,t)
        {
            this.Is1t2 = Is1t2;
            this.sidsI = sidsI;
        }
    }
    public class ColHis
    {
        public int RecIndex { get; set; }
        public Dictionary<int, ColDot> TouchDots { get; }
        public ColHis()
        {
            TouchDots = new Dictionary<int, ColDot>();
            RecIndex = -1;
        }
        public void Add(int Index, ColDot TouchDot) { TouchDots.Add(Index, TouchDot); RecIndex = Index; }
        public void Remove(int Index) {  TouchDots.Remove(Index); }
        public void Clear() { TouchDots.Clear(); }
        public void Rec()
        {
            if (TouchDots.Count != 0)
            {
                foreach (var el in TouchDots)
                {
                    try{
                        if (el.Value.t < TouchDots[RecIndex].t)
                            RecIndex = el.Key;
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
