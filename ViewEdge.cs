using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SearchWay
{
    class ViewEdge
    {
        Color color;
        private int width;

        static Color default_color = Color.Black;

        private ViewEdge(Color color, int width) {
            this.color = color;
            this.width = width;
        }

        public static ViewEdge CreateView(Color color, int width = 6)
        {
            return new ViewEdge(color, width);
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public int Width
        {
            get { return width; }
        }
    }
}
