using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal static class SearchHelpers
    {
        public static readonly int SEARCH_ALGORITHM_KEYS_COUNT_THRESHOLD = 8;

        public static int UpperBound<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            if (len <= SEARCH_ALGORITHM_KEYS_COUNT_THRESHOLD)
                return UpperBoundLinear8(keys, len, key);
            else
                return UpperBoundBinary(keys, len, key);
        }
        public static int UpperBoundLinear<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            int index = -1;
            for (int i = 0; i < len; i++)
            {
                if (key.CompareTo(keys[i]) < 0) { index = i; break; }
            }
            return index;
        }
        public static int UpperBoundLinear4<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            int index = -1;
            int fullLoops = len / 4;
            if (fullLoops == 0)
            {
                for (int i = 0; i < len; i++)
                {
                    if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                }
                return index;
            }
            else
            {
                int i = 0;
                for (int k = 0; k < fullLoops; k++, i += 4)
                {
                    if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                    if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                    if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                    if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; break; }
                }
                if (index == -1)
                {
                    int mod = len % 4;
                    switch (mod)
                    {
                        case 0:
                            break;
                        case 1:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            break;
                        case 2:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                            break;
                        case 3:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                            if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                            break;
                    }
                }
                return index;
            }
        }
        public static int UpperBoundLinear8<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            int index = -1;
            int fullLoops = len / 8;
            if (fullLoops == 0)
            {
                for (int i = 0; i < len; i++)
                {
                    if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                }                
                return index;
            }
            else
            {
                int i = 0;
                for (int k = 0; k < fullLoops; k++, i += 8)
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
                if (index == -1)
                {
                    int mod = len % 8;
                    switch (mod)
                    {
                        case 0:
                            break;
                        case 1:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            break;
                        case 2:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                            break;
                        case 3:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                            if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                            break;
                        case 4:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                            if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                            if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; break; }
                            break;
                        case 5:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                            if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                            if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; break; }
                            if (key.CompareTo(keys[i + 4]) < 0) { index = i + 4; break; }
                            break;
                        case 6:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                            if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                            if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; break; }
                            if (key.CompareTo(keys[i + 4]) < 0) { index = i + 4; break; }
                            if (key.CompareTo(keys[i + 5]) < 0) { index = i + 5; break; }
                            break;
                        case 7:
                            if (key.CompareTo(keys[i]) < 0) { index = i; break; }
                            if (key.CompareTo(keys[i + 1]) < 0) { index = i + 1; break; }
                            if (key.CompareTo(keys[i + 2]) < 0) { index = i + 2; break; }
                            if (key.CompareTo(keys[i + 3]) < 0) { index = i + 3; break; }
                            if (key.CompareTo(keys[i + 4]) < 0) { index = i + 4; break; }
                            if (key.CompareTo(keys[i + 5]) < 0) { index = i + 5; break; }
                            if (key.CompareTo(keys[i + 6]) < 0) { index = i + 6; break; }
                            break;
                    }
                }
                return index;
            }
        }
        public static int UpperBoundBinary<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            if (keys.Length == 0)
                return -1;
            if (keys.Length == 1)
                return key.CompareTo(keys[0]) < 0 ? 0 : -1;
            int low = 0;
            int high = len;
            int mid = low + (high - low) / 2;
            int prevMid = -1;

            while (mid < len)
            {
                if (key.CompareTo(keys[mid]) < 0)
                    high = mid;
                else
                    low = mid + 1;
                if (prevMid == mid)
                    break;
                prevMid = mid;
                mid = low + (high - low) / 2;
            }
            return mid == len ? -1 : mid;
        }
    }
}
