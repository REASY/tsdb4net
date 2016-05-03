using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public struct Measurement
    {
        public long Id;
        public long Time;
        public double Value;
        public int Tag;
        public int User;

        public static Measurement New(long id, long time, double value, int tag, int user)
        {
            return new Measurement() { Id = id, Time = time, Value = value, Tag = tag, User = user };
        }
    }
}
