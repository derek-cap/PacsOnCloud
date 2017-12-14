using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector Orientation(Point targetPosition)
        {
            Vector vector = new Vector(targetPosition.X - X, targetPosition.Y - Y, targetPosition.Z - Z);
            vector.Unit();
            return vector;
        }

        public double Distance(Point other)
        {
            return Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y) + (Z - other.Z) * (Z - other.Z));
        }

        public static double Distance(Point one, Point other)
        {
            return one.Distance(other);
        }

        public static Point operator + (Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Point operator + (Point p1, Vector v1)
        {
            return new Point(p1.X + v1.X * v1.Length, p1.Y + v1.Y * v1.Length, p1.Z + v1.Z * v1.Length);
        }

        public override string ToString()
        {
            return "{" + $"{X}, {Y}, {Z}" + "}";
        }
    }

    public struct Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Length { get; set; }

        //public Vector(double x, double y, double z)
        //{
        //    X = x;
        //    Y = y;
        //    Z = z;
        //    Length = 1;
        //}

        public Vector(double x, double y, double z, double length = 1)
        {
            X = x;
            Y = y;
            Z = z;
            Length = length;
        }

        public void Unit()
        {
            double d = Math.Sqrt(X * X + Y * Y + Z * Z);
            X = X / d;
            Y = Y / d;
            Z = Z / d;
        }
    }
}
