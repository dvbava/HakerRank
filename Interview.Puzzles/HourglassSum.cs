using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Puzzles
{
    class HourglassSum
    {
        static int hourglassSum(int[][] arr)
        {
            int maxResult = -10000;

            for (int i = 0; i < arr.Length - 2; i++)
            {
                for (int j = 0; j < arr[i].Length - 2; j++)
                {
                    var sum = GetSum(i, j, arr);

                    if (maxResult < sum)
                        maxResult = sum;
                }
            }

            return maxResult;
        }

        private static int GetSum(int x, int y, int[][] arr)
        {
            int sum = 0;
            for (int i = x; i <= x + 2; i++)
            {
                for (int j = y; j <= y + 2; j++)
                {
                    if (i == x + 1 && j != y + 1) continue;

                    sum = sum + arr[i][j];
                }
            }

            return sum;
        }
    }
}
