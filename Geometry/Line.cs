using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{ 
    public abstract class Figure : IOneDimensional
    {
        public abstract PointF Center { get; set; }

        public abstract float Length();

        protected virtual double DistanceToPoint(PointF left, PointF right)
        => Math.Sqrt(Math.Pow(left.X - right.X, 2) + Math.Pow(left.Y - right.Y, 2));
    }
}
