namespace ANX.ContentCompiler.GUI.States
{
    partial class MenuState
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuState));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.panelNew = new System.Windows.Forms.Panel();
            this.labelDescription = new System.Windows.Forms.Label();
            this.arrowButtonFolder = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.arrowButtonFile = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.arrowButtonNewProject = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.labelNew = new System.Windows.Forms.Label();
            this.panelOpen = new System.Windows.Forms.Panel();
            this.labelOpenDesc = new System.Windows.Forms.Label();
            this.arrowButtonImport = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.labelOpen = new System.Windows.Forms.Label();
            this.arrowButtonOpen = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.panelSaveAs = new System.Windows.Forms.Panel();
            this.labelDesc = new System.Windows.Forms.Label();
            this.arrowButtonSaveAsCCProj = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.arrowButtonSaveAsCproj = new ANX.ContentCompiler.GUI.Controls.ArrowButton();
            this.labelHeading = new System.Windows.Forms.Label();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.labelSettings = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelNew.SuspendLayout();
            this.panelOpen.SuspendLayout();
            this.panelSaveAs.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Green;
            this.panel1.Controls.Add(this.buttonExit);
            this.panel1.Controls.Add(this.buttonSettings);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonSaveAs);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonOpen);
            this.panel1.Controls.Add(this.buttonNew);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 628);
            this.panel1.TabIndex = 0;
            // 
            // buttonExit
            // 
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Location = new System.Drawing.Point(-1, 279);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(86, 41);
            this.buttonExit.TabIndex = 6;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExitClick);
            // 
            // buttonSettings
            // 
            this.buttonSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Location = new System.Drawing.Point(-1, 228);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(86, 41);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.ButtonSettingsClick);
            // 
            // buttonClose
            // 
            this.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(-1, 178);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(86, 41);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveAs.Location = new System.Drawing.Point(-1, 120);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(86, 41);
            this.buttonSaveAs.TabIndex = 3;
            this.buttonSaveAs.Text = "Save As";
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.ButtonSaveAsClick);
            // 
            // buttonSave
            // 
            this.buttonSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Location = new System.Drawing.Point(-1, 80);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(86, 41);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // buttonOpen
            // 
            this.buttonOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpen.Location = new System.Drawing.Point(-1, 40);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(86, 41);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.ButtonOpenClick);
            // 
            // buttonNew
            // 
            this.buttonNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNew.Location = new System.Drawing.Point(-1, 0);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(86, 41);
            this.buttonNew.TabIndex = 0;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = false;
            this.buttonNew.Click += new System.EventHandler(this.ButtonNewClick);
            // 
            // labelVersion
            // 
            this.labelVersion.Location = new System.Drawing.Point(662, 591);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(188, 26);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "ANX Content Compiler v4.0.0.1";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelNew
            // 
            this.panelNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNew.Controls.Add(this.labelDescription);
            this.panelNew.Controls.Add(this.arrowButtonFolder);
            this.panelNew.Controls.Add(this.arrowButtonFile);
            this.panelNew.Controls.Add(this.arrowButtonNewProject);
            this.panelNew.Controls.Add(this.labelNew);
            this.panelNew.Location = new System.Drawing.Point(95, 0);
            this.panelNew.Name = "panelNew";
            this.panelNew.Size = new System.Drawing.Size(773, 576);
            this.panelNew.TabIndex = 4;
            // 
            // labelDescription
            // 
            this.labelDescription.Location = new System.Drawing.Point(29, 205);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(688, 34);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "If you choose to add a new file or folder, it will be created under the current s" +
    "elected item in the project explorer.";
            // 
            // arrowButtonFolder
            // 
            this.arrowButtonFolder.AutoSize = true;
            this.arrowButtonFolder.Content = "Folder";
            this.arrowButtonFolder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonFolder.Location = new System.Drawing.Point(26, 86);
            this.arrowButtonFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.arrowButtonFolder.Name = "arrowButtonFolder";
            this.arrowButtonFolder.Size = new System.Drawing.Size(216, 33);
            this.arrowButtonFolder.TabIndex = 2;
            this.arrowButtonFolder.Click += new System.EventHandler(this.ArrowButtonFolderClick);
            // 
            // arrowButtonFile
            // 
            this.arrowButtonFile.AutoSize = true;
            this.arrowButtonFile.Content = "File";
            this.arrowButtonFile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonFile.Location = new System.Drawing.Point(26, 46);
            this.arrowButtonFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.arrowButtonFile.Name = "arrowButtonFile";
            this.arrowButtonFile.Size = new System.Drawing.Size(216, 33);
            this.arrowButtonFile.TabIndex = 0;
            this.arrowButtonFile.Click += new System.EventHandler(this.ArrowButtonFileClick);
            // 
            // arrowButtonNewProject
            // 
            this.arrowButtonNewProject.AutoSize = true;
            this.arrowButtonNewProject.Content = "New Project";
            this.arrowButtonNewProject.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonNewProject.Location = new System.Drawing.Point(26, 145);
            this.arrowButtonNewProject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.arrowButtonNewProject.Name = "arrowButtonNewProject";
            this.arrowButtonNewProject.Size = new System.Drawing.Size(216, 33);
            this.arrowButtonNewProject.TabIndex = 3;
            this.arrowButtonNewProject.Click += new System.EventHandler(this.ArrowButtonNewProjectClick);
            // 
            // labelNew
            // 
            this.labelNew.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNew.Location = new System.Drawing.Point(21, 9);
            this.labelNew.Name = "labelNew";
            this.labelNew.Size = new System.Drawing.Size(242, 29);
            this.labelNew.TabIndex = 1;
            this.labelNew.Text = "Create new Stuff";
            this.labelNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelOpen
            // 
            this.panelOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOpen.Controls.Add(this.labelOpenDesc);
            this.panelOpen.Controls.Add(this.arrowButtonImport);
            this.panelOpen.Controls.Add(this.labelOpen);
            this.panelOpen.Controls.Add(this.arrowButtonOpen);
            this.panelOpen.Location = new System.Drawing.Point(95, -1);
            this.panelOpen.Name = "panelOpen";
            this.panelOpen.Size = new System.Drawing.Size(773, 576);
            this.panelOpen.TabIndex = 5;
            this.panelOpen.Visible = false;
            // 
            // labelOpenDesc
            // 
            this.labelOpenDesc.Location = new System.Drawing.Point(28, 169);
            this.labelOpenDesc.Name = "labelOpenDesc";
            this.labelOpenDesc.Size = new System.Drawing.Size(687, 70);
            this.labelOpenDesc.TabIndex = 3;
            this.labelOpenDesc.Text = resources.GetString("labelOpenDesc.Text");
            // 
            // arrowButtonImport
            // 
            this.arrowButtonImport.AutoSize = true;
            this.arrowButtonImport.Content = "Import";
            this.arrowButtonImport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonImport.Location = new System.Drawing.Point(29, 89);
            this.arrowButtonImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.arrowButtonImport.Name = "arrowButtonImport";
            this.arrowButtonImport.Size = new System.Drawing.Size(216, 33);
            this.arrowButtonImport.TabIndex = 2;
            this.arrowButtonImport.Click += new System.EventHandler(this.ArrowButtonImportClick);
            // 
            // labelOpen
            // 
            this.labelOpen.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOpen.Location = new System.Drawing.Point(21, 9);
            this.labelOpen.Name = "labelOpen";
            this.labelOpen.Size = new System.Drawing.Size(242, 29);
            this.labelOpen.TabIndex = 1;
            this.labelOpen.Text = "Open/Import Project";
            this.labelOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // arrowButtonOpen
            // 
            this.arrowButtonOpen.AutoSize = true;
            this.arrowButtonOpen.Content = "Open";
            this.arrowButtonOpen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonOpen.Location = new System.Drawing.Point(29, 52);
            this.arrowButtonOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.arrowButtonOpen.Name = "arrowButtonOpen";
            this.arrowButtonOpen.Size = new System.Drawing.Size(216, 33);
            this.arrowButtonOpen.TabIndex = 0;
            this.arrowButtonOpen.Click += new System.EventHandler(this.ArrowButtonOpenClick);
            // 
            // panelSaveAs
            // 
            this.panelSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSaveAs.Controls.Add(this.labelDesc);
            this.panelSaveAs.Controls.Add(this.arrowButtonSaveAsCCProj);
            this.panelSaveAs.Controls.Add(this.arrowButtonSaveAsCproj);
            this.panelSaveAs.Controls.Add(this.labelHeading);
            this.panelSaveAs.Location = new System.Drawing.Point(95, 0);
            this.panelSaveAs.Name = "panelSaveAs";
            this.panelSaveAs.Size = new System.Drawing.Size(773, 576);
            this.panelSaveAs.TabIndex = 5;
            this.panelSaveAs.Visible = false;
            // 
            // labelDesc
            // 
            this.labelDesc.Location = new System.Drawing.Point(27, 136);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(688, 41);
            this.labelDesc.TabIndex = 3;
            this.labelDesc.Text = "Choose the compressed Content Project if you only have a small number of files or" +
    " the compression will take ages.";
            // 
            // arrowButtonSaveAsCCProj
            // 
            this.arrowButtonSaveAsCCProj.AutoSize = true;
            this.arrowButtonSaveAsCCProj.Content = "Compressed Content Project";
            this.arrowButtonSaveAsCCProj.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonSaveAsCCProj.Location = new System.Drawing.Point(26, 92);
            this.arrowButtonSaveAsCCProj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.arrowButtonSaveAsCCProj.Name = "arrowButtonSaveAsCCProj";
            this.arrowButtonSaveAsCCProj.Size = new System.Drawing.Size(267, 33);
            this.arrowButtonSaveAsCCProj.TabIndex = 2;
            this.arrowButtonSaveAsCCProj.Click += new System.EventHandler(this.ArrowButtonSaveAsCcProjClick);
            // 
            // arrowButtonSaveAsCproj
            // 
            this.arrowButtonSaveAsCproj.AutoSize = true;
            this.arrowButtonSaveAsCproj.Content = "ANX Content Projekt";
            this.arrowButtonSaveAsCproj.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowButtonSaveAsCproj.Location = new System.Drawing.Point(26, 55);
            this.arrowButtonSaveAsCproj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.arrowButtonSaveAsCproj.Name = "arrowButtonSaveAsCproj";
            this.arrowButtonSaveAsCproj.Size = new System.Drawing.Size(267, 33);
            this.arrowButtonSaveAsCproj.TabIndex = 0;
            this.arrowButtonSaveAsCproj.Click += new System.EventHandler(this.ArrowButtonSaveAsCprojClick);
            // 
            // labelHeading
            // 
            this.labelHeading.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.Location = new System.Drawing.Point(21, 9);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(242, 29);
            this.labelHeading.TabIndex = 1;
            this.labelHeading.Text = "Save the project";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelSettings
            // 
            this.panelSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSettings.Controls.Add(this.labelSettings);
            this.panelSettings.Location = new System.Drawing.Point(95, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(773, 576);
            this.panelSettings.TabIndex = 6;
            this.panelSettings.Visible = false;
            // 
            // labelSettings
            // 
            this.labelSettings.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettings.Location = new System.Drawing.Point(21, 9);
            this.labelSettings.Name = "labelSettings";
            this.labelSettings.Size = new System.Drawing.Size(242, 29);
            this.labelSettings.TabIndex = 1;
            this.labelSettings.Text = "Tweak me!";
            this.labelSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MenuState
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.panelOpen);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelSaveAs);
            this.Controls.Add(this.panelNew);
            this.Controls.Add(this.panelSettings);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MenuState";
            this.Size = new System.Drawing.Size(863, 626);
            this.Load += new System.EventHandler(this.MenuState_Load);
            this.panel1.ResumeLayout(false);
            this.panelNew.ResumeLayout(false);
            this.panelNew.PerformLayout();
            this.panelOpen.ResumeLayout(false);
            this.panelOpen.PerformLayout();
            this.panelSaveAs.ResumeLayout(false);
            this.panelSaveAs.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOpen;
        private Controls.ArrowButton arrowButtonFile;
        private Controls.ArrowButton arrowButtonFolder;
        private Controls.ArrowButton arrowButtonNewProject;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Panel panelNew;
        private System.Windows.Forms.Label labelNew;
        private System.Windows.Forms.Panel panelOpen;
        private Controls.ArrowButton arrowButtonImport;
        private System.Windows.Forms.Label labelOpen;
        private Controls.ArrowButton arrowButtonOpen;
        private Controls.ArrowButton arrowButtonSaveAsCCProj;
        private Controls.ArrowButton arrowButtonSaveAsCproj;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Panel panelSaveAs;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelOpenDesc;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Label labelSettings;
    }
}
