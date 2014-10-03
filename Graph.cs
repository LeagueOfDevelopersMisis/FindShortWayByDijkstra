using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace SearchWay
{
    class Graph : IEnumerable, IEnumerator
    {
        int index = -1;
        private Vertex[] graph = { };
        private int auto_increment = 1;

        private ArrayList edges = new ArrayList();

        public Graph() { }
        public Graph(Vertex[] array)
        {
            this.graph = array;
        }

        public ICollection Vertexes
        {
            get
            {
                return graph;
            }
        }

        public Vertex this[int index]
        {
            get
            {
                if (graph.Length == 0)
                    return null;
                return graph[index];
            }

            set
            {
                graph[index] = value;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public bool MoveNext()
        {
            if (index == graph.Length - 1)
            {
                Reset();
                return false;
            }

            index++;
            return true;
        }

        public void Reset()
        {
            index = -1;
        }

        public object Current
        {
            get
            {
                return index < graph.Length ? graph[index] : null;
            }
        }

        public bool isEmpty()
        {
            return graph == null || graph.Length == 0;
        }

        public void Add(Vertex vertex)
        {
            vertex.ID = auto_increment++;
            int size = graph.Length;
            Vertex[] new_array = new Vertex[size + 1];
            Array.Copy(graph, new_array, size);
            graph = new_array;
            new_array = null;
            graph[graph.Length - 1] = vertex;
        }

        public void Remove(int position)
        {
            RemoveVertexEdges(graph[position].ID);
            Vertex[] new_array = new Vertex[graph.Length - 1];
            Array.Copy(graph, new_array, position);
            for (int i = position + 1; i < graph.Length; ++i)
                new_array[i - 1] = graph[i];
            graph = new_array;
            new_array = null;
        }

        public int Length
        {
            get { return graph.Length; }
        }

        public int GetPositionById(int id)
        {
            for (int i = 0; i < graph.Length; ++i) {
                if (graph[i].ID == id) {
                    return i;
                }
            }
            return 0;
        }

        public Vertex GetVertexByPoint(Point point)
        {
            Vertex vertex;
            for (int i = 0; i < graph.Length; ++i) {
                vertex = graph[i];
                if (vertex.Contain(point))
                    return vertex;
            }
            return null;
        }

        public void DeselectAll()
        {
            for (int i = 0; i < graph.Length; ++i) {
                graph[i].Deselect();
            }
        }

        public void AddEdge(Edge edge)
        {
            if (!ContainEdge(edge))
                edges.Add((Edge)edge);
            else
                MessageBox.Show("Ребро между данными вершинами уже существует", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool ContainEdge(Edge edge)
        {
            for (int i = 0; i < edges.Count; ++i)
                if (edge == (Edge)edges[i])
                    return true;
            return false;
        }

        public ArrayList Edges
        {
            get { return edges; }
        }

        public Vertex GetVertexById(int id)
        {
            for (int i = 0; i < graph.Length; ++i)
                if (id == graph[i].ID)
                    return graph[i];
            return null;
        }

        public void RemoveVertexEdges(int id)
        {
            for (int i = 0; i < edges.Count; ++i) {
                Edge edge = edges[i] as Edge;
                if (edge.FirstID == id || edge.SecondID == id) {
                    edges.RemoveAt(i--);
                }
            }
        }

        public void RemoveEdgeBetweenVortex(int id_a, int id_b)
        {
            for (int i = 0; i < edges.Count; ++i)
            {
                Edge edge = edges[i] as Edge;
                if (edge.FirstID == id_a && edge.SecondID == id_b || edge.FirstID == id_b && edge.SecondID == id_a)
                {
                    edges.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveEdgeBetweenVortex(Vertex a, Vertex b)
        {
            int id_a = a.ID, id_b = b.ID;
            for (int i = 0; i < edges.Count; ++i)
            {
                Edge edge = edges[i] as Edge;
                if (edge.FirstID == id_a && edge.SecondID == id_b || edge.FirstID == id_b && edge.SecondID == id_a)
                {
                    edges.RemoveAt(i);
                    break;
                }
            }
        }

        public List<Edge> GetVertexEdges(int id)
        {
            List<Edge> result = new List<Edge>();
            for (int i = 0; i < edges.Count; ++i) {
                Edge edge = edges[i] as Edge;
                if (edge.FirstID == id || edge.SecondID == id)
                    result.Add(edge);
            }
            return result;
        }

        public List<Pair> GetVertexEdgePositions(int id)
        {
            List<Pair> result = new List<Pair>();
            for (int i = 0; i < edges.Count; ++i)
            {
                Edge edge = edges[i] as Edge;
                int vertex_pair_id = -1;

                if (edge.FirstID == id)
                    vertex_pair_id = edge.SecondID;
                if (edge.SecondID == id)
                    vertex_pair_id = edge.FirstID;
                if (vertex_pair_id == -1)
                    continue;

                int position = GetPositionById(vertex_pair_id);
                result.Add(new Pair(position, edge.Weight));
            }
            return result;
        }

        public Edge GetEdgeBetweenVertexes(int id_a, int id_b)
        {
            for (int i = 0; i < edges.Count; ++i)
            {
                Edge edge = edges[i] as Edge;
                if (edge.FirstID == id_a && edge.SecondID == id_b || edge.FirstID == id_b && edge.SecondID == id_a)
                    return edge;
            }
            return null;
        }

        public void SetWeightByIds(int id_a, int id_b, int weight)
        {
            for (int i = 0; i < edges.Count; ++i)
            {
                Edge edge = edges[i] as Edge;
                if (edge.FirstID == id_a && edge.SecondID == id_b || edge.FirstID == id_b && edge.SecondID == id_a)
                    (edges[i] as Edge).Weight = weight;
            }
        }
    }
}
