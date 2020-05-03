using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview.Puzzles
{
    class CheckMagazine
    {
        static void checkMagazine(string[] magazines, string[] notes)
        {
            if (magazines == null || magazines.Length == 0)
            {
                Console.WriteLine("No");
                return;
            }

            if (notes == null || notes.Length == 0)
            {
                Console.WriteLine("Yes");
                return;
            }

            var counter = magazines.GroupBy(s => s).ToDictionary(g => g.Key, g => g.Count());

            foreach (var note in notes)
            {
                if (counter.ContainsKey(note))
                {
                    if (counter[note] > 0)
                    {
                        counter[note] = counter[note] - 1;
                        continue;
                    }
                }

                Console.WriteLine("No");
                return;
            }

            Console.WriteLine("Yes");
        }
    }
}
