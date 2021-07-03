using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public interface IOneDimensional
    {
        public PointF Center { get; set; }
        public float Length();
    }
    public interface ITwoDimensional : IOneDimensional
    {
        public float Radius { get; set; }
        public float Area();
    }
    public interface IThreeDimensional : ITwoDimensional
    {
        public float Height { get; set; }
        public float Volume();
    }
}
