using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SearchWay
{
    partial class SettingsEdgeForm : Form
    {
        private ViewGraphController controller;
        private Graph graph;
        private LinkVertexManager linking_manager;
        private Color selected_color;
        private int weight = 10;

        public SettingsEdgeForm(ref ViewGraphController controller, ref Graph graph, ref LinkVertexManager linking_manager)
        {
            InitializeComponent();
            this.controller = controller;
            this.graph = graph;
            this.linking_manager = linking_manager;
            selected_color = Color.LightGreen;
        }

        private void SelectColorEdge_button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            this.selected_color = colorDialog1.Color;
            pictureBox1.CreateGraphics().Clear(this.selected_color);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SelectColorEdge_button1_Click(null, null);
        }

        private void SettingsEdgeForm_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.CreateGraphics().Clear(selected_color);
        }

        private void Ok_button2_Click(object sender, EventArgs e)
        {
            Edge new_edge = new Edge(linking_manager.Pair.Item1, linking_manager.Pair.Item2, this.weight);
            new_edge.View.Color = selected_color;
            graph.AddEdge(new_edge);
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.weight = (int)numericUpDown1.Value;
        }
    }
}
