using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchWay
{
    class MyAlgorithm
    {
        private Graph graph;
        private const int INF = unchecked((int)10e9);

        List<List<Pair>> g;
        List<int> dist;

        int min_weight = INF;
        List<int> min_distance;

        int from, to;

        public MyAlgorithm(ref Graph graph)
        {
            this.graph = graph;
        }

        public List<Vertex> GetShortRoute(int first_id, int second_id)
        {
            from = graph.GetPositionById(first_id);
            to = graph.GetPositionById(second_id);

            int n = graph.Length;
            g = new List<List<Pair>>();
            dist = new List<int>();

            for (int i = 0; i < n; ++i)
            {
                g.Add(new List<Pair>());
                dist.Add(INF);
            }

            for (int i = 0; i < n; ++i)
                g[i] = graph.GetVertexEdgePositions(graph[i].ID);

            dist[from] = 0;
            next_step(new List<int>(), 0, from); /* it invokes the first step of recursion */

            return min_distance != null ? GetVertexesByPositions(min_distance) : new List<Vertex>();
        }

        private void next_step(List<int> list, int sum_weight, int cur)
        {
            list.Add(cur);
            if (cur == to)
            {
                min_distance = list;
                min_weight = sum_weight;
            }
            
            for (int i = 0; i < g[cur].Count; ++i)
            {
                int pos = g[cur][i].First;
                if (dist[pos] > g[cur][i].Second + sum_weight) {
                    dist[pos] = g[cur][i].Second + sum_weight;
                    next_step(new List<int>(list), dist[pos], pos);
                }
            }
        }

        private List<Vertex> GetVertexesByPositions(List<int> list)
        {
            List<Vertex> result = new List<Vertex>();
            foreach (int pos in list)
                result.Add(graph[pos]);
            return result;
        }
    }
}
