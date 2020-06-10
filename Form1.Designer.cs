using ColorCount;

namespace ColorCount
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btn_save = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_process = new System.Windows.Forms.Button();
            this.cb_tags = new System.Windows.Forms.CheckBox();
            this.cb_grid = new System.Windows.Forms.CheckBox();
            this.textBox_colorHex = new System.Windows.Forms.TextBox();
            this.panel_ignoredColor = new System.Windows.Forms.Panel();
            this.btn_ignoredColor = new System.Windows.Forms.Button();
            this.btn_swatches = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.pb_loader = new System.Windows.Forms.ProgressBar();
            this.myPictureBox = new ColorCount.MyPictureBox();
            this.flowLayoutPanel_counter = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pb_loader, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.myPictureBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel_counter, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(774, 609);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.numericUpDown1);
            this.flowLayoutPanel1.Controls.Add(this.btn_save);
            this.flowLayoutPanel1.Controls.Add(this.numericUpDown2);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.btn_reset);
            this.flowLayoutPanel1.Controls.Add(this.btn_process);
            this.flowLayoutPanel1.Controls.Add(this.cb_tags);
            this.flowLayoutPanel1.Controls.Add(this.cb_grid);
            this.flowLayoutPanel1.Controls.Add(this.textBox_colorHex);
            this.flowLayoutPanel1.Controls.Add(this.panel_ignoredColor);
            this.flowLayoutPanel1.Controls.Add(this.btn_ignoredColor);
            this.flowLayoutPanel1.Controls.Add(this.btn_swatches);
            this.flowLayoutPanel1.Controls.Add(this.btnZoom);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(774, 31);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(713, 4);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 4, 10, 3);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numericUpDown1, "Divide o número de pixel por este");
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged_1);
            // 
            // btn_save
            // 
            this.btn_save.BackgroundImage = global::ColorCount.Properties.Resources.save_disable;
            this.btn_save.Enabled = false;
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(684, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(22, 20);
            this.btn_save.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_save, "Salve sua imagem para um arquivo PNG");
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.EnabledChanged += new System.EventHandler(this.btn_save_EnabledChanged);
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            this.btn_save.MouseLeave += new System.EventHandler(this.btn_save_MouseLeave);
            this.btn_save.MouseHover += new System.EventHandler(this.btn_save_MouseHover);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(628, 4);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(4, 4, 10, 3);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown2.TabIndex = 4;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numericUpDown2, "Tamanho de cada pixel na imagem marcada");
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(516, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tamanho do Bloco";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(457, 3);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(53, 23);
            this.btn_reset.TabIndex = 3;
            this.btn_reset.Text = "Reset";
            this.toolTip1.SetToolTip(this.btn_reset, "Reinicia os contadores de cores");
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_process
            // 
            this.btn_process.Enabled = false;
            this.btn_process.Location = new System.Drawing.Point(376, 3);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(75, 23);
            this.btn_process.TabIndex = 6;
            this.btn_process.Text = "Processar";
            this.toolTip1.SetToolTip(this.btn_process, "Gera a imagem");
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // cb_tags
            // 
            this.cb_tags.Location = new System.Drawing.Point(315, 3);
            this.cb_tags.Name = "cb_tags";
            this.cb_tags.Size = new System.Drawing.Size(55, 24);
            this.cb_tags.TabIndex = 13;
            this.cb_tags.Text = "Tags";
            this.cb_tags.UseVisualStyleBackColor = true;
            this.cb_tags.Click += new System.EventHandler(this.cb_tags_Click);
            // 
            // cb_grid
            // 
            this.cb_grid.Location = new System.Drawing.Point(243, 3);
            this.cb_grid.Name = "cb_grid";
            this.cb_grid.Size = new System.Drawing.Size(66, 24);
            this.cb_grid.TabIndex = 7;
            this.cb_grid.Text = "Grade";
            this.cb_grid.UseVisualStyleBackColor = true;
            this.cb_grid.Click += new System.EventHandler(this.cb_grid_Click);
            // 
            // textBox_colorHex
            // 
            this.textBox_colorHex.Location = new System.Drawing.Point(182, 3);
            this.textBox_colorHex.Name = "textBox_colorHex";
            this.textBox_colorHex.Size = new System.Drawing.Size(55, 20);
            this.textBox_colorHex.TabIndex = 12;
            // 
            // panel_ignoredColor
            // 
            this.panel_ignoredColor.Location = new System.Drawing.Point(152, 3);
            this.panel_ignoredColor.Name = "panel_ignoredColor";
            this.panel_ignoredColor.Size = new System.Drawing.Size(24, 24);
            this.panel_ignoredColor.TabIndex = 9;
            // 
            // btn_ignoredColor
            // 
            this.btn_ignoredColor.Location = new System.Drawing.Point(113, 3);
            this.btn_ignoredColor.Name = "btn_ignoredColor";
            this.btn_ignoredColor.Size = new System.Drawing.Size(33, 23);
            this.btn_ignoredColor.TabIndex = 8;
            this.btn_ignoredColor.Text = "Cor";
            this.toolTip1.SetToolTip(this.btn_ignoredColor, "Selecione uma cor para ser ignorada na contagem e marcação dos pixels");
            this.btn_ignoredColor.UseVisualStyleBackColor = true;
            this.btn_ignoredColor.Click += new System.EventHandler(this.btn_ignoredColor_Click);
            // 
            // btn_swatches
            // 
            this.btn_swatches.Location = new System.Drawing.Point(32, 3);
            this.btn_swatches.Name = "btn_swatches";
            this.btn_swatches.Size = new System.Drawing.Size(75, 23);
            this.btn_swatches.TabIndex = 10;
            this.btn_swatches.Text = "Amostras";
            this.btn_swatches.UseVisualStyleBackColor = true;
            this.btn_swatches.Click += new System.EventHandler(this.btn_swatches_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Location = new System.Drawing.Point(731, 33);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(40, 23);
            this.btnZoom.TabIndex = 11;
            this.btnZoom.Text = "z";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Visible = false;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // pb_loader
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pb_loader, 2);
            this.pb_loader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_loader.Location = new System.Drawing.Point(0, 31);
            this.pb_loader.Margin = new System.Windows.Forms.Padding(0);
            this.pb_loader.Name = "pb_loader";
            this.pb_loader.Size = new System.Drawing.Size(774, 2);
            this.pb_loader.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_loader.TabIndex = 2;
            this.pb_loader.Visible = false;
            // 
            // myPictureBox
            // 
            this.myPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.myPictureBox.BackgroundImage = global::ColorCount.Properties.Resources.drag_drop;
            this.myPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.myPictureBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.myPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPictureBox.Eyedropper = false;
            this.myPictureBox.Image = null;
            this.myPictureBox.ImageLocation = null;
            this.myPictureBox.Location = new System.Drawing.Point(180, 33);
            this.myPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.myPictureBox.Name = "myPictureBox";
            this.myPictureBox.Pan = false;
            this.myPictureBox.Size = new System.Drawing.Size(594, 576);
            this.myPictureBox.TabIndex = 3;
            this.myPictureBox.IgnoredColorChange += new System.Action<System.Drawing.Color>(this.myPictureBox_IgnoredColorChange);
            // 
            // flowLayoutPanel_counter
            // 
            this.flowLayoutPanel_counter.AutoScroll = true;
            this.flowLayoutPanel_counter.AutoSize = true;
            this.flowLayoutPanel_counter.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel_counter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_counter.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_counter.Location = new System.Drawing.Point(0, 33);
            this.flowLayoutPanel_counter.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel_counter.Name = "flowLayoutPanel_counter";
            this.flowLayoutPanel_counter.Size = new System.Drawing.Size(180, 576);
            this.flowLayoutPanel_counter.TabIndex = 4;
            this.flowLayoutPanel_counter.WrapContents = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 609);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Arraste uma imagem BMP - 8 Bits - Sem Compressão";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.CheckBox cb_grid;
        private System.Windows.Forms.ProgressBar pb_loader;
        private System.Windows.Forms.Button btn_ignoredColor;
        private System.Windows.Forms.Panel panel_ignoredColor;
        private MyPictureBox myPictureBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btn_swatches;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_counter;
        private System.Windows.Forms.TextBox textBox_colorHex;
        private System.Windows.Forms.CheckBox cb_tags;
    }


}
