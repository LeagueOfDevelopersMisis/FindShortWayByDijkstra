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
    public partial class Form1 : Form
    {
        static Action current_action = Action.ReadOnly;
        ViewGraphController controller;
        Graph graph;
        ImageList image_list = new ImageList();
        
        public Form1()
        {
            InitializeComponent();
            graph = new Graph();
            controller = new ViewGraphController(ref graph, ref pictureBox1);
            pictureBox1.MouseDown += new MouseEventHandler(Move_pictureBox1_MouseDown);
            pictureBox1.MouseHover += new EventHandler(pictureBox1_MouseHover);
            pictureBox1.MouseWheel += new MouseEventHandler(Wheel_pictureBox1_MouseWheel);
            image_list.Images.Add(new System.Drawing.Icon("accept.ico"));
            ToggleNamesToolStripMenuItem.Image = image_list.Images[0];
        }

        void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        void Wheel_pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            double prev = controller.Scale;
            if (prev > 4) {
                controller.Scale += ((double)e.Delta + 5) / 500;
            } else {
                controller.Scale += ((double)e.Delta + 5) / 1000;
            }
            Point move_point = new Point((e as MouseEventArgs).X, (e as MouseEventArgs).Y);
            if (move_point.X < pictureBox1.Width / 2)
                if (!(move_point.Y < pictureBox1.Height / 2))
                    controller.Offset = new Point(controller.Offset.X, controller.Offset.Y - Math.Sign(e.Delta) * controller.ConvertDevideValueWithScale(50));
            else
                if (move_point.Y < pictureBox1.Height / 2)
                    controller.Offset = new Point(controller.Offset.X - Math.Sign(e.Delta) * controller.ConvertDevideValueWithScale(50), controller.Offset.Y + Math.Sign(e.Delta) * controller.ConvertDevideValueWithScale(50));
                else
                    controller.Offset = new Point(controller.Offset.X - Math.Sign(e.Delta) * controller.ConvertDevideValueWithScale(150), controller.Offset.Y - Math.Sign(e.Delta) * controller.ConvertDevideValueWithScale(150));

            if (prev != controller.Scale)
                controller.Refresh();
        }

        private Point cur_start_move_point;
        void Move_pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (current_action == Action.DragAndDrop || current_action == Action.InsertVertex)
                return;
            cur_start_move_point = new Point((e as MouseEventArgs).X, (e as MouseEventArgs).Y);
            pictureBox1.MouseMove += new MouseEventHandler(Move_pictureBox1_MouseMove);
            pictureBox1.MouseUp += new MouseEventHandler(Move_pictureBox1_MouseUp);
        }

        void Move_pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (current_action == Action.DragAndDrop || current_action == Action.InsertVertex)
                return;
            Point move_point = new Point((e as MouseEventArgs).X, (e as MouseEventArgs).Y);
            Point offset_point = new Point(move_point.X - cur_start_move_point.X, move_point.Y - cur_start_move_point.Y);
            controller.Offset = new Point(
                controller.Offset.X + offset_point.X, 
                controller.Offset.Y + offset_point.Y
            );
                
            cur_start_move_point = move_point;
            controller.Refresh(graph.Length < 40);
        }

        void Move_pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (current_action == Action.DragAndDrop || current_action == Action.InsertVertex)
                return;
            Point move_point = new Point((e as MouseEventArgs).X, (e as MouseEventArgs).Y);
            Point offset_point = new Point(move_point.X - cur_start_move_point.X, move_point.Y - cur_start_move_point.Y);
            controller.Offset = new Point(
                controller.Offset.X + offset_point.X,
                controller.Offset.Y + offset_point.Y
            );
                
            pictureBox1.MouseMove -= Move_pictureBox1_MouseMove;
            pictureBox1.MouseUp -= Move_pictureBox1_MouseUp;
            controller.Refresh();
        }

        public static bool InsertedMetro { get; set; }

        public static Action GetCurrentAction()
        {
            return current_action;
        }

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InsertVertexsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_action == Action.ReadOnly)
            {
                ChangeAction(Action.InsertVertex);

                current_action = Action.InsertVertex;

                Button button_save = new Button();
                button_save.Text = "Сохранить";
                button_save.Location = new Point(12, 35);
                button_save.AutoSize = true;
                button_save.Click += new EventHandler(button_save_vertexes_Click);
                this.Controls.Add(button_save);

                pictureBox1.Click += new EventHandler(InsertVertex_pictureBox1_Click);
                controller.Refresh();
            }
            else
            {
                MessageBox.Show("Сохраните изменения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void button_save_vertexes_Click(object sender, EventArgs e)
        {
            ChangeAction(Action.ReadOnly);
            current_action = Action.ReadOnly;
            Button button = sender as Button;
            this.Controls.Remove(button);
            controller.Refresh();
        }

        private void InsertVertex_pictureBox1_Click(object sender, EventArgs e)
        {
            if (current_action == Action.InsertVertex)
            {
                Vertex vertex = new Vertex(
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).X - controller.ConvertValueWithScale(5)) - controller.Offset.X,
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).Y - controller.ConvertValueWithScale(5)) - controller.Offset.Y
                );
                graph.Add(vertex);
                controller.Refresh();
                SettingsVertexForm form = new SettingsVertexForm(ref controller, ref graph, graph.Vertexes.Count - 1);
                form.FormClosed += new FormClosedEventHandler((a, b) => {
                    controller.Refresh();
                });
                form.ShowDialog(this);
            }
        }

        public void ChangeAction(Action next)
        {
            switch (current_action) {
                case Action.InsertVertex:
                    pictureBox1.Click -= InsertVertex_pictureBox1_Click;
                    break;
                case Action.EditVertex:
                    pictureBox1.Click -= EditVertex_pictureBox1_Click;
                    break;
                case Action.LinkVertex:
                    pictureBox1.Click -= LinkVertex_pictureBox1_Click;
                    graph.DeselectAll();
                    linking_manager = null;
                    break;
                case Action.DragAndDrop:
                    pictureBox1.MouseDown -= SelectDragVertex_pictureBox1_MouseDown;
                    graph.DeselectAll();
                    break;
                case Action.SelectWay:
                    pictureBox1.Click -= SelectWayVertex_pictureBox1_Click;
                    controller.ResetWay();
                    controller.Refresh();
                    graph.DeselectAll();
                    this.Controls.Remove(reset_button);
                    reset_button = null;
                    select_way_manager = null;
                    break;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            controller.Refresh();
        }

        private void pictureBox1_ClientSizeChanged(object sender, EventArgs e)
        {
            if (controller != null)
                controller.Refresh();
        }

        private void ChangeVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_action == Action.ReadOnly)
            {
                ChangeAction(Action.EditVertex);

                current_action = Action.EditVertex;

                Button button_save = new Button();
                button_save.Text = "Сохранить";
                button_save.Location = new Point(12, 35);
                button_save.AutoSize = true;
                button_save.Click += new EventHandler(button_save_vertexes_Click);
                this.Controls.Add(button_save);

                pictureBox1.Click += new EventHandler(EditVertex_pictureBox1_Click);
                controller.Refresh();
            }
            else
            {
                MessageBox.Show("Сохраните изменения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EditVertex_pictureBox1_Click(object sender, EventArgs e)
        {
            if (current_action == Action.EditVertex)
            {
                Point point_click = new Point(
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).X) - controller.Offset.X,
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).Y) - controller.Offset.Y
                );
                Vertex vertex = graph.GetVertexByPoint(point_click);
                if ((object)vertex == null)
                    return;
                graph[graph.GetPositionById(vertex.ID)].Select();
                controller.Refresh();
                SettingsVertexForm form = new SettingsVertexForm(ref controller, ref graph, graph.GetPositionById(vertex.ID));
                form.FormClosed += new FormClosedEventHandler((a, b) => {
                    graph.DeselectAll();
                    controller.Refresh();
                });
                form.ShowDialog(this);
            }
        }

        private void LinkVertex_toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (current_action == Action.ReadOnly)
            {
                ChangeAction(Action.LinkVertex);

                current_action = Action.LinkVertex;

                Button button_save = new Button();
                button_save.Text = "Сохранить";
                button_save.Location = new Point(12, 35);
                button_save.AutoSize = true;
                button_save.Click += new EventHandler(button_save_vertexes_Click);
                this.Controls.Add(button_save);

                if (linking_manager == null)
                    linking_manager = new LinkVertexManager();

                pictureBox1.Click += new EventHandler(LinkVertex_pictureBox1_Click);
                
                controller.Refresh();
            }
            else
            {
                MessageBox.Show("Сохраните изменения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private LinkVertexManager linking_manager;

        private void LinkVertex_pictureBox1_Click(object sender, EventArgs e)
        {
            if (current_action == Action.LinkVertex)
            {
                Point point_click = new Point(
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).X) - controller.Offset.X,
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).Y) - controller.Offset.Y
                );
                Vertex clicked_vertex = graph.GetVertexByPoint(point_click);

                if ((object)clicked_vertex == null)
                    return;

                if (linking_manager.IsFirst(clicked_vertex)) {
                    linking_manager.RemoveFirst();
                    graph[graph.GetPositionById(clicked_vertex.ID)].Deselect();
                    controller.Refresh();
                    return;
                }

                if (linking_manager.IsSecond(clicked_vertex)) {
                    linking_manager.RemoveSecond();
                    graph[graph.GetPositionById(clicked_vertex.ID)].Deselect();
                    controller.Refresh();
                    return;
                }

                if (linking_manager.CurrentState == LinkVertexManager.LinkedState.SelectedTwo) {
                    graph[graph.GetPositionById(linking_manager.Pair.Item2.ID)].Deselect();
                    linking_manager.RemoveSecond();
                }

                SettingsEdgeForm edge_form;

                switch (linking_manager.CurrentState) {
                    case LinkVertexManager.LinkedState.NoSelect:
                        linking_manager.InsertFirst(clicked_vertex);
                        graph[graph.GetPositionById(clicked_vertex.ID)].Select();
                        break;
                    case LinkVertexManager.LinkedState.SelectedOne:
                        linking_manager.InsertSecond(clicked_vertex);
                        graph[graph.GetPositionById(clicked_vertex.ID)].Select();
                        controller.Refresh();

                        edge_form = new SettingsEdgeForm(ref controller, ref graph, ref linking_manager);
                        edge_form.FormClosed += new FormClosedEventHandler((a, b) => {
                            graph[graph.GetPositionById(linking_manager.Pair.Item1.ID)].Deselect();
                            linking_manager.RemoveFirst();
                            graph[graph.GetPositionById(linking_manager.Pair.Item2.ID)].Deselect();
                            linking_manager.RemoveSecond();
                            controller.Refresh();
                        });
                        edge_form.ShowDialog(this);
                        break;
                }
                controller.Refresh();
            }
        }

        private void DragAndDropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_action == Action.ReadOnly)
            {
                ChangeAction(Action.DragAndDrop);

                current_action = Action.DragAndDrop;

                Button button_save = new Button();
                button_save.Text = "Сохранить";
                button_save.Location = new Point(12, 35);
                button_save.AutoSize = true;
                button_save.Click += new EventHandler(button_save_vertexes_Click);
                this.Controls.Add(button_save);

                pictureBox1.MouseDown += new MouseEventHandler(SelectDragVertex_pictureBox1_MouseDown);

                controller.Refresh();
            }
            else
            {
                MessageBox.Show("Сохраните изменения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private Vertex current_drag;
        private void SelectDragVertex_pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (current_action == Action.DragAndDrop)
            {
                Point point_click = new Point(
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).X) - controller.Offset.X,
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).Y) - controller.Offset.Y
                );
                Vertex clicked_vertex = graph.GetVertexByPoint(point_click);

                if ((object)clicked_vertex == null)
                    return;

                current_drag = clicked_vertex;

                graph[graph.GetPositionById(clicked_vertex.ID)].Select();
                controller.Refresh();
                pictureBox1.MouseMove += new MouseEventHandler(Drag_pictureBox1_MouseMove);
                pictureBox1.MouseUp += new MouseEventHandler(Drop_pictureBox1_MouseUp);
            }
        }

        void Drag_pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_mouse = new Point((e as MouseEventArgs).X, (e as MouseEventArgs).Y);
            if (point_mouse.X < 0)
                point_mouse.X = 0;
            if (point_mouse.X > pictureBox1.Width)
                point_mouse.X = pictureBox1.Width - 10;
            if (point_mouse.Y < 0)
                point_mouse.Y = 0;
            if (point_mouse.Y > pictureBox1.Height)
                point_mouse.Y = pictureBox1.Height - 10;

            point_mouse = new Point(
                controller.ConvertDevideValueWithScale(point_mouse.X - controller.ConvertValueWithScale(5)) - controller.Offset.X,
                controller.ConvertDevideValueWithScale(point_mouse.Y - controller.ConvertValueWithScale(5)) - controller.Offset.Y
            );

            graph[graph.GetPositionById(current_drag.ID)].SetCoord(point_mouse);
            controller.Refresh(graph.Length < 40);
        }

        void Drop_pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point point_mouse = new Point((e as MouseEventArgs).X, (e as MouseEventArgs).Y);
            if (point_mouse.X < 0)
                point_mouse.X = 0;
            if (point_mouse.X > pictureBox1.Width)
                point_mouse.X = pictureBox1.Width - 10;
            if (point_mouse.Y < 0)
                point_mouse.Y = 0;
            if (point_mouse.Y > pictureBox1.Height)
                point_mouse.Y = pictureBox1.Height - 10;

            point_mouse = new Point(
                controller.ConvertDevideValueWithScale(point_mouse.X - controller.ConvertValueWithScale(5)) - controller.Offset.X,
                controller.ConvertDevideValueWithScale(point_mouse.Y - controller.ConvertValueWithScale(5)) - controller.Offset.Y
            );
            
            graph[graph.GetPositionById(current_drag.ID)].SetCoord(point_mouse);
            graph[graph.GetPositionById(current_drag.ID)].Deselect();
            pictureBox1.MouseUp -= Drop_pictureBox1_MouseUp;
            pictureBox1.MouseMove -= Drag_pictureBox1_MouseMove;
            controller.Refresh();
        }

        private void ResetScalingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.Offset = new Point(0, 0);
            controller.Scale = 1.0;
            controller.Refresh();
        }

        private void ToggleNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ToggleNamesToolStripMenuItem.Image != null)
            {
                ToggleNamesToolStripMenuItem.Image = null;
                controller.VisibleNames = false;
            }
            else
            {
                ToggleNamesToolStripMenuItem.Image = image_list.Images[0];
                controller.VisibleNames = true;
            }
            controller.Refresh();
        }

        Button reset_button;
        private void RouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_action == Action.ReadOnly)
            {
                ChangeAction(Action.SelectWay);

                current_action = Action.SelectWay;

                Button button_save = new Button();
                button_save.Text = "Готово";
                button_save.Location = new Point(12, 35);
                button_save.AutoSize = true;
                button_save.Click += new EventHandler(button_save_vertexes_Click);

                reset_button = new Button();
                reset_button.Text = "Сбросить путь";
                reset_button.Location = new Point(90, 35);
                reset_button.AutoSize = true;
                reset_button.Enabled = false;
                reset_button.Click += new EventHandler((s, a) =>
                {
                    Button btn = s as Button;
                    btn.Enabled = false;
                    controller.ResetWay();
                    graph.DeselectAll();
                    select_way_manager.RemoveFirst();
                    select_way_manager.RemoveSecond();
                    controller.Refresh();
                });
                this.Controls.Add(button_save);
                this.Controls.Add(reset_button);

                if (select_way_manager == null)
                    select_way_manager = new LinkVertexManager();

                pictureBox1.Click += new EventHandler(SelectWayVertex_pictureBox1_Click);

                controller.Refresh();
            }
            else
            {
                MessageBox.Show("Сохраните изменения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private LinkVertexManager select_way_manager;
        void SelectWayVertex_pictureBox1_Click(object sender, EventArgs e)
        {
            if (current_action == Action.SelectWay)
            {
                Point point_click = new Point(
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).X) - controller.Offset.X,
                    controller.ConvertDevideValueWithScale((e as MouseEventArgs).Y) - controller.Offset.Y
                );
                Vertex clicked_vertex = graph.GetVertexByPoint(point_click);

                if ((object)clicked_vertex == null)
                    return;

                if (select_way_manager.IsFirst(clicked_vertex))
                {
                    if (select_way_manager.CurrentState == LinkVertexManager.LinkedState.SelectedTwo)
                        select_way_manager.Swap();
                    select_way_manager.RemoveSecond();
                    reset_button.Enabled = false;
                    graph[graph.GetPositionById(clicked_vertex.ID)].Deselect();
                    controller.ResetWay();
                    controller.Refresh();
                    return;
                }

                if (select_way_manager.IsSecond(clicked_vertex))
                {
                    select_way_manager.RemoveSecond();
                    reset_button.Enabled = false;
                    graph[graph.GetPositionById(clicked_vertex.ID)].Deselect();
                    controller.ResetWay();
                    controller.Refresh();
                    return;
                }

                if (select_way_manager.CurrentState == LinkVertexManager.LinkedState.SelectedTwo)
                {
                    graph[graph.GetPositionById(select_way_manager.Pair.Item2.ID)].Deselect();
                    reset_button.Enabled = false;
                    select_way_manager.RemoveSecond();
                }

                switch (select_way_manager.CurrentState)
                {
                    case LinkVertexManager.LinkedState.NoSelect:
                        select_way_manager.InsertFirst(clicked_vertex);
                        reset_button.Enabled = true;
                        graph[graph.GetPositionById(clicked_vertex.ID)].Select();
                        break;
                    case LinkVertexManager.LinkedState.SelectedOne:
                        select_way_manager.InsertSecond(clicked_vertex);
                        reset_button.Enabled = true;
                        graph[graph.GetPositionById(clicked_vertex.ID)].Select();
                        DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(ref graph);
                        List<Vertex> short_way = dijkstra.GetShortRoute(select_way_manager.Pair.Item1.ID, select_way_manager.Pair.Item2.ID);
                        if (short_way.Count == 0) {
                            MessageBox.Show("Невозможно проложить путь от указанной вершины");
                            controller.ResetWay();
                            reset_button.Enabled = false;
                            select_way_manager.Clear();
                            graph.DeselectAll();
                            controller.Refresh();
                            return;
                        }
                        controller.SelectWay(short_way);
                        controller.ApplyWay();
                        break;
                }
                controller.Refresh();
            }
        }

        private void ImportExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Settings(ref controller, ref graph);
            form.ShowDialog();
        }

        private void DeleteCurrentGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph = new Graph();
            controller = new ViewGraphController(ref graph, ref pictureBox1);
            controller.Refresh();
            InsertedMetro = false;
        }
    }
}