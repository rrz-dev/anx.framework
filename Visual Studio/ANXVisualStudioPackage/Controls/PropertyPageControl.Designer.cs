namespace ANX.Framework.VisualStudio.Controls
{
    partial class PropertyPageControl
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
            this.label5 = new System.Windows.Forms.Label();
            this.sdkComboBox1 = new ANX.Framework.VisualStudio.Controls.SDKComboBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Target SDK:";
            // 
            // sdkComboBox1
            // 
            this.sdkComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sdkComboBox1.FormattingEnabled = true;
            this.sdkComboBox1.Location = new System.Drawing.Point(6, 16);
            this.sdkComboBox1.Name = "sdkComboBox1";
            this.sdkComboBox1.Size = new System.Drawing.Size(295, 21);
            this.sdkComboBox1.TabIndex = 13;
            this.sdkComboBox1.SelectedIndexChanged += new System.EventHandler(this.sdkComboBox1_SelectedIndexChanged);
            // 
            // PropertyPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sdkComboBox1);
            this.Name = "PropertyPageControl";
            this.Size = new System.Drawing.Size(762, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SDKComboBox sdkComboBox1;
        private System.Windows.Forms.Label label5;
    }
}
