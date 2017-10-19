using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class Appreciation
    {
        public int Id { get; protected set; }

        public string ResourceType { get; protected set; }

        public int Property { get; protected set; }
        public int Capacity { get; protected set; }
        public int Acceleration { get; protected set; }

        public Appreciation(int id, string resouceType, int property, int capacity, int acceleration)
        {
            Id = id;
            ResourceType = resouceType;
            Property = property;
            Capacity = capacity;
            Acceleration = acceleration;
        }

        public static Appreciation NullAppreciation()
        {
            return new Appreciation(0, "None", 0, 0, 0);
        }

        public override string ToString()
        {
            return $"Appreciation {Id} {ResourceType} {Property} {Capacity} {Acceleration}";
        }
    }
}
