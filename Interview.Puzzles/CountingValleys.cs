using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Puzzles
{
    class CountingValleys
    {
        static int countingValleys(int n, string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            int vallyCounter = 0;
            int relPositionToSealevel = 0;

            for (int i = 0; i < s.Length; i++)
            {
                relPositionToSealevel += s[i] == 'U' ? 1 : -1;

                if (relPositionToSealevel == -1 && s[i] != 'U') vallyCounter++;
            }

            return vallyCounter;
        }
    }
}
