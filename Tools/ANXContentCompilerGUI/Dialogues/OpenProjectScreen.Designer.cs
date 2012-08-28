namespace ANX.ContentCompiler.GUI.Dialogues
{
    partial class OpenProjectScreen
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelManualSearch = new System.Windows.Forms.Label();
            this.labelHeading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxRecentProjects = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(12, 317);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.buttonNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Location = new System.Drawing.Point(504, 317);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(84, 30);
            this.buttonNext.TabIndex = 6;
            this.buttonNext.Text = "Open";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.ButtonNextClick);
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.listBoxRecentProjects);
            this.panel1.Controls.Add(this.buttonBrowse);
            this.panel1.Controls.Add(this.textBoxLocation);
            this.panel1.Controls.Add(this.labelManualSearch);
            this.panel1.Controls.Add(this.labelHeading);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 359);
            this.panel1.TabIndex = 14;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonBrowse.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.buttonBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse.Location = new System.Drawing.Point(479, 249);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(87, 23);
            this.buttonBrowse.TabIndex = 20;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowseClick);
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLocation.ForeColor = System.Drawing.Color.White;
            this.textBoxLocation.Location = new System.Drawing.Point(71, 249);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(402, 22);
            this.textBoxLocation.TabIndex = 19;
            // 
            // labelManualSearch
            // 
            this.labelManualSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelManualSearch.Location = new System.Drawing.Point(22, 219);
            this.labelManualSearch.Name = "labelManualSearch";
            this.labelManualSearch.Size = new System.Drawing.Size(554, 27);
            this.labelManualSearch.TabIndex = 18;
            this.labelManualSearch.Text = "Not in there? No problem!";
            this.labelManualSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHeading
            // 
            this.labelHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.Location = new System.Drawing.Point(85, 55);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(429, 38);
            this.labelHeading.TabIndex = 15;
            this.labelHeading.Text = "Do you look for one of these?";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(570, 22);
            this.label1.TabIndex = 14;
            this.label1.Text = "New Project";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxRecentProjects
            // 
            this.listBoxRecentProjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBoxRecentProjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxRecentProjects.ForeColor = System.Drawing.Color.LightGray;
            this.listBoxRecentProjects.FormattingEnabled = true;
            this.listBoxRecentProjects.Items.AddRange(new object[] {
            "Test",
            "Blubb",
            "Put",
            "Recent",
            "Projects",
            "here"});
            this.listBoxRecentProjects.Location = new System.Drawing.Point(71, 96);
            this.listBoxRecentProjects.Name = "listBoxRecentProjects";
            this.listBoxRecentProjects.Size = new System.Drawing.Size(443, 119);
            this.listBoxRecentProjects.TabIndex = 21;
            // 
            // OpenProjectScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(600, 359);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OpenProjectScreen";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FirstStartScreen";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelManualSearch;
        public System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.ListBox listBoxRecentProjects;
    }
}