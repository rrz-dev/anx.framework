namespace ANX.ContentCompiler.GUI.Controls
{
    partial class ArrowButton
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
            this.labelText = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelText.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelText.Location = new System.Drawing.Point(0, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(259, 48);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Test Text";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelText.Click += new System.EventHandler(this.LabelTextClick);
            this.labelText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrowButtonMouseDown);
            this.labelText.MouseEnter += new System.EventHandler(this.ArrowButtonMouseEnter);
            this.labelText.MouseLeave += new System.EventHandler(this.ArrowButtonMouseLeave);
            this.labelText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ArrowButtonMouseUp);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.labelText);
            this.splitContainer.Size = new System.Drawing.Size(310, 48);
            this.splitContainer.SplitterDistance = 47;
            this.splitContainer.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::ANX.ContentCompiler.GUI.Properties.Resources.arrow1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrowButtonMouseDown);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.ArrowButtonMouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.ArrowButtonMouseLeave);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ArrowButtonMouseUp);
            // 
            // ArrowButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ArrowButton";
            this.Size = new System.Drawing.Size(310, 48);
            this.Load += new System.EventHandler(this.ArrowButtonLoad);
            this.FontChanged += new System.EventHandler(this.ArrowButtonFontChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrowButtonMouseDown);
            this.MouseEnter += new System.EventHandler(this.ArrowButtonMouseEnter);
            this.MouseLeave += new System.EventHandler(this.ArrowButtonMouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ArrowButtonMouseUp);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
