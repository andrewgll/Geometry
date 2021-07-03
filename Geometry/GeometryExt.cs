using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    static class GeometryExt 
    {
        public static bool AreaIsGreaterThan(this IThreeDimensional thisfigure, IThreeDimensional otherfigure)
        =>thisfigure.Area() > otherfigure.Area();

        public static bool VolumeIsGreaterThan(this IThreeDimensional thisfigure, IThreeDimensional otherfigure)
        =>thisfigure.Volume() > otherfigure.Volume();

        public static bool LengthIsGreaterThan(this ITwoDimensional thisfigure, ITwoDimensional otherfigure)
        => thisfigure.Length() > otherfigure.Length();

        public static float CircleArea(this ITwoDimensional thisfigure)
            => MathF.PI * thisfigure.Radius * thisfigure.Radius;


    }
}
