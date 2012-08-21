namespace AnxSampleBrowser
{
    partial class SampleDataVisual
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
            this._pImage = new System.Windows.Forms.PictureBox();
            this._lName = new System.Windows.Forms.Label();
            this._bLaunch = new System.Windows.Forms.Button();
            this._rDescription = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this._pImage)).BeginInit();
            this.SuspendLayout();
            // 
            // _pImage
            // 
            this._pImage.ErrorImage = null;
            this._pImage.ImageLocation = "0;0";
            this._pImage.Location = new System.Drawing.Point(481, 28);
            this._pImage.Name = "_pImage";
            this._pImage.Size = new System.Drawing.Size(100, 100);
            this._pImage.TabIndex = 0;
            this._pImage.TabStop = false;
            // 
            // _lName
            // 
            this._lName.AutoSize = true;
            this._lName.BackColor = System.Drawing.Color.Transparent;
            this._lName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lName.Location = new System.Drawing.Point(3, 19);
            this._lName.Name = "_lName";
            this._lName.Size = new System.Drawing.Size(64, 26);
            this._lName.TabIndex = 1;
            this._lName.Text = "label1";
            // 
            // _bLaunch
            // 
            this._bLaunch.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.button;
            this._bLaunch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._bLaunch.Location = new System.Drawing.Point(587, 65);
            this._bLaunch.Name = "_bLaunch";
            this._bLaunch.Size = new System.Drawing.Size(101, 26);
            this._bLaunch.TabIndex = 2;
            this._bLaunch.UseVisualStyleBackColor = true;
            this._bLaunch.Click += new System.EventHandler(this._bLaunch_Click);
            // 
            // _rDescription
            // 
            this._rDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(157)))), ((int)(((byte)(23)))));
            this._rDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._rDescription.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rDescription.Location = new System.Drawing.Point(8, 48);
            this._rDescription.Name = "_rDescription";
            this._rDescription.Size = new System.Drawing.Size(452, 93);
            this._rDescription.TabIndex = 3;
            this._rDescription.Text = "";
            // 
            // SampleDataVisual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.sample1;
            this.Controls.Add(this._pImage);
            this.Controls.Add(this._rDescription);
            this.Controls.Add(this._bLaunch);
            this.Controls.Add(this._lName);
            this.Name = "SampleDataVisual";
            this.Size = new System.Drawing.Size(700, 150);
            ((System.ComponentModel.ISupportInitialize)(this._pImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _pImage;
        private System.Windows.Forms.Label _lName;
        private System.Windows.Forms.Button _bLaunch;
        private System.Windows.Forms.RichTextBox _rDescription;
    }
}
