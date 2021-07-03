using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Geometry
{
    public enum Intersect
    {
        NotIntersect,
        OnePoint,
        TwoPoint,
        Same
    }

 

    [Serializable]
    public class Circle : Figure, IComparable<Circle>, IEquatable<Circle>, ICloneable, ITwoDimensional
    {
        #region FieldsAndProperties
        
        protected float _radius;
        public override PointF Center { get; set; }

        public bool CompletelyInSameQuarter
        {
            get => _radius <= MathF.Abs(Center.X) && _radius <= MathF.Abs(Center.Y);
        }

        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value > 0 ? _radius = value : throw new ArgumentException("Non-negative radius required");
            }

        }
        #endregion

        #region Constructors
        public Circle(PointF center, float radius)
        {
            (Center, Radius) = (center, radius);
        }
        public Circle(float x, float y, float radius) : this
            (new PointF(x, y), radius)
        { }
        
        public Circle() : this(0, 0, 1) { }
        #endregion

        #region Methods
        public int CompareTo(Circle others)
        {
            var distance = CenterDistance(this, others);
            if (distance < Radius - others?.Radius)
                return 1;
            if (distance < others?.Radius - Radius)
                return -1;
            return 0;
        }

        public virtual float Area()
            => MathF.PI * Radius * Radius;

        public override float Length()
            => 2 * MathF.PI * Radius;
        public Intersect IntersectWith(Circle other)
        {
            var distance = DistanceToPoint(Center, other.Center);
            if (distance == 0 && Radius == other.Radius)
                return Intersect.Same;

            if (distance > Radius + other.Radius || distance < Math.Abs(Radius - other.Radius))
                return Intersect.NotIntersect;

            if ((distance == Radius + other.Radius) || (distance == Math.Abs(Radius - other.Radius)))
                return Intersect.OnePoint;

            return Intersect.TwoPoint;

        }

        public virtual bool Contains(PointF point) =>
            Math.Pow(Center.X - point.X, 2) + Math.Pow(Center.Y - point.Y, 2) <= _radius * _radius;

        public static float CenterDistance(Circle circle1, Circle circle2) =>
            circle1 is not null && circle2 is not null
                ? MathF.Sqrt((circle1.Center.X - circle2.Center.X) * (circle1.Center.X - circle2.Center.X) +
                             (circle1.Center.Y - circle2.Center.Y) * (circle1.Center.Y - circle2.Center.Y))
                : float.NaN;

        public virtual object Clone()
             => new Circle(Center, _radius);
        

        public virtual bool Equals(Circle other)
            => Center == other?.Center && _radius == other.Radius;
        public override bool Equals(object obj)
            => (obj is Circle circle) ? Equals(circle) : false;
        #endregion

        #region Operators
        public static bool operator == (Circle left, Circle right)
            => ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

        public static bool operator !=(Circle left, Circle right) => !(left == right);

        public static Circle operator +(Circle circle, PointF value)
            => circle is null
                ? throw new ArgumentNullException(nameof(circle))
                : new Circle(new PointF(circle.Center.X + value.X, circle.Center.Y + value.Y), circle.Radius);
        public static Circle operator *(Circle circle, float value)
            => circle is null
                ? throw new ArgumentNullException(nameof(circle))
                : new Circle(circle.Center, circle.Radius * value);

        #endregion

        #region Serialization
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
        #endregion

    }
}
