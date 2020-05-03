using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview.Puzzles
{
    class FunWithAnagrams
    {
        public static List<string> funWithAnagrams(List<string> text)
        {
            HashSet<string> lst = new HashSet<string> { text[0] };

            for (int i = 0; i < text.Count; i++)
            {
                for (int j = i + 1; j < text.Count; j++)
                {
                    if (isAnnagram(text[i], text[j]))
                        continue;
                    else
                        lst.Add(text[j]);
                }
            }

            return lst.ToList();
        }

        private static bool isAnnagram(string v1, string v2)
        {
            return string.Concat(v1.OrderBy(c => c)) == string.Concat(v2.OrderBy(c => c));
        }

    }
}
