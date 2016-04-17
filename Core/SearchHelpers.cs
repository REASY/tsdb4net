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

        /// <summary>
        /// Returns an index of the first element that is greater than key. 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="keys"></param>
        /// <param name="len"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int UpperBound<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            if (len <= SEARCH_ALGORITHM_KEYS_COUNT_THRESHOLD)
                return UpperBoundLinear8(keys, len, key);
            else
                return UpperBoundBinary(keys, len, key);
        }
        /// <summary>
        /// Returns an index of the first element that is greater than key.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="keys"></param>
        /// <param name="len"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int UpperBoundLinear<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            int index = -1;
            for (int i = 0; i < len; i++)
            {
                if (key.CompareTo(keys[i]) < 0) { index = i; break; }
            }
            return index;
        }
        /// <summary>
        /// Returns an index of the first element that is greater than key.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="keys"></param>
        /// <param name="len"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Returns an index of the first element that is greater than key.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="keys"></param>
        /// <param name="len"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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
        internal static int UpperBoundBinary<K>(K[] keys, int len, K key) where K : IComparable<K>
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
        /// <summary>
        /// Returns an index of the first element that is not less than(i.e.greater or equal to) key.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="keys"></param>
        /// <param name="len"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int LowerBound<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            if (len <= SEARCH_ALGORITHM_KEYS_COUNT_THRESHOLD)
                return LowerBoundLinear8(keys, len, key);
            else
                return LowerBoundBinary(keys, len, key);
        }
        /// <summary>
        /// Returns an index of the first element that is not less than(i.e.greater or equal to) key.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="keys"></param>
        /// <param name="len"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static int LowerBoundLinear<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            int index = -1;
            for (int i = 0; i < len; i++)
            {
                if (keys[i].CompareTo(key) >= 0) { index = i; break; }
            }
            return index;
        }
        /// <summary>
        /// Returns an index of the first element that is not less than(i.e.greater or equal to) key.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="keys"></param>
        /// <param name="len"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static int LowerBoundLinear8<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            int index = -1;
            int fullLoops = len / 8;
            if (fullLoops == 0)
            {
                for (int i = 0; i < len; i++)
                {
                    if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                }
                return index;
            }
            else
            {
                int i = 0;
                for (int k = 0; k < fullLoops; k++, i += 8)
                {
                    if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                    if (keys[i + 1].CompareTo(key) >= 0) { index = i + 1; break; }
                    if (keys[i + 2].CompareTo(key) >= 0) { index = i + 2; break; }
                    if (keys[i + 3].CompareTo(key) >= 0) { index = i + 3; break; }
                    if (keys[i + 4].CompareTo(key) >= 0) { index = i + 4; break; }
                    if (keys[i + 5].CompareTo(key) >= 0) { index = i + 5; break; }
                    if (keys[i + 6].CompareTo(key) >= 0) { index = i + 6; break; }
                    if (keys[i + 7].CompareTo(key) >= 0) { index = i + 7; break; }
                }
                if (index == -1)
                {
                    int mod = len % 8;
                    switch (mod)
                    {
                        case 0:
                            break;
                        case 1:
                            if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                            break;
                        case 2:
                            if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                            if (keys[i + 1].CompareTo(key) >= 0) { index = i + 1; break; }
                            break;
                        case 3:
                            if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                            if (keys[i + 1].CompareTo(key) >= 0) { index = i + 1; break; }
                            if (keys[i + 2].CompareTo(key) >= 0) { index = i + 2; break; }
                            break;
                        case 4:
                            if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                            if (keys[i + 1].CompareTo(key) >= 0) { index = i + 1; break; }
                            if (keys[i + 2].CompareTo(key) >= 0) { index = i + 2; break; }
                            if (keys[i + 3].CompareTo(key) >= 0) { index = i + 3; break; }
                            break;
                        case 5:
                            if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                            if (keys[i + 1].CompareTo(key) >= 0) { index = i + 1; break; }
                            if (keys[i + 2].CompareTo(key) >= 0) { index = i + 2; break; }
                            if (keys[i + 3].CompareTo(key) >= 0) { index = i + 3; break; }
                            if (keys[i + 4].CompareTo(key) >= 0) { index = i + 4; break; }
                            break;
                        case 6:
                            if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                            if (keys[i + 1].CompareTo(key) >= 0) { index = i + 1; break; }
                            if (keys[i + 2].CompareTo(key) >= 0) { index = i + 2; break; }
                            if (keys[i + 3].CompareTo(key) >= 0) { index = i + 3; break; }
                            if (keys[i + 4].CompareTo(key) >= 0) { index = i + 4; break; }
                            if (keys[i + 5].CompareTo(key) >= 0) { index = i + 5; break; }
                            break;
                        case 7:
                            if (keys[i].CompareTo(key) >= 0) { index = i; break; }
                            if (keys[i + 1].CompareTo(key) >= 0) { index = i + 1; break; }
                            if (keys[i + 2].CompareTo(key) >= 0) { index = i + 2; break; }
                            if (keys[i + 3].CompareTo(key) >= 0) { index = i + 3; break; }
                            if (keys[i + 4].CompareTo(key) >= 0) { index = i + 4; break; }
                            if (keys[i + 5].CompareTo(key) >= 0) { index = i + 5; break; }
                            if (keys[i + 6].CompareTo(key) >= 0) { index = i + 6; break; }
                            break;
                    }
                }
                return index;
            }
        }
        /// <summary>
        /// Returns an index of the first element that is not less than(i.e.greater or equal to) key.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="keys"></param>
        /// <param name="len"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static int LowerBoundBinary<K>(K[] keys, int len, K key) where K : IComparable<K>
        {
            if (keys.Length == 0)
                return -1;
            if (keys.Length == 1)
                return keys[0].CompareTo(key) >= 0 ? 0 : -1;
            int low = 0;
            int high = len;
            int mid = low + (high - low) / 2;
            int prevMid = -1;

            while (mid < len)
            {
                if (keys[mid].CompareTo(key) >= 0)
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
