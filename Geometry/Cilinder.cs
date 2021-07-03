using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{

    [Serializable]
    public class Cilinder : Circle, IThreeDimensional
    {
        private float _height;
        public float Height
        {
            get => _height;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Non-negative radius required");
                    _height = value;
            }
        }
        public Cilinder(PointF center, float radius, float height) : base (center,radius) { _height = height; }
        public Cilinder(float x, float y, float radius, float height) : this(new PointF(x,y), radius, height) { }
        public Cilinder() : this(0, 0, 1, 1) { }

        public override float Area() => 2 * base.Area() + SideArea();
        public float SideArea() => Length() * _height;
        public float Volume() => base.Area() * _height;
        public override object Clone()
            => new Cilinder(Center, _radius, _height);
        public bool Equals(Cilinder other)
            => Center == other.Center && _radius == other.Radius && other._height == _height;
        public override bool Equals(object obj)
            => (obj is Cilinder cilinder) ? Equals(cilinder) : false;
        public static bool operator ==(Cilinder left, Cilinder right)
            => ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

        public static bool operator !=(Cilinder left, Cilinder right) => !(left == right);

        public static Circle operator +(Cilinder circle, PointF value)
            => circle is null
                ? throw new ArgumentNullException(nameof(circle))
                : new Cilinder(new PointF(circle.Center.X + value.X, circle.Center.Y + value.Y), circle.Radius, circle.Height);
        public static Circle operator *(Cilinder circle, float value)
            => circle is null
                ? throw new ArgumentNullException(nameof(circle))
                : new Cilinder(circle.Center, circle.Radius * value, circle.Height* value);

        public object Deserialize(Stream serializationStream)
        {

            BinaryFormatter formatter = new BinaryFormatter();

            Console.WriteLine("Object has been deserialized");
            return formatter.Deserialize(serializationStream);
        }
        public void Serialize(Stream serializationStream, Circle graph)
        {

            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(serializationStream, graph);

            Console.WriteLine("Object has been serialized");
        }
    }
}
