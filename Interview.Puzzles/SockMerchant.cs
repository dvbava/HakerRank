using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview.Puzzles
{
    class SockMerchant
    {
        static int sockMerchant(int n, int[] ar)
        {

            if (ar == null || ar.Count() == 0) return 0;

            return ar.GroupBy(i => i).Select(i => i.Count() / 2).Sum();

        }
    }
}
