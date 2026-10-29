using ANX.ContentCompiler.GUI.Properties;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    partial class FirstStartScreen
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
            this.labelTour = new System.Windows.Forms.Label();
            this.labelSkip = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBoxSmiley = new System.Windows.Forms.PictureBox();
            this.pictureBoxArrowLeft = new System.Windows.Forms.PictureBox();
            this.pictureBoxArrowRight = new System.Windows.Forms.PictureBox();
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelSlogan = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSmiley)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTour
            // 
            this.labelTour.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTour.Location = new System.Drawing.Point(387, 260);
            this.labelTour.Name = "labelTour";
            this.labelTour.Size = new System.Drawing.Size(172, 60);
            this.labelTour.TabIndex = 3;
            this.labelTour.Text = "May I show you around?";
            this.labelTour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSkip
            // 
            this.labelSkip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkip.Location = new System.Drawing.Point(114, 274);
            this.labelSkip.Name = "labelSkip";
            this.labelSkip.Size = new System.Drawing.Size(120, 60);
            this.labelSkip.TabIndex = 4;
            this.labelSkip.Text = "Or do you know\r\nme already?";
            this.labelSkip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 5;
            this.button1.Text = "Skip";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(504, 358);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 30);
            this.button2.TabIndex = 6;
            this.button2.Text = "Take Tour";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(572, -1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "X";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // pictureBoxSmiley
            // 
            this.pictureBoxSmiley.Image = global::ANX.ContentCompiler.GUI.Properties.Resources.Deleket_Smileys_8;
            this.pictureBoxSmiley.Location = new System.Drawing.Point(238, 129);
            this.pictureBoxSmiley.Name = "pictureBoxSmiley";
            this.pictureBoxSmiley.Size = new System.Drawing.Size(143, 139);
            this.pictureBoxSmiley.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSmiley.TabIndex = 2;
            this.pictureBoxSmiley.TabStop = false;
            // 
            // pictureBoxArrowLeft
            // 
            this.pictureBoxArrowLeft.Image = global::ANX.ContentCompiler.GUI.Properties.Resources.arrow_left;
            this.pictureBoxArrowLeft.Location = new System.Drawing.Point(176, 274);
            this.pictureBoxArrowLeft.Name = "pictureBoxArrowLeft";
            this.pictureBoxArrowLeft.Size = new System.Drawing.Size(118, 114);
            this.pictureBoxArrowLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxArrowLeft.TabIndex = 8;
            this.pictureBoxArrowLeft.TabStop = false;
            // 
            // pictureBoxArrowRight
            // 
            this.pictureBoxArrowRight.Image = global::ANX.ContentCompiler.GUI.Properties.Resources.arrow_right;
            this.pictureBoxArrowRight.Location = new System.Drawing.Point(317, 274);
            this.pictureBoxArrowRight.Name = "pictureBoxArrowRight";
            this.pictureBoxArrowRight.Size = new System.Drawing.Size(119, 114);
            this.pictureBoxArrowRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxArrowRight.TabIndex = 9;
            this.pictureBoxArrowRight.TabStop = false;
            // 
            // labelHeading
            // 
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.Location = new System.Drawing.Point(229, 55);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(171, 48);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Hello.";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSlogan
            // 
            this.labelSlogan.BackColor = System.Drawing.Color.Transparent;
            this.labelSlogan.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSlogan.Location = new System.Drawing.Point(264, 95);
            this.labelSlogan.Name = "labelSlogan";
            this.labelSlogan.Size = new System.Drawing.Size(223, 27);
            this.labelSlogan.TabIndex = 1;
            this.labelSlogan.Text = "Are you new here?";
            this.labelSlogan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ANX.ContentCompiler.GUI.Properties.Resources.clouds;
            this.pictureBox1.Location = new System.Drawing.Point(-42, 73);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(264, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.labelSlogan);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pictureBoxSmiley);
            this.panel1.Controls.Add(this.labelSkip);
            this.panel1.Controls.Add(this.labelTour);
            this.panel1.Controls.Add(this.pictureBoxArrowLeft);
            this.panel1.Controls.Add(this.pictureBoxArrowRight);
            this.panel1.Controls.Add(this.labelHeading);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 400);
            this.panel1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(570, 22);
            this.label1.TabIndex = 14;
            this.label1.Text = "First Start";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FirstStartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FirstStartScreen";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FirstStartScreen";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSmiley)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArrowRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelSlogan;
        private System.Windows.Forms.PictureBox pictureBoxSmiley;
        private System.Windows.Forms.Label labelTour;
        private System.Windows.Forms.Label labelSkip;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBoxArrowLeft;
        private System.Windows.Forms.PictureBox pictureBoxArrowRight;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}