using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SearchWay
{
    partial class Settings : Form
    {
        private Graph graph;
        private ViewGraphController controller;

        public Settings(ref ViewGraphController controller, ref Graph graph)
        {
            InitializeComponent();
            this.graph = graph;
            this.controller = controller;
            if (Form1.InsertedMetro)
            {
                SelectFile_button1.Enabled = false;
                SelectFile_button1.Text = "Карта метро загружена";
            }
        }

        private void SelectFile_button1_Click(object sender, EventArgs e)
        {
            if (Form1.InsertedMetro) {
                return;
            }
            string path = @"./Metro.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            List<Vertex> vertexes = new List<Vertex>();
            List<Edge> edges = new List<Edge>();
            foreach (XmlNode node in xml.SelectNodes("/data/vertexes/vertex"))
            {
                Vertex vertex;
                int id;
                Point location = new Point();
                Color color = Color.Black;
                string name = string.Empty;
                Dir direction = Dir.Right;
                foreach (XmlNode vertex_param in node.ChildNodes)
                {
                    if (vertex_param.FirstChild.NodeType == XmlNodeType.Text)
                    {
                        switch (vertex_param.Name)
                        {
                            case "id":
                                int.TryParse(vertex_param.FirstChild.Value, out id);
                                break;
                            case "point":
                                string[] str_point = vertex_param.FirstChild.Value.Split(',');
                                location = new Point(int.Parse(str_point[0]), int.Parse(str_point[1]));
                                break;
                            case "color":
                                string[] str_color = vertex_param.FirstChild.Value.Split(',');
                                color = Color.FromArgb(
                                    int.Parse(str_color[0]),
                                    int.Parse(str_color[1]),
                                    int.Parse(str_color[2])
                                );
                                break;
                            case "name":
                                name = vertex_param.FirstChild.Value;
                                break;
                            case "direction":
                                if (vertex_param.FirstChild.Value == "left")
                                    direction = Dir.Left;
                                break;
                        }
                    }
                }
                vertex = new Vertex(location.X, location.Y, color);
                vertex.GetView.Name = name;
                vertex.GetView.TextDirection = direction;
                vertexes.Add(vertex);
            }

            foreach (XmlNode node in xml.SelectNodes("/data/edges/edge"))
            {
                Edge edge;
                int first_id = -1, second_id = -1, weight = 10;
                Color color = Color.Black;
                foreach (XmlNode edge_param in node.ChildNodes)
                {
                    if (edge_param.FirstChild.NodeType == XmlNodeType.Text)
                    {
                        switch (edge_param.Name)
                        {
                            case "first":
                                int.TryParse(edge_param.FirstChild.Value, out first_id);
                                break;
                            case "second":
                                int.TryParse(edge_param.FirstChild.Value, out second_id);
                                break;
                            case "weight":
                                int.TryParse(edge_param.FirstChild.Value, out weight);
                                break;
                            case "color":
                                string[] str_color = edge_param.FirstChild.Value.Split(',');
                                color = Color.FromArgb(
                                    int.Parse(str_color[0]),
                                    int.Parse(str_color[1]),
                                    int.Parse(str_color[2])
                                );
                                break;
                        }
                    }
                }
                edge = new Edge(first_id, second_id, weight);
                edge.View.Color = color;
                edges.Add(edge);
            }
            AddVertexes(vertexes);
            AddEdges(edges);
            controller.Refresh();
            SelectFile_button1.Enabled = false;
            Form1.InsertedMetro = true;
            SelectFile_button1.Text = "Карта метро загружена";
        }

        private void AddVertexes(List<Vertex> list)
        {
            foreach (Vertex vertex in list)
                graph.Add(vertex);
        }

        private void AddEdges(List<Edge> list)
        {
            foreach (Edge edge in list)
                graph.AddEdge(edge);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog(this);
            string path = folderBrowserDialog1.SelectedPath;
            string name = @"\Metro.xml";
            if (path == null || path == string.Empty)
                return;
            StreamWriter file = new StreamWriter(path + name);

            int id = 1;

            file.WriteLine("<data>");
            file.WriteLine("\t<vertexes>");
            foreach (Vertex vertex in graph.Vertexes) {
                file.WriteLine("\t\t<vertex>");
                file.WriteLine("\t\t\t<id>{0}</id>", id++);
                file.WriteLine("\t\t\t<color>{0},{1},{2}</color>", vertex.GetView.Color.R, vertex.GetView.Color.G, vertex.GetView.Color.B);
                file.WriteLine("\t\t\t<name>{0}</name>", vertex.Name);
                file.WriteLine("\t\t\t<point>{0},{1}</point>", vertex.GetCoord.X, vertex.GetCoord.Y);
                file.WriteLine("\t\t\t<direction>{0}</direction>", vertex.GetView.TextDirection == Dir.Left ? "left" : "right");
                file.WriteLine("\t\t</vertex>");
            }
            file.WriteLine("\t</vertexes>");

            file.WriteLine("\t<edges>");
            foreach (Edge edge in graph.Edges)
            {
                file.WriteLine("\t\t<edge>");
                file.WriteLine("\t\t\t<first>{0}</first>", edge.FirstID);
                file.WriteLine("\t\t\t<second>{0}</second>", edge.SecondID);
                file.WriteLine("\t\t\t<weight>{0}</weight>", edge.Weight);
                file.WriteLine("\t\t\t<color>{0},{1},{2}</color>", edge.View.Color.R, edge.View.Color.G, edge.View.Color.B);
                file.WriteLine("\t\t</edge>");
            }
            file.WriteLine("\t</edges>");
            file.WriteLine("</data>");

            file.Close();
            this.Close();
        }
    }
}
