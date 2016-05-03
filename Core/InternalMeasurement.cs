using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal struct InternalMeasurement
    {
        public long Time;
        public double Value;
        public int Tag;
        public int User;

        public static InternalMeasurement From(Measurement m)
        {
            return new InternalMeasurement() { Time = m.Time, Value = m.Value, Tag = m.Tag, User = m.User };
        }
    }
}
