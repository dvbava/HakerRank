using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Puzzles
{
    public static class PowerSet
    {
        /// <summary>
        /// Find all the subsets of a given set.
        /// </summary>
        /// <param name="set"></param>
        /// <param name="set_size"></param>
        public static void PrintPowerSet(string[] set, int set_size)
        {
            /* Set_size of power set of a set with set_size n is (2**n -1) */
            uint pow_set_size = (uint)Math.Pow(2, set_size);
            int counter, j;

            /* Run from counter 000..0 to 111..1 */
            for (counter = 0; counter < pow_set_size; counter++)
            {
                List<string> list = new List<string>();
                for (j = 0; j < set_size; j++)
                {
                    /* Check if jth bit in the counter is set If set then print jth element from set */
                    if ((counter & (1 << j)) > 0)
                        list.Add(set[j]);
                }

                Console.WriteLine($"{{{string.Join(',', list)}}}");
            }
        }
    }
}
