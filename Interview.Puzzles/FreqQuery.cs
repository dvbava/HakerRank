using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview.Puzzles
{
    class FreqQuery
    {
        static List<int> freqQuery(List<List<int>> queries)
        {
            List<int> res = new List<int>();

            if (queries == null || queries.Count == 0) return res;

            Dictionary<int, int> currentList = new Dictionary<int, int>();

            foreach (var query in queries)
            {
                int op = query[0], num = query[1];

                switch (op)
                {
                    case 1:
                        if (currentList.ContainsKey(num))
                            currentList[num]++;
                        else
                            currentList[num] = 1;
                        break;
                    case 2:
                        if (currentList.ContainsKey(num) && currentList[num] == 1)
                        {
                            currentList.Remove(num);
                            break;
                        }
                        if (currentList.ContainsKey(num))
                            currentList[num]--;
                        break;
                    case 3:
                        var grp = currentList.Any(g => g.Value == num);
                        res.Add(grp ? 1 : 0);
                        break;
                }
            }

            return res;
        }
    }
}
