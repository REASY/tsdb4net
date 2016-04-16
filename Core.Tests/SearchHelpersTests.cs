using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tests
{
    [TestClass]
    public class SearchHelpersTests
    {
        [TestMethod]
        public void UpperBoundBinary_Seq()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GenerateKeys(i);
                for(int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.UpperBoundBinary(keys, keys.Length, keys[k]));
                }
            }
        }
        [TestMethod]
        public void UpperBoundBinary_Random()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GetRandomKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.UpperBoundBinary(keys, keys.Length, keys[k]));
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k] + 1), SearchHelpers.UpperBoundBinary(keys, keys.Length, keys[k] + 1));
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k] - 1), SearchHelpers.UpperBoundBinary(keys, keys.Length, keys[k] - 1));
                }
            }
        }
        [TestMethod]
        public void UpperBoundLinear4_Seq()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GenerateKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.UpperBoundLinear4(keys, keys.Length, keys[k]));
                }
            }
        }
        [TestMethod]
        public void UpperBoundLinear4_Random()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GetRandomKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.UpperBoundLinear4(keys, keys.Length, keys[k]));
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k] + 1), SearchHelpers.UpperBoundLinear4(keys, keys.Length, keys[k] + 1));
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k] - 1), SearchHelpers.UpperBoundLinear4(keys, keys.Length, keys[k] - 1));
                }
            }
        }
        [TestMethod]
        public void UpperBoundLinear8_Seq()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GenerateKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.UpperBoundLinear4(keys, keys.Length, keys[k]));
                }
            }
        }
        [TestMethod]
        public void UpperBoundLinear8_Random()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GetRandomKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.UpperBoundLinear8(keys, keys.Length, keys[k]));
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k] + 1), SearchHelpers.UpperBoundLinear8(keys, keys.Length, keys[k] + 1));
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k] - 1), SearchHelpers.UpperBoundLinear8(keys, keys.Length, keys[k] - 1));
                }
            }
        }
        private long[] GetRandomKeys(int number)
        {
            Random rnd = new Random(Environment.TickCount);

            HashSet<long> hs = new HashSet<long>();
            for (int i = 1; i <= number; i++)
            {
                int value = rnd.Next();
                while (hs.Contains(value))
                    value = rnd.Next();
                hs.Add(value);
            }
            var keys = hs.ToArray();
            Array.Sort(keys);
            return keys;
        }
        private long[] GenerateKeys(int count)
        {
            long[] result = new long[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = i;
            }
            return result;
        }
    }
}
