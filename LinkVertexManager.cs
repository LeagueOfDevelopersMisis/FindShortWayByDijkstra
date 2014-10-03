using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchWay
{
    class LinkVertexManager
    {
        private LinkedState current_state;

        public enum LinkedState {
            NoSelect,
            SelectedOne,
            SelectedTwo
        }

        private Tuple<Vertex, Vertex> pair;

        public LinkVertexManager()
        {
            current_state = LinkedState.NoSelect;
            pair = new Tuple<Vertex, Vertex>(null, null);
        }

        public LinkedState CurrentState
        {
            get { return current_state; }
        }

        public void InsertFirst(Vertex first)
        {
            Vertex second = pair.Item2;
            pair = new Tuple<Vertex, Vertex>(first, second);
            current_state = this.DetectState();
        }

        public void Swap()
        {
            Vertex first = pair.Item1;
            Vertex second = pair.Item2;
            pair = new Tuple<Vertex, Vertex>(second, first);
            current_state = this.DetectState();
        }

        public void InsertSecond(Vertex second)
        {
            Vertex first = pair.Item1;
            pair = new Tuple<Vertex, Vertex>(first, second);
            current_state = this.DetectState();
        }

        public Tuple<Vertex, Vertex> Pair
        {
            get { return pair; }
        }

        public bool IsFirst(Vertex vertex)
        {
            return vertex != null 
                && pair.Item1 != null 
                && vertex == pair.Item1;
        }

        public bool IsSecond(Vertex vertex)
        {
            return vertex != null 
                && pair.Item2 != null 
                && vertex == pair.Item2;
        }

        public void RemoveFirst()
        {
            Vertex second = pair.Item2;
            pair = new Tuple<Vertex, Vertex>(null, second);
            current_state = this.DetectState();
        }

        public void RemoveSecond()
        {
            Vertex first = pair.Item1;
            pair = new Tuple<Vertex, Vertex>(first, null);
            current_state = this.DetectState();
        }

        private LinkedState DetectState()
        {
            object first = (object)pair.Item1, second = (object)pair.Item2;

            if (first != null && second != null)
                return LinkedState.SelectedTwo;
            if (first != null && second == null || first == null && second != null)
                return LinkedState.SelectedOne;
            if (first == null && second == null)
                return LinkedState.NoSelect;
            return LinkedState.NoSelect;
        }

        public bool HasFirst()
        {
            return pair.Item1 != null;
        }

        public bool HasSecond()
        {
            return pair.Item2 != null;
        }

        public void Clear()
        {
            RemoveFirst();
            RemoveSecond();
            current_state = this.DetectState();
        }
    }
}
