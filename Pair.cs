using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
}
