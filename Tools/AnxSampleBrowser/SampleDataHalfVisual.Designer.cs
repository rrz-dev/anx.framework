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
            this._lName = new System.Windows.Forms.Label();
            this._bLaunch = new System.Windows.Forms.Button();
            this._rDescription = new System.Windows.Forms.RichTextBox();
            this._pImage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _lName
            // 
            this._lName.AutoSize = true;
            this._lName.BackColor = System.Drawing.Color.Transparent;
            this._lName.Font = new System.Drawing.Font("Segoe WP", 15.75F);
            this._lName.ForeColor = System.Drawing.Color.White;
            this._lName.Location = new System.Drawing.Point(5, 10);
            this._lName.Name = "_lName";
            this._lName.Size = new System.Drawing.Size(64, 28);
            this._lName.TabIndex = 1;
            this._lName.Text = "label1";
            // 
            // _bLaunch
            // 
            this._bLaunch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this._bLaunch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._bLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bLaunch.Font = new System.Drawing.Font("Segoe WP", 10F, System.Drawing.FontStyle.Bold);
            this._bLaunch.ForeColor = System.Drawing.Color.White;
            this._bLaunch.Location = new System.Drawing.Point(259, 103);
            this._bLaunch.Name = "_bLaunch";
            this._bLaunch.Size = new System.Drawing.Size(90, 26);
            this._bLaunch.TabIndex = 2;
            this._bLaunch.Text = "Launch";
            this._bLaunch.UseVisualStyleBackColor = false;
            this._bLaunch.Click += new System.EventHandler(this._bLaunch_Click);
            // 
            // _rDescription
            // 
            this._rDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this._rDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._rDescription.Font = new System.Drawing.Font("Segoe WP", 12F);
            this._rDescription.ForeColor = System.Drawing.Color.White;
            this._rDescription.Location = new System.Drawing.Point(10, 39);
            this._rDescription.Name = "_rDescription";
            this._rDescription.Size = new System.Drawing.Size(243, 90);
            this._rDescription.TabIndex = 3;
            this._rDescription.Text = "";
            // 
            // _pImage
            // 
            this._pImage.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.ANX_Icon_100x100;
            this._pImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._pImage.Location = new System.Drawing.Point(259, 10);
            this._pImage.Name = "_pImage";
            this._pImage.Size = new System.Drawing.Size(90, 90);
            this._pImage.TabIndex = 4;
            // 
            // SampleDataHalfVisual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this._pImage);
            this.Controls.Add(this._rDescription);
            this.Controls.Add(this._bLaunch);
            this.Controls.Add(this._lName);
            this.Name = "SampleDataHalfVisual";
            this.Size = new System.Drawing.Size(358, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lName;
        private System.Windows.Forms.Button _bLaunch;
        private System.Windows.Forms.RichTextBox _rDescription;
        private System.Windows.Forms.Panel _pImage;
    }
}
