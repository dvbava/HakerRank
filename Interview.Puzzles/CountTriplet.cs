using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Puzzles
{
    public class CountTriplet
    {
        public static long countTriplets(List<long> arr, long r)
        {
            var mp2 = new Dictionary<long, long>();
            var mp3 = new Dictionary<long, long>();
            long res = 0;
            foreach (long val in arr)
            {
                if (mp3.ContainsKey(val))
                    res += mp3[val];

                if (mp2.ContainsKey(val))
                    if (mp3.ContainsKey(val * r))
                        mp3[val * r] += mp2[val];
                    else
                        mp3[val * r] = mp2[val];

                if (mp2.ContainsKey(val * r))
                    mp2[val * r]++;
                else
                    mp2[val * r] = 1;
            }
            return res;
        }
    }
}
