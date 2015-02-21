namespace AnxSampleBrowser
{
    partial class SampleDataHalfVisual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleDataHalfVisual));
            this._lName = new System.Windows.Forms.Label();
            this._bLaunch = new System.Windows.Forms.Button();
            this._rDescription = new System.Windows.Forms.RichTextBox();
            this._pImage = new System.Windows.Forms.Panel();
            this._bOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lName
            // 
            resources.ApplyResources(this._lName, "_lName");
            this._lName.BackColor = System.Drawing.Color.Transparent;
            this._lName.ForeColor = System.Drawing.Color.White;
            this._lName.Name = "_lName";
            // 
            // _bLaunch
            // 
            resources.ApplyResources(this._bLaunch, "_bLaunch");
            this._bLaunch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this._bLaunch.ForeColor = System.Drawing.Color.White;
            this._bLaunch.Name = "_bLaunch";
            this._bLaunch.UseVisualStyleBackColor = false;
            this._bLaunch.Click += new System.EventHandler(this._bLaunch_Click);
            // 
            // _rDescription
            // 
            resources.ApplyResources(this._rDescription, "_rDescription");
            this._rDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this._rDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._rDescription.ForeColor = System.Drawing.Color.White;
            this._rDescription.Name = "_rDescription";
            // 
            // _pImage
            // 
            resources.ApplyResources(this._pImage, "_pImage");
            this._pImage.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.ANX_Icon_100x100;
            this._pImage.Name = "_pImage";
            // 
            // _bOpen
            // 
            resources.ApplyResources(this._bOpen, "_bOpen");
            this._bOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this._bOpen.ForeColor = System.Drawing.Color.White;
            this._bOpen.Name = "_bOpen";
            this._bOpen.UseVisualStyleBackColor = false;
            this._bOpen.Click += new System.EventHandler(this._bOpen_Click);
            // 
            // SampleDataHalfVisual
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this._bOpen);
            this.Controls.Add(this._pImage);
            this.Controls.Add(this._rDescription);
            this.Controls.Add(this._bLaunch);
            this.Controls.Add(this._lName);
            this.Name = "SampleDataHalfVisual";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lName;
        private System.Windows.Forms.Button _bLaunch;
        private System.Windows.Forms.RichTextBox _rDescription;
        private System.Windows.Forms.Panel _pImage;
        private System.Windows.Forms.Button _bOpen;
    }
}
