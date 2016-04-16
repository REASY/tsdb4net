using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    static class Helpers
    {
        public static void SplitLeaf<K, V>(K[] leftKeys, V[] leftValues, K key, out int lastLeftKeysIndex, out K[] rightKeys, out V[] rightValues, out int lastRightKeysIndex, out K midElement) where K : IComparable<K>
        {
            var keysLength = leftKeys.Length;
            lastLeftKeysIndex = keysLength;
            rightKeys = new K[keysLength];
            rightValues = new V[keysLength];
            int fakeIndex = 0;
            bool isFound = false;
            int mid = (int)Math.Ceiling((float)(keysLength - 1) / 2);
            midElement = default(K);
            int rightIndex = 0;
            for (int i = 0; i < keysLength; i++)
            {
                if (key.CompareTo(leftKeys[i]) <= 0 && !isFound)
                {
                    if (mid == fakeIndex) { midElement = key; }
                    fakeIndex += 1;
                    isFound = true;
                }
                if (fakeIndex >= mid)
                {
                    rightKeys[rightIndex] = leftKeys[i];
                    rightValues[rightIndex] = leftValues[i];
                    rightIndex++;
                }
                if (mid == fakeIndex) { midElement = leftKeys[i]; }
                fakeIndex += 1;
            }
            lastRightKeysIndex = rightIndex - 1;
            fakeIndex = 0;
            int deleted = 0;
            int needDelete = keysLength - mid;
            lastLeftKeysIndex--;
            for (int i = 0; i < keysLength; i++)
            {
                if (deleted == 0 && key.CompareTo(leftKeys[i]) <= 0)
                {
                    if (fakeIndex < mid)
                    {
                        leftKeys[lastLeftKeysIndex] = default(K);
                        leftValues[lastLeftKeysIndex] = default(V);
                        lastLeftKeysIndex--;
                        deleted++;
                    }
                    fakeIndex += 1;
                }
                if (fakeIndex >= mid && deleted <= needDelete)
                {
                    leftKeys[lastLeftKeysIndex] = default(K);
                    leftValues[lastLeftKeysIndex] = default(V);
                    lastLeftKeysIndex--;
                    deleted++;
                }
                fakeIndex += 1;
            }
            if (deleted == 0)
            {
                leftKeys[keysLength] = default(K);
                leftValues[keysLength] = default(V);
                lastLeftKeysIndex--;
            }

        }
        public static void SplitNode<K, V>(K[] leftKeys, V[] leftChildren, K key, out int lastLeftKeyIndex, out K[] rightKeys, out V[] rightChildren, out int lastRightKeyIndex, out int lastRightChildIndex, out K midElement) where K : IComparable<K>
        {
            bool isFound = false;
            var keysLength = leftKeys.Length;
            int midIndex = (keysLength + 1) / 2;
            int keyIndex = 0;
            midElement = default(K);
            for (int i = 0, j = 0; i < leftKeys.Length; i++, j++)
            {
                if (key.CompareTo(leftKeys[i]) < 0 && !isFound)
                {
                    if (midIndex == j)
                        midElement = key;
                    keyIndex = i;
                    isFound = true;
                }
                else if (midIndex == j)
                {
                    keyIndex = i;
                    midElement = leftKeys[keyIndex];
                    leftKeys[keyIndex] = default(K);
                }
            }
            lastLeftKeyIndex = keyIndex - 1;
            lastRightKeyIndex = 0;
            rightKeys = new K[keysLength];
            rightChildren = new V[keysLength + 1];
            if (isFound)
            {
                for (int i = keyIndex; i < keysLength; i++)
                {
                    rightKeys[lastRightKeyIndex++] = leftKeys[i];
                    leftKeys[i] = default(K);
                }
            }
            else
            {
                bool isKeyWritten = false;
                for (int i = keyIndex + 1; i < keysLength; i++)
                {
                    if (key.CompareTo(leftKeys[i]) < 0 && !isKeyWritten)
                    {
                        rightKeys[lastRightKeyIndex++] = key;
                        isKeyWritten = true;
                    }
                    else rightKeys[lastRightKeyIndex++] = leftKeys[i];
                    leftKeys[i] = default(K);
                }
                if (!isKeyWritten)
                    rightKeys[lastRightKeyIndex++] = key;
            }
            lastRightKeyIndex--;
            lastRightChildIndex = 0;
            for (int i = keyIndex + 1; i < leftChildren.Length; i++)
            {
                rightChildren[lastRightChildIndex++] = leftChildren[i];
                leftChildren[i] = default(V);
            }
            lastRightChildIndex--;
        }
        public static bool AreKeysOk<K>(K[] keys, int lastIndex) where K : IComparable<K>
        {
            K prevK = keys[0];
            for (int i = 1; i <= lastIndex; i++)
            {
                if (prevK.CompareTo(keys[i]) > 0)
                    return false;
                prevK = keys[i];
            }
            return true;
        }
        public static bool CheckNodes<K, V>(Node<K, V> node) where K : IComparable<K>
        {
            if (node is Leaf<K, V>)
            {
                if (!Helpers.AreKeysOk(node.Keys, node.KeyIndex))
                {
                    Console.WriteLine("Keys for {0} isn't OK", node);
                    return false;
                }
                else return true;
            }
            var internalNode = node as InternalNode<K, V>;
            var child = internalNode.Children[0];
            if (!Helpers.AreKeysOk(child.Keys, child.KeyIndex))
            {
                Console.WriteLine("Keys for {0} isn't OK", child);
                return false;
            }
            for (int i = 1; i <= internalNode.KeyIndex + 1; i++)
            {
                var currNode = internalNode.Children[i];
                if (!Helpers.AreKeysOk(currNode.Keys, currNode.KeyIndex))
                {
                    Console.WriteLine("Keys for {0} isn't OK", currNode);
                    return false;
                }
                if (child.GetType() != currNode.GetType())
                {
                    Console.WriteLine("Type of {0} != Type of {1}", child, internalNode);
                    return false;
                }
                var maxKeyInChild = child.Keys.Take(child.KeyIndex + 1).Max();
                var minKeyInCurrent = currNode.Keys.Take(currNode.KeyIndex + 1).Min();
                if (maxKeyInChild.CompareTo(minKeyInCurrent) > 0)
                {
                    Console.WriteLine("MaxKey {0} must be > MinKey {1}", child, currNode);
                    return false;
                }
                child = currNode;
            }
            for (int i = 0; i <= internalNode.KeyIndex + 1; i++)
            {
                CheckNodes(internalNode.Children[i]);
            }
            return true;
        }
    }
}
