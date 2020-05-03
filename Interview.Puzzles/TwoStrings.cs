using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview.Puzzles
{
    class TwoStrings
    {
        static string twoStrings(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2)) return "NO";

            var s1Index = s1.GroupBy(s => s).ToDictionary(g => g.Key);
            var s2Index = s2.GroupBy(s => s).ToDictionary(g => g.Key);

            if (s1Index.Keys.Intersect(s2Index.Keys).Count() > 0) return "YES";

            return "NO";
        }
    }
}
