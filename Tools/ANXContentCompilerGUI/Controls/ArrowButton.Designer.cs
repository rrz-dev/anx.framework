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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelText.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelText.Location = new System.Drawing.Point(82, -1);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(252, 90);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Test Text";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelText.Click += new System.EventHandler(this.labelText_Click);
            this.labelText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrowButton_MouseDown);
            this.labelText.MouseEnter += new System.EventHandler(this.ArrowButton_MouseEnter);
            this.labelText.MouseLeave += new System.EventHandler(this.ArrowButton_MouseLeave);
            this.labelText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ArrowButton_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pictureBox1.Image = global::ANX.ContentCompiler.GUI.Properties.Resources.arrow;
            this.pictureBox1.Location = new System.Drawing.Point(17, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrowButton_MouseDown);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.ArrowButton_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.ArrowButton_MouseLeave);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ArrowButton_MouseUp);
            // 
            // ArrowButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelText);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ArrowButton";
            this.Size = new System.Drawing.Size(337, 93);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrowButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.ArrowButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ArrowButton_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ArrowButton_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
