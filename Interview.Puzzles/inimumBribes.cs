using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Puzzles
{
    public class inimumBribes
    {
        public static void minimumBribes(int[] q)
        {

            int ans = 0;
            for (int i = q.Length - 1; i >= 0; i--)
            {
                if (q[i] - (i + 1) > 2)
                {
                    Console.WriteLine("Too chaotic");
                    return;
                }
                for (int j = Math.Max(0, q[i] - 2); j < i; j++)
                    if (q[j] > q[i]) ans++;
            }
            Console.WriteLine(ans);
        }
    }
}
