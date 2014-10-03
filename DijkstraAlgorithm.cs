using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SearchWay
{
    class Pair
    {
        int first, second;

        public Pair()
        {
            first = 0; second = 0;
        }

        public Pair(int a, int b)
        {
            this.first = a;
            this.second = b;
        }

        public int First
        {
            get { return first; }
            set { first = value; }
        }

        public int Second
        {
            get { return second; }
            set { second = value; }
        }
    }

    class DijkstraAlgorithm
    {
        private Graph graph;
        private const int INF = unchecked((int)10e9);

        public DijkstraAlgorithm(ref Graph graph)
        {
            this.graph = graph;
        }

        public List<Vertex> GetShortRoute(int first_id, int second_id)
        {
            int arrival = graph.GetPositionById(second_id);
            int n = graph.Length;
            List<List<Pair>> g = new List<List<Pair>>();
            for (int i = 0; i < n; ++i)
                g.Add(new List<Pair>());

            for (int i = 0; i < n; ++i)
                g[i] = graph.GetVertexEdgePositions(graph[i].ID);


            int s = graph.GetPositionById(first_id);
            List<int> d = new List<int>(), p = new List<int>();
            List<bool> used = new List<bool>();
            for (int i = 0; i < n; ++i)
            {
                d.Add(INF);
                p.Add(-1);
                used.Add(false);
            }
            d[s] = 0;

            for (int i = 0; i < n; ++i)
            {
                int v = -1;
                for (int j = 0; j < n; ++j)
                    if (!used[j] && (v == -1 || d[j] < d[v]))
                        v = j;
                if (d[v] == INF)
                    break;
                used[v] = true;

                for (int j = 0; j < g[v].Count; ++j)
                {
                    int to = g[v][j].First,
                        len = g[v][j].Second;
                    if (d[v] + len < d[to])
                    {
                        d[to] = d[v] + len;
                        p[to] = v;
                    }
                }
            }

            List<Vertex> way = GetWayByPositions(p, s, arrival);


            return way;
        }

        private List<Vertex> GetWayByPositions(List<int> p, int s, int arrival)
        {
            List<Vertex> way = new List<Vertex>();
            way.Add(graph[arrival]);
            int cur = arrival;
            while (cur != s)
            {
                cur = p[cur];
                if (cur != -1)
                    way.Add(graph[cur]);
                else
                {
                    way.Clear();
                    break;
                }
            }

            return way;
        }
    }
}
