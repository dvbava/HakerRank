using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview.Puzzles
{
    class SherlockAndAnagrams
    {
        static int sherlockAndAnagrams(string s)
        {
            var counter = 0;

            for (int i = 1; i <= s.Length - 1; i++)
            {
                var tokens = new List<string>();

                for (int j = 0; j <= s.Length - i; j++)
                    tokens.Add(new string(s.Substring(j, i).OrderBy(c => c).ToArray()));

                var anagramGroups = tokens.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

                foreach (var grps in anagramGroups)
                    counter += grps.Value * (grps.Value - 1) / 2;
            }

            return counter;
        }
    }
}
