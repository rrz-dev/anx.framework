namespace ANX.ContentCompiler.GUI.States
{
    partial class StartState
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
            this.label1 = new System.Windows.Forms.Label();
            this.arrowButtonLoad = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.arrowButtonImport = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.arrowButtonNew = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 61);
            this.label1.TabIndex = 3;
            this.label1.Text = "What do you want to do?\r\n\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // arrowButtonLoad
            // 
            this.arrowButtonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrowButtonLoad.AutoSize = true;
            this.arrowButtonLoad.Content = "Load Project";
            this.arrowButtonLoad.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonLoad.Location = new System.Drawing.Point(20, 158);
            this.arrowButtonLoad.Name = "arrowButtonLoad";
            this.arrowButtonLoad.Size = new System.Drawing.Size(348, 64);
            this.arrowButtonLoad.TabIndex = 2;
            // 
            // arrowButtonImport
            // 
            this.arrowButtonImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrowButtonImport.AutoSize = true;
            this.arrowButtonImport.Content = "Import Project";
            this.arrowButtonImport.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonImport.Location = new System.Drawing.Point(20, 228);
            this.arrowButtonImport.Name = "arrowButtonImport";
            this.arrowButtonImport.Size = new System.Drawing.Size(348, 64);
            this.arrowButtonImport.TabIndex = 1;
            // 
            // arrowButtonNew
            // 
            this.arrowButtonNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrowButtonNew.AutoSize = true;
            this.arrowButtonNew.Content = "Create Project";
            this.arrowButtonNew.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonNew.Location = new System.Drawing.Point(20, 88);
            this.arrowButtonNew.Name = "arrowButtonNew";
            this.arrowButtonNew.Size = new System.Drawing.Size(348, 64);
            this.arrowButtonNew.TabIndex = 0;
            // 
            // StartState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.arrowButtonLoad);
            this.Controls.Add(this.arrowButtonImport);
            this.Controls.Add(this.arrowButtonNew);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "StartState";
            this.Size = new System.Drawing.Size(386, 323);
            this.Load += new System.EventHandler(this.StartState_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ArrowButton arrowButtonNew;
        private Controls.ArrowButton arrowButtonImport;
        private Controls.ArrowButton arrowButtonLoad;
        private System.Windows.Forms.Label label1;
    }
}
