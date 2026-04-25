using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry_2D;
using Action = Geometry_2D.Action;

namespace Geometry_3D
{
    public class BP_Plane
    {
        public float u { get; }
        public float v { get; }
        public float d { get; }

        public GStraight G1 { get; }
        public GStraight G2 { get; }
        public Project_Plane Shodow { get; }
        public BP_Plane(float u, float v, float d)
        {
            this.u = u;
            this.v = v;
            this.d = d;
        }
        public BP_Plane(Dot Basis, Dot BasisVector, Dot SpeedVector)
        {
            u = -BasisVector.x / BasisVector.y;
            d = -(Basis.x + Basis.y * u);
            v = -(SpeedVector.x + SpeedVector.y * u);
            Shodow = new Project_Plane(new Line(Basis, Basis + BasisVector), SpeedVector);
            G1 = new GStraight(new GiperDot(Basis, 0), new GiperDot(Basis + SpeedVector, 1));
            G2 = new GStraight(new GiperDot(Basis + BasisVector, 0), new GiperDot(Basis + BasisVector + SpeedVector, 1));
        }
    }
    public class Project_Plane
    {
        public Line BasisNow { get; }
        public Line BasisFut { get; }
        public Line Edge1 { get; }
        public Line Edge2 { get; }
        public Easy_Project_BP Project_Exception;
        public bool IsException;
        public Project_Plane(Line line, Dot SpeedVector)
        {
            if(SpeedVector != new Dot(0,0))
            {
                Dot DL = line.End - line.Beg;
                float cos = (SpeedVector * DL) / (SpeedVector.GetLong() * DL.GetLong());
                if (cos == 1 || cos == -1)
                {
                    IsException = true;
                    Project_Exception = new Easy_Project_BP(line, SpeedVector, cos == 1);
                }
                else
                {
                    IsException = false;
                    BasisNow = line;
                    BasisFut = new Line(line.Beg + SpeedVector, line.End + SpeedVector);
                    Edge1 = new Line(line.Beg, line.Beg + SpeedVector);
                    Edge2 = new Line(line.End, line.End + SpeedVector);
                }
            }
            else
            {
                IsException = true;
                Project_Exception = new Easy_Project_BP(line, SpeedVector, true);
            }
            
        }
        public bool Belong(Dot dot)
        {
            if (!IsException)
            {
                Straight straight = new Straight(BasisNow.k, dot.y - dot.x * BasisNow.k);
                Dot Int1 = Action.Intersection(straight, Edge1);
                if (Edge1.Belong(Int1))
                {
                    Dot Int2 = Action.Intersection(straight, Edge2);
                    Line line = new Line(Int1, Int2);
                    return line.Belong(dot);
                }
                else return false;
            }
            else
            {
                return Project_Exception.Belong(dot);
            }
        }
    }
    public class Easy_Project_BP
    {
        public Line TrackLine { get; }
        public Easy_Project_BP(Line line, Dot SpeedVector, bool IsForward)
        {
            if (IsForward)
            {
                TrackLine = new Line(line.Beg, line.End + SpeedVector);   
            }
            else
            {
                TrackLine = new Line(line.Beg - SpeedVector, line.End);
            }
        }
        public bool Belong(Dot dot)
        {
            return TrackLine.Belong(dot);
        }
    }
}
