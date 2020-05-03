using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Puzzles
{
    class Jumping_on_the_Clouds
    {
        static int jumpingOnClouds(int[] c)
        {
            int jumpCount = 0;
            int currentPosition = 0;

            if (c == null) return jumpCount;

            while (true)
            {
                if (canJump(currentPosition, c, 2))
                {
                    currentPosition += 2;
                    jumpCount++;
                }
                else if (canJump(currentPosition, c, 1))
                {
                    currentPosition += 1;
                    jumpCount++;
                }

                if (currentPosition >= c.Length - 1) break;
            }

            return jumpCount;
        }

        private static bool canJump(int i, int[] c, int hope)
        {
            if (i + hope > c.Length - 1) return false;

            if (c[i + hope] == 0) return true;

            return false;
        }
    }
}
