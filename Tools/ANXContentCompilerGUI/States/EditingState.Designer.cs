namespace ANX.ContentCompiler.GUI.States
{
    partial class EditingState
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
            this.labelHead = new System.Windows.Forms.Label();
            this.arrowButtonAddFiles = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.arrowButtonCreateFolder = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.arrowButtonPreview = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.arrowButtonBuild = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHead.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHead.Location = new System.Drawing.Point(15, 11);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(351, 61);
            this.labelHead.TabIndex = 4;
            this.labelHead.Text = "What now?";
            this.labelHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // arrowButtonAddFiles
            // 
            this.arrowButtonAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrowButtonAddFiles.AutoSize = true;
            this.arrowButtonAddFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.arrowButtonAddFiles.Content = "Add Files";
            this.arrowButtonAddFiles.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonAddFiles.ForeColor = System.Drawing.Color.White;
            this.arrowButtonAddFiles.Location = new System.Drawing.Point(16, 73);
            this.arrowButtonAddFiles.Name = "arrowButtonAddFiles";
            this.arrowButtonAddFiles.Size = new System.Drawing.Size(348, 64);
            this.arrowButtonAddFiles.TabIndex = 5;
            // 
            // arrowButtonCreateFolder
            // 
            this.arrowButtonCreateFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrowButtonCreateFolder.AutoSize = true;
            this.arrowButtonCreateFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.arrowButtonCreateFolder.Content = "Create Folder";
            this.arrowButtonCreateFolder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonCreateFolder.ForeColor = System.Drawing.Color.White;
            this.arrowButtonCreateFolder.Location = new System.Drawing.Point(16, 141);
            this.arrowButtonCreateFolder.Name = "arrowButtonCreateFolder";
            this.arrowButtonCreateFolder.Size = new System.Drawing.Size(348, 64);
            this.arrowButtonCreateFolder.TabIndex = 6;
            // 
            // arrowButtonPreview
            // 
            this.arrowButtonPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrowButtonPreview.AutoSize = true;
            this.arrowButtonPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.arrowButtonPreview.Content = "Preview File";
            this.arrowButtonPreview.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonPreview.ForeColor = System.Drawing.Color.White;
            this.arrowButtonPreview.Location = new System.Drawing.Point(16, 206);
            this.arrowButtonPreview.Name = "arrowButtonPreview";
            this.arrowButtonPreview.Size = new System.Drawing.Size(348, 64);
            this.arrowButtonPreview.TabIndex = 7;
            // 
            // arrowButtonBuild
            // 
            this.arrowButtonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrowButtonBuild.AutoSize = true;
            this.arrowButtonBuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.arrowButtonBuild.Content = "Build Project";
            this.arrowButtonBuild.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonBuild.ForeColor = System.Drawing.Color.White;
            this.arrowButtonBuild.Location = new System.Drawing.Point(16, 273);
            this.arrowButtonBuild.Name = "arrowButtonBuild";
            this.arrowButtonBuild.Size = new System.Drawing.Size(348, 64);
            this.arrowButtonBuild.TabIndex = 8;
            // 
            // EditingState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.arrowButtonBuild);
            this.Controls.Add(this.arrowButtonPreview);
            this.Controls.Add(this.arrowButtonCreateFolder);
            this.Controls.Add(this.arrowButtonAddFiles);
            this.Controls.Add(this.labelHead);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "EditingState";
            this.Size = new System.Drawing.Size(380, 384);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHead;
        private Controls.ArrowButton arrowButtonAddFiles;
        private Controls.ArrowButton arrowButtonCreateFolder;
        private Controls.ArrowButton arrowButtonPreview;
        private Controls.ArrowButton arrowButtonBuild;

    }
}
