using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SearchWay
{
    class ViewGraphController
    {
        private Graph graph;
        private PictureBox draw;
        private Point offset;
        private double scale;
        private bool visible_names;
        private bool select_way;
        private List<Vertex> short_way;

        public ViewGraphController(ref Graph graph, ref PictureBox draw)
        {
            this.graph = graph;
            this.draw = draw;
            offset = new Point(300, 0);
            scale = 0.62;
            visible_names = true;
            select_way = false;
        }

        public void ApplyWay()
        {
            select_way = short_way != null;
        }

        public void ResetWay()
        {
            select_way = false;
            short_way = null;
            //graph.DeselectAll();
        }

        public void SelectWay(List<Vertex> list)
        {
            short_way = list;
        }

        public bool VisibleNames
        {
            get { return visible_names; }
            set { visible_names = value; }
        }

        public Point Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public double Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                if (scale > 10) 
                    scale = 10;
                if (scale < 0.4)
                    scale = 0.375;
                if (Math.Abs(scale - 1) <= 0.1) {
                    scale = 1;
                }
            }
        }

        public void Generate(bool type = true)
        {
            if (graph != null && !graph.isEmpty())
            {
                Graphics g = draw.CreateGraphics();

                Edge edge;
                int alpha = 255;
                for (int i = 0; i < graph.Edges.Count; ++i)
                {
                    edge = (Edge)graph.Edges[i];
                    Vertex first = graph.GetVertexById(edge.FirstID),
                    second = graph.GetVertexById(edge.SecondID);
                    if ((object)first != null && (object)second != null) {
                        Random rnd = new Random(12);
                        Point a = new Point(first.GetCoord.X + 5, first.GetCoord.Y + 5);
                        Point b = new Point(second.GetCoord.X + 5, second.GetCoord.Y + 5);

                        double dist = Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
                        int sm = (int)(Math.Log(dist));
                        if (sm > 60) {
                            sm = 60;
                        }
                        if (sm < 0) {
                            sm = 1;
                        }
                        Point center = new Point((int)(a.X + b.X) / 2 + rnd.Next(0, sm) - sm / 2, (int)(a.Y + b.Y) / 2 + rnd.Next(0, sm) - sm / 2);
                        a = ConvertCoordWithScale(new Point(a.X + offset.X, a.Y + offset.Y));
                        b = ConvertCoordWithScale(new Point(b.X + offset.X, b.Y + offset.Y));
                        center = ConvertCoordWithScale(new Point(center.X + offset.X, center.Y + offset.Y));
                        g.DrawBezier(
                            new Pen(
                                new SolidBrush(
                                    edge.View.Color
                                ),
                                ConvertValueWithScale(edge.View.Width)
                            ),
                            a, center, center, b
                        );
                    }
                }

                Vertex vertex;
                for (int i = 0; i < graph.Length; ++i)
                {
                    vertex = graph[i];
                    if (vertex.Selected) {
                        g.FillEllipse(
                            new SolidBrush(Color.LightSkyBlue),
                            new Rectangle(
                                ConvertCoordWithScale(new Point(vertex.GetCoord.X - 5 + offset.X, vertex.GetCoord.Y - 5 + offset.Y)), 
                                ConvertSizeWithScale(new Size(20, 20))
                            )
                        );
                        g.DrawEllipse(
                            new Pen(Color.Black, 1),
                            new Rectangle(
                                ConvertCoordWithScale(new Point(vertex.GetCoord.X - 5 + offset.X, vertex.GetCoord.Y - 5 + offset.Y)), 
                                ConvertSizeWithScale(new Size(20, 20))
                            )
                        );
                    }
                    g.FillEllipse(
                        new SolidBrush(
                            Color.FromArgb(alpha, vertex.GetView.Color.R, vertex.GetView.Color.G, vertex.GetView.Color.B)
                        ),
                        new Rectangle(
                            ConvertCoordWithScale(new Point(vertex.GetCoord.X + offset.X, vertex.GetCoord.Y + offset.Y)),
                            ConvertSizeWithScale(new Size(10, 10))
                        )
                    );
                    g.DrawEllipse(
                        new Pen(
                            Color.Black, 1
                        ),
                        new Rectangle(
                            ConvertCoordWithScale(new Point(vertex.GetCoord.X + offset.X, vertex.GetCoord.Y + offset.Y)),
                            ConvertSizeWithScale(new Size(10, 10))
                        )
                    );
                    if (VisibleNames && type) {
                        SizeF text_size = g.MeasureString(
                            vertex.GetView.Name == "Вершина"
                            ? vertex.GetView.Name + " " + vertex.ID
                            : vertex.GetView.Name,
                            new Font("Segoe UI", ConvertValueWithScale(9) != 0 ? ConvertValueWithScale(9) : 1)
                        );
                        SolidBrush brush_cover = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                        if (vertex.Selected)
                            brush_cover = new SolidBrush(Color.FromArgb(160, 200, 255, 255));
                        Size size_cover = ConvertSizeWithScale(new Size(ConvertDevideValueWithScale((int)text_size.Width), 19));
                        if (vertex.GetView.TextDirection == Dir.Left)
                        {
                            g.FillRectangle(
                                brush_cover,
                                new Rectangle(
                                    ConvertCoordWithScale(new Point(vertex.GetCoord.X - (vertex.Selected ? 4 : 0) + offset.X - ConvertDevideValueWithScale((int)text_size.Width), vertex.GetCoord.Y - 4 + offset.Y)),
                                    size_cover
                                )
                            );
                            g.DrawString(
                                vertex.GetView.Name == "Вершина"
                                ? vertex.GetView.Name + " " + vertex.ID
                                : vertex.GetView.Name,
                                new Font("Segoe UI", ConvertValueWithScale(9) != 0 ? ConvertValueWithScale(9) : 1),
                                new SolidBrush(Color.Black),
                                ConvertCoordWithScale(new Point(vertex.GetCoord.X - (vertex.Selected ? 4 : 0) + offset.X - ConvertDevideValueWithScale((int)text_size.Width), vertex.GetCoord.Y - 6 + offset.Y))
                            );
                        }
                        else
                        {
                            g.FillRectangle(
                                brush_cover,
                                new Rectangle(
                                    ConvertCoordWithScale(new Point(vertex.GetCoord.X + (vertex.Selected ? 15 : 11) + offset.X, vertex.GetCoord.Y - 3 + offset.Y)),
                                    size_cover
                                )
                            );
                            g.DrawString(
                                vertex.GetView.Name == "Вершина"
                                ? vertex.GetView.Name + " " + vertex.ID
                                : vertex.GetView.Name,
                                new Font("Segoe UI", ConvertValueWithScale(9) != 0 ? ConvertValueWithScale(9) : 1),
                                new SolidBrush(Color.Black),
                                ConvertCoordWithScale(new Point(vertex.GetCoord.X + (vertex.Selected ? 15 : 11) + offset.X, vertex.GetCoord.Y - 5 + offset.Y))
                            );
                        }
                    }
                }
                if (select_way)
                {
                    g.FillRectangle(
                        new SolidBrush(Color.FromArgb(200, 255, 255, 255)),
                        new Rectangle(0, 0, draw.Width, draw.Height)
                    );
                    GenerateSelectedWay(type);
                }
            }
            DrawCurrentIndicatorScale();
            
        }

        Label label;
        public void DrawCurrentIndicatorScale()
        {
            if (label == null) {
                label = new Label();
                label.AutoSize = true;
                label.Text = "Масштаб: 1:" + Math.Round(scale, 2);
                label.Location = new Point(10, draw.Height - 30);
                draw.Controls.Add(label);
            }
            label.Text = "Масштаб: 1:" + Math.Round(scale, 2);
            label.Location = new Point(10, draw.Height - 30);
        }

        private void GenerateSelectedWay(bool type = true)
        {
            Graphics g = draw.CreateGraphics();

            for (int i = 1; i < short_way.Count; ++i)
            {
                Vertex first = short_way[i - 1],
                second = short_way[i];
                Edge edge = graph.GetEdgeBetweenVertexes(first.ID, second.ID);
                if ((object)first != null && (object)second != null)
                {
                    Random rnd = new Random(12);
                    Point a = new Point(first.GetCoord.X + 5, first.GetCoord.Y + 5);
                    Point b = new Point(second.GetCoord.X + 5, second.GetCoord.Y + 5);

                    double dist = Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
                    int sm = (int)(Math.Log(dist));
                    if (sm > 60)
                    {
                        sm = 60;
                    }
                    if (sm < 0)
                    {
                        sm = 1;
                    }
                    Point center = new Point((int)(a.X + b.X) / 2 + rnd.Next(0, sm) - sm / 2, (int)(a.Y + b.Y) / 2 + rnd.Next(0, sm) - sm / 2);
                    a = ConvertCoordWithScale(new Point(a.X + offset.X, a.Y + offset.Y));
                    b = ConvertCoordWithScale(new Point(b.X + offset.X, b.Y + offset.Y));
                    center = ConvertCoordWithScale(new Point(center.X + offset.X, center.Y + offset.Y));
                    g.DrawBezier(
                        new Pen(
                            new SolidBrush(
                                Color.FromArgb(255, edge.View.Color.R, edge.View.Color.G, edge.View.Color.B)
                            ),
                            ConvertValueWithScale(edge.View.Width)
                        ),
                        a, center, center, b
                    );
                }
            }

            Vertex vertex;
            for (int i = 0; i < short_way.Count; ++i)
            {
                vertex = short_way[i];
                if (vertex.Selected)
                {
                    g.FillEllipse(
                        new SolidBrush(Color.LightSkyBlue),
                        new Rectangle(
                            ConvertCoordWithScale(new Point(vertex.GetCoord.X - 5 + offset.X, vertex.GetCoord.Y - 5 + offset.Y)),
                            ConvertSizeWithScale(new Size(20, 20))
                        )
                    );
                    g.DrawEllipse(
                        new Pen(Color.Black, 1),
                        new Rectangle(
                            ConvertCoordWithScale(new Point(vertex.GetCoord.X - 5 + offset.X, vertex.GetCoord.Y - 5 + offset.Y)),
                            ConvertSizeWithScale(new Size(20, 20))
                        )
                    );
                }
                g.FillEllipse(
                    new SolidBrush(
                        Color.FromArgb(255, vertex.GetView.Color.R, vertex.GetView.Color.G, vertex.GetView.Color.B)
                    ),
                    new Rectangle(
                        ConvertCoordWithScale(new Point(vertex.GetCoord.X + offset.X, vertex.GetCoord.Y + offset.Y)),
                        ConvertSizeWithScale(new Size(10, 10))
                    )
                );
                g.DrawEllipse(
                    new Pen(
                        Color.FromArgb(255, 0, 0, 0), 1
                    ),
                    new Rectangle(
                        ConvertCoordWithScale(new Point(vertex.GetCoord.X + offset.X, vertex.GetCoord.Y + offset.Y)),
                        ConvertSizeWithScale(new Size(10, 10))
                    )
                );
                if (VisibleNames && type)
                {
                    SizeF text_size = g.MeasureString(
                        vertex.GetView.Name == "Вершина"
                        ? vertex.GetView.Name + " " + vertex.ID
                        : vertex.GetView.Name,
                        new Font("Segoe UI", ConvertValueWithScale(9) != 0 ? ConvertValueWithScale(9) : 1)
                    );
                    SolidBrush brush_cover = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
                    if (vertex.Selected)
                        brush_cover = new SolidBrush(Color.FromArgb(160, 200, 255, 255));
                    Size size_cover = ConvertSizeWithScale(new Size(ConvertDevideValueWithScale((int)text_size.Width), 19));
                    if (vertex.GetView.TextDirection == Dir.Left)
                    {
                        g.FillRectangle(
                            brush_cover,
                            new Rectangle(
                                ConvertCoordWithScale(new Point(vertex.GetCoord.X - (vertex.Selected ? 4 : 0) + offset.X - ConvertDevideValueWithScale((int)text_size.Width), vertex.GetCoord.Y - 4 + offset.Y)),
                                size_cover
                            )
                        );
                        g.DrawString(
                            vertex.GetView.Name == "Вершина"
                            ? vertex.GetView.Name + " " + vertex.ID
                            : vertex.GetView.Name,
                            new Font("Segoe UI", ConvertValueWithScale(9) != 0 ? ConvertValueWithScale(9) : 1),
                            new SolidBrush(Color.FromArgb(255, 0, 0, 0)),
                            ConvertCoordWithScale(new Point(vertex.GetCoord.X - (vertex.Selected ? 4 : 0) + offset.X - ConvertDevideValueWithScale((int)text_size.Width), vertex.GetCoord.Y - 6 + offset.Y))
                        );
                    }
                    else
                    {
                        g.FillRectangle(
                            brush_cover,
                            new Rectangle(
                                ConvertCoordWithScale(new Point(vertex.GetCoord.X + (vertex.Selected ? 15 : 11) + offset.X, vertex.GetCoord.Y - 3 + offset.Y)),
                                size_cover
                            )
                        );
                        g.DrawString(
                            vertex.GetView.Name == "Вершина"
                            ? vertex.GetView.Name + " " + vertex.ID
                            : vertex.GetView.Name,
                            new Font("Segoe UI", ConvertValueWithScale(9) != 0 ? ConvertValueWithScale(9) : 1),
                            new SolidBrush(Color.FromArgb(255, 0, 0, 0)),
                            ConvertCoordWithScale(new Point(vertex.GetCoord.X + (vertex.Selected ? 15 : 11) + offset.X, vertex.GetCoord.Y - 5 + offset.Y))
                        );
                    }
                }
            }
        }

        public Point ConvertCoordWithScale(Point point)
        {
            return new Point((int)(this.scale * point.X), (int)(this.scale * point.Y));
        }

        public Point ConvertDevideCoordWithScale(Point point)
        {
            return new Point((int)(point.X / (double)this.scale), (int)(point.Y / (double)this.scale));
        }

        public int ConvertValueWithScale(int value)
        {
            return (int)(this.scale * value);
        }

        public int ConvertDevideValueWithScale(int value)
        {
            return (int)(value / (double)this.scale);
        }

        public int ConvertDevideValueWithScale(double value)
        {
            return (int)(value / (double)this.scale);
        }

        public Size ConvertSizeWithScale(Size size)
        {
            return new Size(ConvertValueWithScale(size.Width), ConvertValueWithScale(size.Height));
        }

        public void ClearCanvas()
        {
            draw.CreateGraphics().Clear(SystemColors.Window);
        }

        public void Refresh(bool type = true)
        {
            ClearCanvas();
            Generate(type);
        }
    }
}
