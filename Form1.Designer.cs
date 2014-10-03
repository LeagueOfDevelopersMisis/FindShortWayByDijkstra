namespace SearchWay
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteCurrentGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.графToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertVertexsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinkVertex_toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DragAndDropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetScalingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(923, 476);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_ClientSizeChanged);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.SettingsToolStripMenuItem,
            this.графToolStripMenuItem,
            this.видToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(947, 28);
            this.Menu.TabIndex = 1;
            this.Menu.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.ExitToolStripMenuItem.Text = "Выход";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click_1);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportExportToolStripMenuItem,
            this.DeleteCurrentGraphToolStripMenuItem});
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.SettingsToolStripMenuItem.Text = "Настройки";
            // 
            // ImportExportToolStripMenuItem
            // 
            this.ImportExportToolStripMenuItem.Name = "ImportExportToolStripMenuItem";
            this.ImportExportToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.ImportExportToolStripMenuItem.Text = "Импорт/Экспорт";
            this.ImportExportToolStripMenuItem.Click += new System.EventHandler(this.ImportExportToolStripMenuItem_Click);
            // 
            // DeleteCurrentGraphToolStripMenuItem
            // 
            this.DeleteCurrentGraphToolStripMenuItem.Name = "DeleteCurrentGraphToolStripMenuItem";
            this.DeleteCurrentGraphToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.DeleteCurrentGraphToolStripMenuItem.Text = "Удалить текущий граф";
            this.DeleteCurrentGraphToolStripMenuItem.Click += new System.EventHandler(this.DeleteCurrentGraphToolStripMenuItem_Click);
            // 
            // графToolStripMenuItem
            // 
            this.графToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InsertVertexsToolStripMenuItem,
            this.ChangeVertexToolStripMenuItem,
            this.LinkVertex_toolStripMenuItem1,
            this.DragAndDropToolStripMenuItem,
            this.RouteToolStripMenuItem});
            this.графToolStripMenuItem.Name = "графToolStripMenuItem";
            this.графToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.графToolStripMenuItem.Text = "Граф";
            // 
            // InsertVertexsToolStripMenuItem
            // 
            this.InsertVertexsToolStripMenuItem.Name = "InsertVertexsToolStripMenuItem";
            this.InsertVertexsToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.InsertVertexsToolStripMenuItem.Text = "Вставить вершины";
            this.InsertVertexsToolStripMenuItem.Click += new System.EventHandler(this.InsertVertexsToolStripMenuItem_Click);
            // 
            // ChangeVertexToolStripMenuItem
            // 
            this.ChangeVertexToolStripMenuItem.Name = "ChangeVertexToolStripMenuItem";
            this.ChangeVertexToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.ChangeVertexToolStripMenuItem.Text = "Изменить вершины";
            this.ChangeVertexToolStripMenuItem.Click += new System.EventHandler(this.ChangeVertexToolStripMenuItem_Click);
            // 
            // LinkVertex_toolStripMenuItem1
            // 
            this.LinkVertex_toolStripMenuItem1.Name = "LinkVertex_toolStripMenuItem1";
            this.LinkVertex_toolStripMenuItem1.Size = new System.Drawing.Size(233, 24);
            this.LinkVertex_toolStripMenuItem1.Text = "Соединить вершины";
            this.LinkVertex_toolStripMenuItem1.Click += new System.EventHandler(this.LinkVertex_toolStripMenuItem1_Click);
            // 
            // DragAndDropToolStripMenuItem
            // 
            this.DragAndDropToolStripMenuItem.Name = "DragAndDropToolStripMenuItem";
            this.DragAndDropToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.DragAndDropToolStripMenuItem.Text = "Перетащить вершины";
            this.DragAndDropToolStripMenuItem.Click += new System.EventHandler(this.DragAndDropToolStripMenuItem_Click);
            // 
            // RouteToolStripMenuItem
            // 
            this.RouteToolStripMenuItem.Name = "RouteToolStripMenuItem";
            this.RouteToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.RouteToolStripMenuItem.Text = "Проложить путь";
            this.RouteToolStripMenuItem.Click += new System.EventHandler(this.RouteToolStripMenuItem_Click);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResetScalingToolStripMenuItem,
            this.ToggleNamesToolStripMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // ResetScalingToolStripMenuItem
            // 
            this.ResetScalingToolStripMenuItem.Name = "ResetScalingToolStripMenuItem";
            this.ResetScalingToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.ResetScalingToolStripMenuItem.Text = "Сбросить масштаб";
            this.ResetScalingToolStripMenuItem.Click += new System.EventHandler(this.ResetScalingToolStripMenuItem_Click);
            // 
            // ToggleNamesToolStripMenuItem
            // 
            this.ToggleNamesToolStripMenuItem.Name = "ToggleNamesToolStripMenuItem";
            this.ToggleNamesToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.ToggleNamesToolStripMenuItem.Text = "Названия вершин";
            this.ToggleNamesToolStripMenuItem.Click += new System.EventHandler(this.ToggleNamesToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 558);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Menu);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.Menu;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск кратчайшего пути Белов, Вотенцев, Емельянов";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem графToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertVertexsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeVertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LinkVertex_toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DragAndDropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetScalingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToggleNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RouteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteCurrentGraphToolStripMenuItem;
    }
}

