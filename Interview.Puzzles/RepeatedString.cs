using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Puzzles
{
    class RepeatedString
    {
        static long repeatedString(string s, long n)
        {
            var count = 0L;
            if (string.IsNullOrEmpty(s)) return count;

            foreach (var letter in s)
            {
                if (letter == 'a')
                    count++;
            }

            var possibleStringRepeatitions = n / s.Length;
            count *= possibleStringRepeatitions;
            var offsetStringLength = n % s.Length;

            for (int i = 0; i < offsetStringLength; i++)
            {
                if (s[i] == 'a')
                    count++;
            }

            return count;
        }
    }
}
