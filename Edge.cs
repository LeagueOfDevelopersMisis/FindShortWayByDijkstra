using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SearchWay
{
    class Edge
    {
        private int id_first, id_second;
        private ViewEdge view;
        private int weight;

        public Edge(int a, int b, int weight = 10) 
        {
            this.id_first = a;
            this.id_second = b;
            this.weight = weight;
            this.view = ViewEdge.CreateView(Color.Lime);
        }

        public Edge(Vertex a, Vertex b, int weight = 10)
        {
            if ((object)a == null || (object)b == null)
                throw new ArgumentNullException();
            this.id_first = a.ID;
            this.id_second = b.ID;
            this.weight = weight;
            this.view = ViewEdge.CreateView(Color.Lime);
        }

        public int FirstID
        {
            get { return id_first; }
            set { id_first = value; }
        }

        public int SecondID
        {
            get { return id_second; }
            set { id_second = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public ViewEdge View
        {
            get { return view; }
        }

        public static bool operator ==(Edge a, Edge b) 
        {
            return a.id_first == b.id_first && a.id_second == b.id_second
                || a.id_first == b.id_second && a.id_second == b.id_first;
        }

        public static bool operator !=(Edge a, Edge b)
        {
            return !(operator ==(a, b));
        }
    }
}
