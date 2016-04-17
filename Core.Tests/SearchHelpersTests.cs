using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tests
{
    [TestClass]
    public class SearchHelpersTests
    {
        
        /* UpperBound tests */
        [TestMethod]
        public void UpperBoundLinear_ArrayLengthIsOne()
        {
            var keys = new long[] { 1 };
            int index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void UpperBoundLinear_ArrayLengthIsTwo()
        {
            var keys = new long[] { 1, 2 };

            int index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(-1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 3);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void UpperBoundLinear_ArrayLengthIsThree()
        {
            var keys = new long[] { 1, 2, 3 };

            int index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(2, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 3);
            Assert.AreEqual(-1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void UpperBoundLinear_ContainsDuplicates()
        {
            var keys = new long[] { 1, 2, 3, 3};

            int index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(2, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 3);
            Assert.AreEqual(-1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void UpperBoundLinear_ContainsDuplicates2()
        {
            var keys = new long[] { 1, 3, 3, 5, 7, 7, 9 };

            int index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 3);
            Assert.AreEqual(3, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 4);
            Assert.AreEqual(3, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 5);
            Assert.AreEqual(4, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 6);
            Assert.AreEqual(4, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 7);
            Assert.AreEqual(6, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 8);
            Assert.AreEqual(6, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 9);
            Assert.AreEqual(-1, index);

            index = SearchHelpers.UpperBoundLinear(keys, keys.Length, 10);
            Assert.AreEqual(-1, index);
        }
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
                var keys = GetSortedRandomKeys(i);
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
                var keys = GetSortedRandomKeys(i);
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
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.UpperBoundLinear8(keys, keys.Length, keys[k]));
                }
            }
        }
        [TestMethod]
        public void UpperBoundLinear8_Random()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GetSortedRandomKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.UpperBoundLinear8(keys, keys.Length, keys[k]));
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k] + 1), SearchHelpers.UpperBoundLinear8(keys, keys.Length, keys[k] + 1));
                    Assert.AreEqual(SearchHelpers.UpperBoundLinear(keys, keys.Length, keys[k] - 1), SearchHelpers.UpperBoundLinear8(keys, keys.Length, keys[k] - 1));
                }
            }
        }
        /* LowerBound tests */
        [TestMethod]
        public void LowerBoundLinear_ArrayLengthIsOne()
        {
            var keys = new long[] { 1 };

            int index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear_ArrayLengthIsTwo()
        {
            var keys = new long[] { 1, 2 };

            int index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 3);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear_ArrayLengthIsThree()
        {
            var keys = new long[] { 1, 2, 3 };

            int index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 3);
            Assert.AreEqual(2, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear_ContainsDuplicates()
        {
            var keys = new long[] { 1, 2, 3, 3 };
            int index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 3);
            Assert.AreEqual(2, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear_ContainsDuplicates2()
        {
            var keys = new long[] { 1, 3, 3, 5, 7, 7, 9 };

            int index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 3);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 4);
            Assert.AreEqual(3, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 5);
            Assert.AreEqual(3, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 6);
            Assert.AreEqual(4, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 7);
            Assert.AreEqual(4, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 8);
            Assert.AreEqual(6, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 9);
            Assert.AreEqual(6, index);

            index = SearchHelpers.LowerBoundLinear(keys, keys.Length, 10);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear8_ArrayLengthIsOne()
        {
            var keys = new long[] { 1 };

            int index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 2);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear8_ArrayLengthIsTwo()
        {
            var keys = new long[] { 1, 2 };

            int index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 3);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear8_ArrayLengthIsThree()
        {
            var keys = new long[] { 1, 2, 3 };

            int index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 3);
            Assert.AreEqual(2, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear8_ContainsDuplicates()
        {
            var keys = new long[] { 1, 2, 3, 3 };
            int index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 3);
            Assert.AreEqual(2, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear8_ContainsDuplicates2()
        {
            var keys = new long[] { 1, 3, 3, 5, 7, 7, 9 };

            int index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 3);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 4);
            Assert.AreEqual(3, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 5);
            Assert.AreEqual(3, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 6);
            Assert.AreEqual(4, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 7);
            Assert.AreEqual(4, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 8);
            Assert.AreEqual(6, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 9);
            Assert.AreEqual(6, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 10);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear8_ArrayLengthIs8_WithoutDuplicates()
        {
            var keys = new long[] { 1, 3, 5, 7, 9, 11, 13, 15 };

            int index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 3);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 4);
            Assert.AreEqual(2, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 5);
            Assert.AreEqual(2, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 6);
            Assert.AreEqual(3, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 7);
            Assert.AreEqual(3, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 8);
            Assert.AreEqual(4, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 9);
            Assert.AreEqual(4, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 10);
            Assert.AreEqual(5, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 11);
            Assert.AreEqual(5, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 12);
            Assert.AreEqual(6, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 13);
            Assert.AreEqual(6, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 14);
            Assert.AreEqual(7, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 15);
            Assert.AreEqual(7, index);

            index = SearchHelpers.LowerBoundLinear8(keys, keys.Length, 16);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundLinear8_Seq()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GenerateKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.LowerBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.LowerBoundLinear8(keys, keys.Length, keys[k]));
                }
            }
        }
        [TestMethod]
        public void LowerBoundLinear8_Random()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GetSortedRandomKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.LowerBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.LowerBoundLinear8(keys, keys.Length, keys[k]));
                    Assert.AreEqual(SearchHelpers.LowerBoundLinear(keys, keys.Length, keys[k] + 1), SearchHelpers.LowerBoundLinear8(keys, keys.Length, keys[k] + 1));
                    Assert.AreEqual(SearchHelpers.LowerBoundLinear(keys, keys.Length, keys[k] - 1), SearchHelpers.LowerBoundLinear8(keys, keys.Length, keys[k] - 1));
                }
            }
        }

        [TestMethod]
        public void LowerBoundBinary_ArrayLengthIsOne()
        {
            var keys = new long[] { 1 };

            int index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 2);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundBinary_ArrayLengthIsTwo()
        {
            var keys = new long[] { 1, 2 };

            int index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 3);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundBinary_ArrayLengthIsThree()
        {
            var keys = new long[] { 1, 2, 3 };

            int index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 3);
            Assert.AreEqual(2, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundBinary_ContainsDuplicates()
        {
            var keys = new long[] { 1, 2, 3, 3 };
            int index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 3);
            Assert.AreEqual(2, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundBinary_ContainsDuplicates2()
        {
            var keys = new long[] { 1, 3, 3, 5, 7, 7, 9 };

            int index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 0);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 1);
            Assert.AreEqual(0, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 2);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 3);
            Assert.AreEqual(1, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 4);
            Assert.AreEqual(3, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 5);
            Assert.AreEqual(3, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 6);
            Assert.AreEqual(4, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 7);
            Assert.AreEqual(4, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 8);
            Assert.AreEqual(6, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 9);
            Assert.AreEqual(6, index);

            index = SearchHelpers.LowerBoundBinary(keys, keys.Length, 10);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void LowerBoundBinary_Seq()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GenerateKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.LowerBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.LowerBoundBinary(keys, keys.Length, keys[k]));
                }
            }
        }
        [TestMethod]
        public void LowerBoundBinary_Random()
        {
            for (int i = 1; i < 1000; i++)
            {
                var keys = GetSortedRandomKeys(i);
                for (int k = 0; k < keys.Length; k++)
                {
                    Assert.AreEqual(SearchHelpers.LowerBoundLinear(keys, keys.Length, keys[k]), SearchHelpers.LowerBoundBinary(keys, keys.Length, keys[k]));
                    Assert.AreEqual(SearchHelpers.LowerBoundLinear(keys, keys.Length, keys[k] + 1), SearchHelpers.LowerBoundBinary(keys, keys.Length, keys[k] + 1));
                    Assert.AreEqual(SearchHelpers.LowerBoundLinear(keys, keys.Length, keys[k] - 1), SearchHelpers.LowerBoundBinary(keys, keys.Length, keys[k] - 1));
                }
            }
        }
        private long[] GetSortedRandomKeys(int number)
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
