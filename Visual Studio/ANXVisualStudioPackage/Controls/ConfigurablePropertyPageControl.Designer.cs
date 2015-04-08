namespace ANX.Framework.VisualStudio.Controls
{
    partial class ConfigurablePropertyPageControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_OutputDirectory = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_OutputDirectory = new System.Windows.Forms.TextBox();
            this.comboBox_GraphicsProfile = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_CompressContent = new System.Windows.Forms.CheckBox();
            this.browseFolderDialog1 = new ANX.Framework.VisualStudio.Controls.BrowseFolderDialog();
            this.SuspendLayout();
            // 
            // button_OutputDirectory
            // 
            this.button_OutputDirectory.AutoSize = true;
            this.button_OutputDirectory.Location = new System.Drawing.Point(304, 68);
            this.button_OutputDirectory.Name = "button_OutputDirectory";
            this.button_OutputDirectory.Size = new System.Drawing.Size(76, 23);
            this.button_OutputDirectory.TabIndex = 25;
            this.button_OutputDirectory.Text = "Browse...";
            this.button_OutputDirectory.UseVisualStyleBackColor = true;
            this.button_OutputDirectory.Click += new System.EventHandler(this.button_OutputDirectory_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Output Directory:";
            // 
            // textBox_OutputDirectory
            // 
            this.textBox_OutputDirectory.Location = new System.Drawing.Point(3, 70);
            this.textBox_OutputDirectory.Name = "textBox_OutputDirectory";
            this.textBox_OutputDirectory.Size = new System.Drawing.Size(295, 20);
            this.textBox_OutputDirectory.TabIndex = 23;
            this.textBox_OutputDirectory.TextChanged += new System.EventHandler(this.textBox_OutputDirectory_TextChanged);
            // 
            // comboBox_GraphicsProfile
            // 
            this.comboBox_GraphicsProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_GraphicsProfile.FormattingEnabled = true;
            this.comboBox_GraphicsProfile.Location = new System.Drawing.Point(3, 23);
            this.comboBox_GraphicsProfile.Name = "comboBox_GraphicsProfile";
            this.comboBox_GraphicsProfile.Size = new System.Drawing.Size(295, 21);
            this.comboBox_GraphicsProfile.TabIndex = 21;
            this.comboBox_GraphicsProfile.SelectedIndexChanged += new System.EventHandler(this.comboBox_GraphicsProfile_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Graphics Profile:";
            // 
            // checkBox_CompressContent
            // 
            this.checkBox_CompressContent.AutoSize = true;
            this.checkBox_CompressContent.Location = new System.Drawing.Point(316, 25);
            this.checkBox_CompressContent.Name = "checkBox_CompressContent";
            this.checkBox_CompressContent.Size = new System.Drawing.Size(110, 17);
            this.checkBox_CompressContent.TabIndex = 20;
            this.checkBox_CompressContent.Text = "compress content";
            this.checkBox_CompressContent.UseVisualStyleBackColor = true;
            this.checkBox_CompressContent.CheckedChanged += new System.EventHandler(this.checkBox_CompressContent_CheckedChanged);
            // 
            // browseFolderDialog1
            // 
            this.browseFolderDialog1.RootFolder = null;
            this.browseFolderDialog1.SelectedPath = null;
            this.browseFolderDialog1.Title = null;
            // 
            // ConfigurablePropertyPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_OutputDirectory);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_OutputDirectory);
            this.Controls.Add(this.comboBox_GraphicsProfile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_CompressContent);
            this.Name = "ConfigurablePropertyPageControl";
            this.Size = new System.Drawing.Size(828, 398);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_OutputDirectory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_OutputDirectory;
        private System.Windows.Forms.ComboBox comboBox_GraphicsProfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_CompressContent;
        private BrowseFolderDialog browseFolderDialog1;
    }
}
