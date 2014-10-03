using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SearchWay
{
    class Vertex
    {
        private Point point;
        ViewVertex view;
        private int id;

        public Vertex(int x, int y)
        {
            this.point = new Point(x, y);
            this.view = ViewVertex.CreateView(Color.LightPink);
        }

        public Vertex(int x, int y, Color color, int width = 10)
        {
            this.point = new Point(x, y);
            this.view = ViewVertex.CreateView(color, width);
        }

        public Point GetCoord
        {
            get { return point; }
        }

        public ViewVertex GetView
        {
            get { return view; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public bool Contain(Point point_click)
        {
            PointF p = new PointF(point_click.X, point_click.Y);
            PointF center = new PointF(point.X + 5, point.Y + 5);
            double dist = Math.Sqrt((p.X - center.X) * (p.X - center.X) + (p.Y - center.Y) * (p.Y - center.Y));
            if (dist <= 6) {
                return true;
            }
            return false;
        }

        public void Select()
        {
            view.Select = true;
        }

        public void Deselect()
        {
            view.Select = false;
        }

        public static bool operator ==(Vertex first, Vertex second) 
        {
            return (object)first != null && (object)second != null && first.ID == second.ID;
        }

        public static bool operator !=(Vertex first, Vertex second)
        {
            if ((object)first == null || (object)second == null)
                return true;
            return first.ID != second.ID;
        }

        public bool Selected
        {
            get { return view.Select; }
        }

        public string Name
        {
            get { return view.Name == "Вершина" ? view.Name + " " + id : view.Name; }
        }

        public void SetCoord(Point point)
        {
            this.point = point;
        }
    }
}
