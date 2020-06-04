namespace ColorCount
{
    partial class Swatche
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.numericUpDownTag = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTag)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxName);
            this.panel1.Controls.Add(this.numericUpDownTag);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 40);
            this.panel1.TabIndex = 0;
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxName.Location = new System.Drawing.Point(45, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(110, 13);
            this.textBoxName.TabIndex = 1;
            // 
            // numericUpDownTag
            // 
            this.numericUpDownTag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownTag.Location = new System.Drawing.Point(4, 10);
            this.numericUpDownTag.Name = "numericUpDownTag";
            this.numericUpDownTag.Size = new System.Drawing.Size(37, 16);
            this.numericUpDownTag.TabIndex = 0;
            this.numericUpDownTag.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Swatche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Swatche";
            this.Size = new System.Drawing.Size(160, 40);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTag)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.NumericUpDown numericUpDownTag;
    }
}
