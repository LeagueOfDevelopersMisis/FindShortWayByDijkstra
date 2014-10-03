using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SearchWay
{
    partial class SettingsVertexForm : Form
    {
        private ViewGraphController controller;
        private Graph graph;
        private int position;
        private int selected_id = -1;
        private int selected_index = -1;

        public SettingsVertexForm(ref ViewGraphController controller, ref Graph graph, int position)
        {
            InitializeComponent();
            this.controller = controller;
            this.graph = graph;
            this.position = position;
            List<Edge> list = graph.GetVertexEdges(graph[position].ID);
            List<Vertex> listVertex = new List<Vertex>();
            Vertex vertex;
            int current_id = graph[position].ID;
            foreach (Edge edge in list)
            {
                vertex = graph.GetVertexById(edge.FirstID != current_id ? edge.FirstID : edge.SecondID);
                listVertex.Add(vertex);
            }
            InsertListToCombo(ref comboBox1, listVertex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog(this);
            Color color = colorDialog1.Color;
            pictureBox1.CreateGraphics().Clear(color);
            graph[position].GetView.Color = color;
            controller.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0) {
                graph[position].GetView.Name = textBox1.Text;
                controller.Refresh();
                (sender as Button).Visible = false;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            button2.Visible = true;
            if (textBox1.Text.Length != 0)
            {
                graph[position].GetView.Name = textBox1.Text;
                controller.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            graph.Remove(position);
            this.Close();
        }

        private void SettingsVertexForm_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.CreateGraphics().Clear(graph[position].GetView.Color);
            textBox1.Text = graph[position].GetView.Name;

            if (selected_id != -1) {
                int current_id = graph[position].ID;
                Edge current_edge = graph.GetEdgeBetweenVertexes(selected_id, current_id);
                if ((object)current_edge != null)
                {
                    pictureBox2.CreateGraphics().Clear(current_edge.View.Color);
                    numericUpDown1.Value = current_edge.Weight;
                }
            }
        }

        private void InsertListToCombo(ref ComboBox comboBox, List<Vertex> list)
        {
            int current_id = graph[position].ID;
            List<Vertex> vertexes = list;
            
            comboBox.DataSource = vertexes;
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "ID";
            if (vertexes.Count != 0)
            {
                comboBox.Enabled = true;
                pictureBox2.Enabled = true;
                button5.Enabled = true;
                numericUpDown1.Enabled = true;
                label3.Enabled = true;
                comboBox.SelectedIndex = 0;
                this.selected_id = ((Vertex)comboBox.SelectedItem).ID;
                this.selected_index = comboBox.SelectedIndex;
                numericUpDown1.Value = graph.GetEdgeBetweenVertexes(current_id, selected_id).Weight;
                comboBox.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            }
            else
            {
                this.selected_id = -1;
                this.selected_index = -1;
                comboBox.Enabled = false;
                pictureBox2.Enabled = false;
                button5.Enabled = false;
                numericUpDown1.Enabled = false;
                label3.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                this.selected_id = ((Vertex)comboBox.SelectedItem).ID;
                this.selected_index = comboBox.SelectedIndex;
                int current_id = graph[position].ID;
                Edge current_edge = graph.GetEdgeBetweenVertexes(selected_id, current_id);
                pictureBox2.CreateGraphics().Clear(current_edge.View.Color);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int current_id = graph[position].ID;
            Edge current_edge = graph.GetEdgeBetweenVertexes(selected_id, current_id);
            colorDialog2.ShowDialog();
            current_edge.View.Color = colorDialog2.Color;
            controller.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int current_id = graph[position].ID;
            graph.RemoveEdgeBetweenVortex(current_id, this.selected_id);
            List<Vertex> list = (List<Vertex>)comboBox1.DataSource;
            list.RemoveAt(this.selected_index);
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            InsertListToCombo(ref comboBox1, list);
            controller.Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (selected_id != -1)
            {
                int current_id = graph[position].ID;
                graph.SetWeightByIds(selected_id, current_id, (int)numericUpDown1.Value);
            }
        }
    }
}
