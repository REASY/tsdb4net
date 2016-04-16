using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tests
{
    [TestClass]
    public class HelpersTests
    {
        [TestMethod]
        public void SplitLeaf_MaxDegree3_InsertingAt0Position()
        {
            int[] keys = new int[] { 7, 10 };
            int[] values = new int[] { 1, 2};
            int leftKeyIndex = 0;

            int rightKeyIndex = 0;
            int[] rightKeys = null;
            int[] rightValues = null;
            int midElement = 0;

            Helpers.SplitLeaf(keys, values, 0, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);

            Assert.AreEqual(7, midElement);
            Assert.AreEqual(-1, leftKeyIndex);
            Assert.AreEqual(0, keys[0]);
            Assert.AreEqual(0, keys[1]);
            Assert.AreEqual(0, values[0]);
            Assert.AreEqual(0, values[1]);

            Assert.AreEqual(1, rightKeyIndex);
            Assert.AreEqual(7, rightKeys[0]);
            Assert.AreEqual(10, rightKeys[1]);
            Assert.AreEqual(1, rightValues[0]);
            Assert.AreEqual(2, rightValues[1]);
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree3_InsertingAt1Position()
        {
            int[] keys = new int[] { 7, 10 };
            int[] values = new int[] { 1, 2 };
            int leftKeyIndex = 0;

            int rightKeyIndex = 0;
            int[] rightKeys = null;
            int[] rightValues = null;
            int midElement = 0;

            Helpers.SplitLeaf(keys, values, 8, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);

            Assert.AreEqual(8, midElement);
            Assert.AreEqual(0, leftKeyIndex);
            Assert.AreEqual(7, keys[0]);
            Assert.AreEqual(0, keys[1]);
            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(0, values[1]);

            Assert.AreEqual(0, rightKeyIndex);
            Assert.AreEqual(10, rightKeys[0]);
            Assert.AreEqual(2, rightValues[0]);
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree3_InsertingAt2Position()
        {
            int[] keys = new int[] { 7, 10 };
            int[] values = new int[] { 1, 2 };
            int leftKeyIndex = 0;

            int rightKeyIndex = 0;
            int[] rightKeys = null;
            int[] rightValues = null;
            int midElement = 0;

            Helpers.SplitLeaf(keys, values, 11, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);

            Assert.AreEqual(10, midElement);
            Assert.AreEqual(0, leftKeyIndex);
            Assert.AreEqual(7, keys[0]);
            Assert.AreEqual(0, keys[1]);
            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(0, values[1]);

            Assert.AreEqual(0, rightKeyIndex);
            Assert.AreEqual(10, rightKeys[0]);
            Assert.AreEqual(2, rightValues[0]);
        }

        [TestMethod]
        public void SplitLeaf_MaxDegree4_InsertingAt0Position()
        {
            int[] keys = new int[] { 7, 10, 13 };
            int[] values = new int[] { 1, 2, 3 };
            int leftKeyIndex = 0;

            int rightKeyIndex = 0;
            int[] rightKeys = null;
            int[] rightValues = null;
            int midElement = 0;

            Helpers.SplitLeaf(keys, values, 0, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);

            Assert.AreEqual(7, midElement);
            Assert.AreEqual(-1, leftKeyIndex);
            Assert.AreEqual(0, keys[0]);
            Assert.AreEqual(0, keys[1]);
            Assert.AreEqual(0, keys[2]);
            Assert.AreEqual(0, values[0]);
            Assert.AreEqual(0, values[1]);
            Assert.AreEqual(0, values[2]);

            Assert.AreEqual(2, rightKeyIndex);
            Assert.AreEqual(7, rightKeys[0]);
            Assert.AreEqual(10, rightKeys[1]);
            Assert.AreEqual(13, rightKeys[2]);
            Assert.AreEqual(1, rightValues[0]);
            Assert.AreEqual(2, rightValues[1]);
            Assert.AreEqual(3, rightValues[2]);
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree4_InsertingAt1Position()
        {
            int[] keys = new int[] { 7, 10, 13 };
            int[] values = new int[] { 1, 2, 3 };
            int leftKeyIndex = 0;

            int rightKeyIndex = 0;
            int[] rightKeys = null;
            int[] rightValues = null;
            int midElement = 0;

            Helpers.SplitLeaf(keys, values, 8, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);

            Assert.AreEqual(8, midElement);
            Assert.AreEqual(0, leftKeyIndex);
            Assert.AreEqual(7, keys[0]);
            Assert.AreEqual(0, keys[1]);
            Assert.AreEqual(0, keys[2]);
            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(0, values[1]);
            Assert.AreEqual(0, values[2]);

            Assert.AreEqual(1, rightKeyIndex);
            Assert.AreEqual(10, rightKeys[0]);
            Assert.AreEqual(13, rightKeys[1]);
            Assert.AreEqual(0, rightKeys[2]);
            Assert.AreEqual(2, rightValues[0]);
            Assert.AreEqual(3, rightValues[1]);
            Assert.AreEqual(0, rightValues[2]);
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree4_InsertingAt2Position()
        {
            int[] keys = new int[] { 7, 10, 13 };
            int[] values = new int[] { 1, 2, 3 };
            int leftKeyIndex = 0;

            int rightKeyIndex = 0;
            int[] rightKeys = null;
            int[] rightValues = null;
            int midElement = 0;

            Helpers.SplitLeaf(keys, values, 11, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);

            Assert.AreEqual(10, midElement);
            Assert.AreEqual(0, leftKeyIndex);
            Assert.AreEqual(7, keys[0]);
            Assert.AreEqual(0, keys[1]);
            Assert.AreEqual(0, keys[2]);
            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(0, values[1]);
            Assert.AreEqual(0, values[2]);

            Assert.AreEqual(1, rightKeyIndex);
            Assert.AreEqual(10, rightKeys[0]);
            Assert.AreEqual(13, rightKeys[1]);
            Assert.AreEqual(0, rightKeys[2]);
            Assert.AreEqual(2, rightValues[0]);
            Assert.AreEqual(3, rightValues[1]);
            Assert.AreEqual(0, rightValues[2]);
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree4_InsertingAt3Position()
        {
            int[] keys = new int[] { 7, 10, 13 };
            int[] values = new int[] { 1, 2, 3 };
            int leftKeyIndex = 0;

            int rightKeyIndex = 0;
            int[] rightKeys = null;
            int[] rightValues = null;
            int midElement = 0;

            Helpers.SplitLeaf(keys, values, 14, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);

            Assert.AreEqual(10, midElement);
            Assert.AreEqual(0, leftKeyIndex);
            Assert.AreEqual(7, keys[0]);
            Assert.AreEqual(0, keys[1]);
            Assert.AreEqual(0, keys[2]);
            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(0, values[1]);
            Assert.AreEqual(0, values[2]);

            Assert.AreEqual(1, rightKeyIndex);
            Assert.AreEqual(10, rightKeys[0]);
            Assert.AreEqual(13, rightKeys[1]);
            Assert.AreEqual(0, rightKeys[2]);
            Assert.AreEqual(2, rightValues[0]);
            Assert.AreEqual(3, rightValues[1]);
            Assert.AreEqual(0, rightValues[2]);
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree5()
        {
            for (int insertKey = 0; insertKey < 10; insertKey++)
            {
                int[] keys = new int[] { 1, 2, 3, 4 };
                int[] values = new int[] { 1, 2, 3, 4 };
                int leftKeyIndex = 0;

                int rightKeyIndex = 0;
                int[] rightKeys = null;
                int[] rightValues = null;
                int midElement = 0;

                var realResult = new List<int>(keys);
                realResult.Add(insertKey);
                realResult.Sort();
                int mid = (int)Math.Ceiling((float)(keys.Length - 1) / 2);
                int realMid = realResult.ElementAt(mid);


                Helpers.SplitLeaf(keys, values, insertKey, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);
                Assert.AreEqual(leftKeyIndex + 1 + rightKeyIndex + 1, keys.Length);
                Assert.AreEqual(realMid, midElement);
                var afterSplit = new List<int>();
                afterSplit.Add(insertKey);
                for (int i = 0; i <= leftKeyIndex; i++)
                    afterSplit.Add(keys[i]);
                for (int i = 0; i <= rightKeyIndex; i++)
                    afterSplit.Add(rightKeys[i]);
                afterSplit.Sort();
                Assert.AreEqual(0, realResult.Except(afterSplit).Count());
            }
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree6()
        {
            for (int insertKey = 0; insertKey < 10; insertKey++)
            {
                int[] keys = new int[] { 1, 2, 3, 4, 5 };
                int[] values = new int[] { 1, 2, 3, 4, 5};
                int leftKeyIndex = 0;

                int rightKeyIndex = 0;
                int[] rightKeys = null;
                int[] rightValues = null;
                int midElement = 0;

                var realResult = new List<int>(keys);
                realResult.Add(insertKey);
                realResult.Sort();
                int mid = (int)Math.Ceiling((float)(keys.Length - 1) / 2);
                int realMid = realResult.ElementAt(mid);


                Helpers.SplitLeaf(keys, values, insertKey, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);
                Assert.AreEqual(leftKeyIndex + 1 + rightKeyIndex + 1, keys.Length);
                Assert.AreEqual(realMid, midElement);
                var afterSplit = new List<int>();
                afterSplit.Add(insertKey);
                for (int i = 0; i <= leftKeyIndex; i++)
                    afterSplit.Add(keys[i]);
                for (int i = 0; i <= rightKeyIndex; i++)
                    afterSplit.Add(rightKeys[i]);
                afterSplit.Sort();
                Assert.AreEqual(0, realResult.Except(afterSplit).Count());
            }
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree7()
        {
            for (int insertKey = 0; insertKey < 10; insertKey++)
            {
                int[] keys = new int[] { 1, 2, 3, 4, 5, 6 };
                int[] values = new int[] { 1, 2, 3, 4, 5, 6 };
                int leftKeyIndex = 0;

                int rightKeyIndex = 0;
                int[] rightKeys = null;
                int[] rightValues = null;
                int midElement = 0;

                var realResult = new List<int>(keys);
                realResult.Add(insertKey);
                realResult.Sort();
                int mid = (int)Math.Ceiling((float)(keys.Length - 1) / 2);
                int realMid = realResult.ElementAt(mid);


                Helpers.SplitLeaf(keys, values, insertKey, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);
                Assert.AreEqual(leftKeyIndex + 1 + rightKeyIndex + 1, keys.Length);
                Assert.AreEqual(realMid, midElement);
                var afterSplit = new List<int>();
                afterSplit.Add(insertKey);
                for (int i = 0; i <= leftKeyIndex; i++)
                    afterSplit.Add(keys[i]);
                for (int i = 0; i <= rightKeyIndex; i++)
                    afterSplit.Add(rightKeys[i]);
                afterSplit.Sort();
                Assert.AreEqual(0, realResult.Except(afterSplit).Count());
            }
        }
        [TestMethod]
        public void SplitLeaf_MaxDegree7_AllAreEqual()
        {
            for (int insertKey = 0; insertKey < 10; insertKey++)
            {
                int[] keys = new int[] { 1, 1, 1, 1, 1, 1 };
                int[] values = new int[] { 1, 2, 3, 4, 5, 6 };
                int leftKeyIndex = 0;

                int rightKeyIndex = 0;
                int[] rightKeys = null;
                int[] rightValues = null;
                int midElement = 0;

                var realResult = new List<int>(keys);
                realResult.Add(insertKey);
                realResult.Sort();
                int mid = (int)Math.Ceiling((float)(keys.Length - 1) / 2);
                int realMid = realResult.ElementAt(mid);


                Helpers.SplitLeaf(keys, values, insertKey, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);
                Assert.AreEqual(leftKeyIndex + 1 + rightKeyIndex + 1, keys.Length);
                Assert.AreEqual(realMid, midElement);
                var afterSplit = new List<int>();
                afterSplit.Add(insertKey);
                for (int i = 0; i <= leftKeyIndex; i++)
                    afterSplit.Add(keys[i]);
                for (int i = 0; i <= rightKeyIndex; i++)
                    afterSplit.Add(rightKeys[i]);
                afterSplit.Sort();
                Assert.AreEqual(0, realResult.Except(afterSplit).Count());
            }
        }
        [TestMethod]
        public void SplitLeaf_UpToMaxDegree1000_AllAreEqual()
        {
            for (int maxDegree = 8; maxDegree <= 1000; maxDegree++)
            {
                for (int insertKey = 0; insertKey < 10; insertKey++)
                {
                    var keys = GenerateKeys(maxDegree- 1);
                    var values = keys.ToArray();
                    int leftKeyIndex = 0;

                    int rightKeyIndex = 0;
                    long[] rightKeys = null;
                    long[] rightValues = null;
                    long midElement = 0;

                    var realResult = new List<long>(keys);
                    realResult.Add(insertKey);
                    realResult.Sort();
                    int mid = (int)Math.Ceiling((float)(keys.Length - 1) / 2);
                    var realMid = realResult.ElementAt(mid);


                    Helpers.SplitLeaf(keys, values, insertKey, out leftKeyIndex, out rightKeys, out rightValues, out rightKeyIndex, out midElement);
                    Assert.AreEqual(leftKeyIndex + 1 + rightKeyIndex + 1, keys.Length);
                    Assert.AreEqual(realMid, midElement);
                    var afterSplit = new List<long>();
                    afterSplit.Add(insertKey);
                    for (int i = 0; i <= leftKeyIndex; i++)
                        afterSplit.Add(keys[i]);
                    for (int i = 0; i <= rightKeyIndex; i++)
                        afterSplit.Add(rightKeys[i]);
                    afterSplit.Sort();
                    Assert.AreEqual(0, realResult.Except(afterSplit).Count());
                }
            }
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
