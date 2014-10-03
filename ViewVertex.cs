using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SearchWay
{
    public enum Dir {
        Right,
        Left
    }

    class ViewVertex
    {
        Color color;
        private int width;
        private string name = "Вершина";
        private bool selected;
        private Dir direction;

        static Color default_color = Color.Black;

        private ViewVertex(Color color, int width) {
            this.color = color;
            this.width = width;
            this.selected = false;
            this.direction = Dir.Right;
        }

        public static ViewVertex CreateView(Color color, int width = 5)
        {
            return new ViewVertex(color, width);
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Dir TextDirection
        {
            get { return direction; }
            set { direction = value; }
        }

        public int GetWidth
        {
            get { return width; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Select
        {
            get { return selected; }
            set { selected = value; }
        }
    }
}
