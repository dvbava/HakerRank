using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution2
{

    class Node
    {
        public int Weight;
        public int[] Nodes;

        public Node(int w, int[] nodes)
        {
            this.Weight = w;
            this.Nodes = nodes;
        }

        public Node() : this(0, new int[0]) { }
    }

    // Complete the solve function below.
    static int[] solve(int[] c, int[][] tree, int[][] queries)
    {
        Dictionary<int, Node> nodeList = new Dictionary<int, Node>();
        var dict = tree.GroupBy(k => k[0], v => v[1]).ToDictionary(k => k.Key, v => v.ToArray());

        for (int i = 0; i < c.Length; i++)
            nodeList[i + 1] = new Node(c[i], dict.ContainsKey(i + 1) ? dict[i + 1] : new int[0]);

        foreach (var q in queries)
        {
            int x = q[0], y = q[1], z = q[2], w = q[3];

            for (int i = x; i <= y; i++)
            {

            }
        }

        return new int[0];
    }

    static void Main(string[] args)
    {
        int[] result = solve(new int[] { 10, 2, 3, 5, 10, 5, 3, 6, 2, 1 },
            new int[][]
            {
                new int[] {1, 2 },
                new int[] {1, 3 },
                new int[] {3, 4 },
                new int[] {3, 5 },
                new int[] {3, 6 },
                new int[] {4, 7 },
                new int[] {5, 8 },
                new int[] {7, 9 },
                new int[] {2, 10}
            },

            new int[][]
            {
                new int[] {8, 5, 2, 10},
                new int[] {3, 8, 4, 9 },
                new int[] {1, 9, 5, 9 },
                new int[] {4, 6, 4, 6 },
                new int[] {5, 8, 5, 8 }
            });
    }
}
