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
        private const int SEARCH_ALGORITHM_KEYS_COUNT_THRESHOLD = 55;

        public static Node<K, V> RecursiveChooseSubtree<K, V>(K key, Node<K, V> node) where K : IComparable<K>
        {
            if (node is Leaf<K, V>) return node;
            else
            {
                var n = node as InternalNode<K, V>;
                int index = UpperBound(n.Keys, n.KeyIndex, key);
                return RecursiveChooseSubtree(key, n.Children[index]);
            }
        }
        public static Node<K, V> ChooseSubtree<K, V>(K key, Node<K, V> node) where K : IComparable<K>
        {
            if (node is Leaf<K, V>) return node;
            else
            {
                while (true)
                {
                    var n = node as InternalNode<K, V>;
                    int index = UpperBound(n.Keys, n.KeyIndex, key);
                    node = n.Children[index];
                    if (node is Leaf<K, V>)
                        return node;
                }
            }
        }
        public static int UpperBound<K>(K[] keys, int lastIndex, K key) where K : IComparable<K>
        {
            if (lastIndex <= SEARCH_ALGORITHM_KEYS_COUNT_THRESHOLD)
                return UpperBoundLinear8(keys, lastIndex, key);
            else
                return UpperBoundBinary(keys, lastIndex, key);
        }
        public static int UpperBoundLinear<K>(K[] keys, int lastIndex, K key) where K : IComparable<K>
        {
            int index = -1;
            for (int i = 0; i <= lastIndex; i++)
            {
                if (key.CompareTo(keys[i]) < 0) { index = i; break; }
            }
            if (index == -1) index = lastIndex + 1;
            return index;
        }
        public static int UpperBoundLinear4<K>(K[] keys, int lastIndex, K key) where K : IComparable<K>
        {
            int index = -1;
            int fullCnt = lastIndex / 4;
            if (fullCnt == 0)
            {
                for (int i = 0; i <= lastIndex; i++)
                {
                    if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                }
                if (index == -1) index = lastIndex + 1;
                return index;
            }
            else
            {
                int i = 0;
                for (i = 0; i < fullCnt; i += 4)
                {
                    if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                    if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                    if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                    if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; break; }
                }
                int mod = lastIndex % 4;
                switch(mod)
                {
                    case 0:
                        break;
                    case 1:
                        if (key.CompareTo(keys[i]) < 0) {  index = i;  }
                        break;
                    case 2:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1;}
                        break;
                    case 3:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; }
                        if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; }
                        break;
                }
                if (index == -1) index = lastIndex + 1;
                return index;
            }
        }
        public static int UpperBoundLinear8<K>(K[] keys, int lastIndex, K key) where K : IComparable<K>
        {
            int index = -1;
            int fullCnt = lastIndex / 8;
            if (fullCnt == 0)
            {
                for (int i = 0; i <= lastIndex; i++)
                {
                    if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                }
                if (index == -1) index = lastIndex + 1;
                return index;
            }
            else
            {
                int i = 0;
                for (i = 0; i < fullCnt; i += 8)
                {
                    if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                    if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                    if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                    if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; break; }
                    if (key.CompareTo(keys[i + 4]) < 0) { index = i + 4; break; }
                    if (key.CompareTo(keys[i + 5]) < 0) { index = i + 5; break; }
                    if (key.CompareTo(keys[i + 6]) < 0) { index = i + 6; break; }
                    if (key.CompareTo(keys[i + 7]) < 0) { index = i + 7; break; }
                }
                int mod = lastIndex % 8;
                switch (mod)
                {
                    case 0:
                        break;
                    case 1:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        break;
                    case 2:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; }
                        break;
                    case 3:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; }
                        if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; }
                        break;
                    case 4:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; }
                        if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; }
                        if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; }
                        break;
                    case 5:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; }
                        if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; }
                        if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; }
                        if (key.CompareTo(keys[i + 4]) < 0) { index = i + 4; }
                        break;
                    case 6:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; }
                        if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; }
                        if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; }
                        if (key.CompareTo(keys[i + 4]) < 0) { index = i + 4; }
                        if (key.CompareTo(keys[i + 5]) < 0) { index = i + 5; }
                        break;
                    case 7:
                        if (key.CompareTo(keys[i]) < 0) { index = i; }
                        if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; }
                        if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; }
                        if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; }
                        if (key.CompareTo(keys[i + 4]) < 0) { index = i + 4; }
                        if (key.CompareTo(keys[i + 5]) < 0) { index = i + 5; }
                        if (key.CompareTo(keys[i + 6]) < 0) { index = i + 6; }
                        break;
                }
                if (index == -1) index = lastIndex + 1;
                return index;
            }
        }
        public static int UpperBoundBinary<K>(K[] keys, int lastIndex, K key) where K : IComparable<K>
        {
            int low = 0;
            int high = lastIndex;
            int mid = 0;
            int prevMid = 0;
            while (true)
            {
                mid = low + (high - low) / 2;
                if (keys[mid].CompareTo(key) <= 0)
                    low = mid;
                else
                    high = mid;
                if (prevMid == mid)
                    break;
                prevMid = mid;
            }
            if (mid == lastIndex - 1 && keys[mid].CompareTo(key) < 0)
            {
                if (keys[mid + 1].CompareTo(key) <= 0)
                    return lastIndex + 1;
                else
                    return lastIndex;
            }
            if (keys[mid].CompareTo(key) == 0)
                mid++;
            return mid;
        }
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
            int midIndexInLeftKeys = 0;
            for (int i = 0, j = 0; i < leftKeys.Length; i++, j++)
            {
                if (key.CompareTo(leftKeys[i]) < 0 && !isFound)
                {
                    j++;
                    isFound = true;
                }
                if (midIndex == j) midIndexInLeftKeys = i;
            }
            midElement = leftKeys[midIndexInLeftKeys];
            leftKeys[midIndexInLeftKeys] = default(K);
            lastLeftKeyIndex = midIndexInLeftKeys - 1;
            lastRightKeyIndex = 0;
            rightKeys = new K[keysLength];
            rightChildren = new V[keysLength + 1];
            for (int i = midIndexInLeftKeys + 1; i < keysLength; i++)
            {
                rightKeys[lastRightKeyIndex] = leftKeys[i];
                leftKeys[i] = default(K);
            }
            lastRightKeyIndex--;
            lastRightChildIndex = 0;
            for (int i = midIndexInLeftKeys + 1; i < leftChildren.Length; i++)
            {
                rightChildren[lastRightChildIndex++] = leftChildren[i];
                leftChildren[i] = default(V);
            }
            lastRightChildIndex--;
        }
    }
}
