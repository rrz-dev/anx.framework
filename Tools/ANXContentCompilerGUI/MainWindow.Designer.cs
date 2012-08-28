using ANX.ContentCompiler.GUI.States;

namespace ANX.ContentCompiler.GUI
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainerMenuLayout = new System.Windows.Forms.SplitContainer();
            this.ribbonButtonHelp = new ANX.ContentCompiler.GUI.Controls.RibbonButton();
            this.ribbonButtonWeb = new ANX.ContentCompiler.GUI.Controls.RibbonButton();
            this.ribbonButtonClean = new ANX.ContentCompiler.GUI.Controls.RibbonButton();
            this.ribbonButtonSave = new ANX.ContentCompiler.GUI.Controls.RibbonButton();
            this.ribbonButtonLoad = new ANX.ContentCompiler.GUI.Controls.RibbonButton();
            this.ribbonButtonNew = new ANX.ContentCompiler.GUI.Controls.RibbonButton();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonMenu = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.splitContainerFileTree = new System.Windows.Forms.SplitContainer();
            this.labelFileTree = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.treeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.treeViewItemAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewItemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerProperties = new System.Windows.Forms.SplitContainer();
            this.editingState = new ANX.ContentCompiler.GUI.States.EditingState();
            this.startState = new ANX.ContentCompiler.GUI.States.StartState();
            this.labelProperties = new System.Windows.Forms.Label();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.menuState = new ANX.ContentCompiler.GUI.States.MenuState();
            this.splitContainerMenuLayout.Panel1.SuspendLayout();
            this.splitContainerMenuLayout.Panel2.SuspendLayout();
            this.splitContainerMenuLayout.SuspendLayout();
            this.splitContainerFileTree.Panel1.SuspendLayout();
            this.splitContainerFileTree.Panel2.SuspendLayout();
            this.splitContainerFileTree.SuspendLayout();
            this.treeViewContextMenu.SuspendLayout();
            this.splitContainerProperties.Panel1.SuspendLayout();
            this.splitContainerProperties.Panel2.SuspendLayout();
            this.splitContainerProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMenuLayout
            // 
            this.splitContainerMenuLayout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMenuLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMenuLayout.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMenuLayout.IsSplitterFixed = true;
            this.splitContainerMenuLayout.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMenuLayout.Name = "splitContainerMenuLayout";
            this.splitContainerMenuLayout.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMenuLayout.Panel1
            // 
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.ribbonButtonHelp);
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.ribbonButtonWeb);
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.ribbonButtonClean);
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.ribbonButtonSave);
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.ribbonButtonLoad);
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.ribbonButtonNew);
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.buttonQuit);
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.buttonMenu);
            this.splitContainerMenuLayout.Panel1.Controls.Add(this.labelTitle);
            // 
            // splitContainerMenuLayout.Panel2
            // 
            this.splitContainerMenuLayout.Panel2.Controls.Add(this.splitContainerFileTree);
            this.splitContainerMenuLayout.Size = new System.Drawing.Size(865, 652);
            this.splitContainerMenuLayout.SplitterDistance = 99;
            this.splitContainerMenuLayout.TabIndex = 0;
            // 
            // ribbonButtonHelp
            // 
            this.ribbonButtonHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ribbonButtonHelp.Content = "Help";
            this.ribbonButtonHelp.Image = null;
            this.ribbonButtonHelp.Location = new System.Drawing.Point(299, 26);
            this.ribbonButtonHelp.Name = "ribbonButtonHelp";
            this.ribbonButtonHelp.Size = new System.Drawing.Size(52, 68);
            this.ribbonButtonHelp.TabIndex = 8;
            // 
            // ribbonButtonWeb
            // 
            this.ribbonButtonWeb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ribbonButtonWeb.Content = "Website";
            this.ribbonButtonWeb.Image = null;
            this.ribbonButtonWeb.Location = new System.Drawing.Point(237, 26);
            this.ribbonButtonWeb.Name = "ribbonButtonWeb";
            this.ribbonButtonWeb.Size = new System.Drawing.Size(63, 68);
            this.ribbonButtonWeb.TabIndex = 7;
            // 
            // ribbonButtonClean
            // 
            this.ribbonButtonClean.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ribbonButtonClean.Content = "Clean";
            this.ribbonButtonClean.Image = null;
            this.ribbonButtonClean.Location = new System.Drawing.Point(186, 26);
            this.ribbonButtonClean.Name = "ribbonButtonClean";
            this.ribbonButtonClean.Size = new System.Drawing.Size(52, 68);
            this.ribbonButtonClean.TabIndex = 6;
            // 
            // ribbonButtonSave
            // 
            this.ribbonButtonSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ribbonButtonSave.Content = "Save";
            this.ribbonButtonSave.Image = null;
            this.ribbonButtonSave.Location = new System.Drawing.Point(105, 26);
            this.ribbonButtonSave.Name = "ribbonButtonSave";
            this.ribbonButtonSave.Size = new System.Drawing.Size(52, 68);
            this.ribbonButtonSave.TabIndex = 5;
            // 
            // ribbonButtonLoad
            // 
            this.ribbonButtonLoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ribbonButtonLoad.Content = "Open";
            this.ribbonButtonLoad.Image = null;
            this.ribbonButtonLoad.Location = new System.Drawing.Point(54, 26);
            this.ribbonButtonLoad.Name = "ribbonButtonLoad";
            this.ribbonButtonLoad.Size = new System.Drawing.Size(52, 68);
            this.ribbonButtonLoad.TabIndex = 4;
            this.ribbonButtonLoad.Click += new System.EventHandler(this.RibbonButtonLoadClick);
            // 
            // ribbonButtonNew
            // 
            this.ribbonButtonNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ribbonButtonNew.Content = "New";
            this.ribbonButtonNew.Image = null;
            this.ribbonButtonNew.Location = new System.Drawing.Point(3, 26);
            this.ribbonButtonNew.Name = "ribbonButtonNew";
            this.ribbonButtonNew.Size = new System.Drawing.Size(52, 68);
            this.ribbonButtonNew.TabIndex = 3;
            this.ribbonButtonNew.Click += new System.EventHandler(this.RibbonButtonNewClick);
            // 
            // buttonQuit
            // 
            this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuit.FlatAppearance.BorderSize = 0;
            this.buttonQuit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonQuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.buttonQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQuit.ForeColor = System.Drawing.Color.White;
            this.buttonQuit.Location = new System.Drawing.Point(837, -1);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(26, 23);
            this.buttonQuit.TabIndex = 1;
            this.buttonQuit.Text = "X";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.ButtonQuitClick);
            // 
            // buttonMenu
            // 
            this.buttonMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonMenu.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonMenu.FlatAppearance.BorderSize = 0;
            this.buttonMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.buttonMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.buttonMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMenu.ForeColor = System.Drawing.Color.White;
            this.buttonMenu.Location = new System.Drawing.Point(-1, -1);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(85, 24);
            this.buttonMenu.TabIndex = 0;
            this.buttonMenu.Text = "File";
            this.buttonMenu.UseVisualStyleBackColor = false;
            this.buttonMenu.Click += new System.EventHandler(this.ButtonMenuClick);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Silver;
            this.labelTitle.Location = new System.Drawing.Point(-1, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(865, 24);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "ANX Content Compiler";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LabelTitleMouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LabelTitleMouseMove);
            this.labelTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LabelTitleMouseUp);
            // 
            // splitContainerFileTree
            // 
            this.splitContainerFileTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerFileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFileTree.Location = new System.Drawing.Point(0, 0);
            this.splitContainerFileTree.Name = "splitContainerFileTree";
            // 
            // splitContainerFileTree.Panel1
            // 
            this.splitContainerFileTree.Panel1.Controls.Add(this.labelFileTree);
            this.splitContainerFileTree.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainerFileTree.Panel2
            // 
            this.splitContainerFileTree.Panel2.Controls.Add(this.splitContainerProperties);
            this.splitContainerFileTree.Size = new System.Drawing.Size(865, 549);
            this.splitContainerFileTree.SplitterDistance = 221;
            this.splitContainerFileTree.TabIndex = 0;
            // 
            // labelFileTree
            // 
            this.labelFileTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFileTree.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileTree.ForeColor = System.Drawing.Color.White;
            this.labelFileTree.Location = new System.Drawing.Point(-1, 0);
            this.labelFileTree.Name = "labelFileTree";
            this.labelFileTree.Size = new System.Drawing.Size(220, 19);
            this.labelFileTree.TabIndex = 1;
            this.labelFileTree.Text = "Project Explorer";
            this.labelFileTree.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.ContextMenuStrip = this.treeViewContextMenu;
            this.treeView.ForeColor = System.Drawing.Color.White;
            this.treeView.LineColor = System.Drawing.Color.White;
            this.treeView.Location = new System.Drawing.Point(0, 22);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(219, 527);
            this.treeView.TabIndex = 0;
            // 
            // treeViewContextMenu
            // 
            this.treeViewContextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.treeViewItemAddFolder,
            this.treeViewItemRename,
            this.treeViewItemDelete});
            this.treeViewContextMenu.Name = "treeViewContextMenu";
            this.treeViewContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.treeViewContextMenu.ShowImageMargin = false;
            this.treeViewContextMenu.Size = new System.Drawing.Size(93, 70);
            // 
            // treeViewItemAddFolder
            // 
            this.treeViewItemAddFolder.ForeColor = System.Drawing.Color.White;
            this.treeViewItemAddFolder.Name = "treeViewItemAddFolder";
            this.treeViewItemAddFolder.Size = new System.Drawing.Size(92, 22);
            this.treeViewItemAddFolder.Text = "Add";
            this.treeViewItemAddFolder.MouseEnter += new System.EventHandler(this.TreeViewItemMouseEnter);
            this.treeViewItemAddFolder.MouseLeave += new System.EventHandler(this.TreeViewItemeLeave);
            // 
            // treeViewItemRename
            // 
            this.treeViewItemRename.ForeColor = System.Drawing.Color.White;
            this.treeViewItemRename.Name = "treeViewItemRename";
            this.treeViewItemRename.Size = new System.Drawing.Size(92, 22);
            this.treeViewItemRename.Text = "Rename";
            this.treeViewItemRename.MouseEnter += new System.EventHandler(this.TreeViewItemMouseEnter);
            this.treeViewItemRename.MouseLeave += new System.EventHandler(this.TreeViewItemeLeave);
            // 
            // treeViewItemDelete
            // 
            this.treeViewItemDelete.ForeColor = System.Drawing.Color.White;
            this.treeViewItemDelete.Name = "treeViewItemDelete";
            this.treeViewItemDelete.Size = new System.Drawing.Size(92, 22);
            this.treeViewItemDelete.Text = "Delete";
            this.treeViewItemDelete.MouseEnter += new System.EventHandler(this.TreeViewItemMouseEnter);
            this.treeViewItemDelete.MouseLeave += new System.EventHandler(this.TreeViewItemeLeave);
            // 
            // splitContainerProperties
            // 
            this.splitContainerProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProperties.ForeColor = System.Drawing.Color.White;
            this.splitContainerProperties.Location = new System.Drawing.Point(0, 0);
            this.splitContainerProperties.Name = "splitContainerProperties";
            // 
            // splitContainerProperties.Panel1
            // 
            this.splitContainerProperties.Panel1.Controls.Add(this.editingState);
            this.splitContainerProperties.Panel1.Controls.Add(this.startState);
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.Controls.Add(this.labelProperties);
            this.splitContainerProperties.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainerProperties.Panel2.ForeColor = System.Drawing.Color.White;
            this.splitContainerProperties.Size = new System.Drawing.Size(640, 549);
            this.splitContainerProperties.SplitterDistance = 409;
            this.splitContainerProperties.TabIndex = 0;
            // 
            // editingState
            // 
            this.editingState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editingState.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editingState.Location = new System.Drawing.Point(0, 0);
            this.editingState.Name = "editingState";
            this.editingState.Size = new System.Drawing.Size(407, 547);
            this.editingState.TabIndex = 1;
            this.editingState.Visible = false;
            // 
            // startState
            // 
            this.startState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startState.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startState.Location = new System.Drawing.Point(0, 0);
            this.startState.Name = "startState";
            this.startState.Size = new System.Drawing.Size(407, 547);
            this.startState.TabIndex = 0;
            this.startState.Visible = false;
            // 
            // labelProperties
            // 
            this.labelProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProperties.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProperties.ForeColor = System.Drawing.Color.White;
            this.labelProperties.Location = new System.Drawing.Point(3, 0);
            this.labelProperties.Name = "labelProperties";
            this.labelProperties.Size = new System.Drawing.Size(221, 19);
            this.labelProperties.TabIndex = 2;
            this.labelProperties.Text = "Properties";
            this.labelProperties.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.propertyGrid.CategoryForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.propertyGrid.CommandsActiveLinkColor = System.Drawing.Color.White;
            this.propertyGrid.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.propertyGrid.CommandsForeColor = System.Drawing.Color.White;
            this.propertyGrid.CommandsLinkColor = System.Drawing.Color.White;
            this.propertyGrid.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.propertyGrid.HelpForeColor = System.Drawing.Color.White;
            this.propertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.propertyGrid.Location = new System.Drawing.Point(3, 22);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(221, 503);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.propertyGrid.ViewForeColor = System.Drawing.Color.White;
            // 
            // menuState
            // 
            this.menuState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuState.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuState.Location = new System.Drawing.Point(-2, 23);
            this.menuState.Name = "menuState";
            this.menuState.Size = new System.Drawing.Size(865, 630);
            this.menuState.TabIndex = 1;
            this.menuState.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(865, 652);
            this.Controls.Add(this.menuState);
            this.Controls.Add(this.splitContainerMenuLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ANX Content Compiler";
            this.Shown += new System.EventHandler(this.MainWindowShown);
            this.splitContainerMenuLayout.Panel1.ResumeLayout(false);
            this.splitContainerMenuLayout.Panel2.ResumeLayout(false);
            this.splitContainerMenuLayout.ResumeLayout(false);
            this.splitContainerFileTree.Panel1.ResumeLayout(false);
            this.splitContainerFileTree.Panel2.ResumeLayout(false);
            this.splitContainerFileTree.ResumeLayout(false);
            this.treeViewContextMenu.ResumeLayout(false);
            this.splitContainerProperties.Panel1.ResumeLayout(false);
            this.splitContainerProperties.Panel2.ResumeLayout(false);
            this.splitContainerProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMenuLayout;
        private System.Windows.Forms.SplitContainer splitContainerFileTree;
        private System.Windows.Forms.SplitContainer splitContainerProperties;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelFileTree;
        private System.Windows.Forms.Label labelProperties;
        private Controls.RibbonButton ribbonButtonNew;
        private Controls.RibbonButton ribbonButtonSave;
        private Controls.RibbonButton ribbonButtonLoad;
        private Controls.RibbonButton ribbonButtonHelp;
        private Controls.RibbonButton ribbonButtonWeb;
        private Controls.RibbonButton ribbonButtonClean;
        private StartState startState;
        private System.Windows.Forms.ContextMenuStrip treeViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem treeViewItemRename;
        private System.Windows.Forms.ToolStripMenuItem treeViewItemDelete;
        private System.Windows.Forms.ToolStripMenuItem treeViewItemAddFolder;
        private EditingState editingState;
        private MenuState menuState;
        private System.Windows.Forms.Button buttonMenu;
    }
}

