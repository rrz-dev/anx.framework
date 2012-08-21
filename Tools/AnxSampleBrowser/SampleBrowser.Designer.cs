namespace AnxSampleBrowser
{
    partial class AnxSampleBrowser
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
            this._pSamples = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._lTitle = new System.Windows.Forms.Label();
            this._bClear = new System.Windows.Forms.Button();
            this._tSearch = new System.Windows.Forms.TextBox();
            this._bSearch = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this._dCategories = new System.Windows.Forms.ComboBox();
            this._lCategorie = new System.Windows.Forms.Label();
            this._lFilter = new System.Windows.Forms.Label();
            this._cFilter = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pSamples
            // 
            this._pSamples.AutoScroll = true;
            this._pSamples.BackColor = System.Drawing.Color.Transparent;
            this._pSamples.Location = new System.Drawing.Point(0, 56);
            this._pSamples.Name = "_pSamples";
            this._pSamples.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._pSamples.Size = new System.Drawing.Size(730, 470);
            this._pSamples.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.header;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this._lTitle);
            this.panel1.Controls.Add(this._bClear);
            this.panel1.Controls.Add(this._tSearch);
            this.panel1.Controls.Add(this._bSearch);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 50);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.ANX_Framework;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(13, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(119, 43);
            this.panel2.TabIndex = 3;
            // 
            // _lTitle
            // 
            this._lTitle.AutoSize = true;
            this._lTitle.BackColor = System.Drawing.Color.Transparent;
            this._lTitle.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(157)))), ((int)(((byte)(23)))));
            this._lTitle.Location = new System.Drawing.Point(138, 9);
            this._lTitle.Name = "_lTitle";
            this._lTitle.Size = new System.Drawing.Size(213, 36);
            this._lTitle.TabIndex = 2;
            this._lTitle.Text = "Sample Browser";
            // 
            // _bClear
            // 
            this._bClear.Location = new System.Drawing.Point(838, 12);
            this._bClear.Name = "_bClear";
            this._bClear.Size = new System.Drawing.Size(84, 23);
            this._bClear.TabIndex = 4;
            this._bClear.Text = "Clear";
            this._bClear.UseVisualStyleBackColor = true;
            this._bClear.Click += new System.EventHandler(this._bClear_Click);
            // 
            // _tSearch
            // 
            this._tSearch.Location = new System.Drawing.Point(567, 14);
            this._tSearch.Name = "_tSearch";
            this._tSearch.Size = new System.Drawing.Size(159, 20);
            this._tSearch.TabIndex = 1;
            // 
            // _bSearch
            // 
            this._bSearch.Location = new System.Drawing.Point(751, 12);
            this._bSearch.Name = "_bSearch";
            this._bSearch.Size = new System.Drawing.Size(84, 23);
            this._bSearch.TabIndex = 0;
            this._bSearch.Text = "Search";
            this._bSearch.UseVisualStyleBackColor = true;
            this._bSearch.Click += new System.EventHandler(this._bSearch_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this._dCategories);
            this.panel3.Controls.Add(this._lCategorie);
            this.panel3.Controls.Add(this._lFilter);
            this.panel3.Controls.Add(this._cFilter);
            this.panel3.Location = new System.Drawing.Point(736, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 486);
            this.panel3.TabIndex = 1;
            // 
            // _dCategories
            // 
            this._dCategories.FormattingEnabled = true;
            this._dCategories.Location = new System.Drawing.Point(15, 260);
            this._dCategories.Name = "_dCategories";
            this._dCategories.Size = new System.Drawing.Size(171, 21);
            this._dCategories.TabIndex = 3;
            this._dCategories.SelectedIndexChanged += new System.EventHandler(this._dCategories_SelectedIndexChanged);
            // 
            // _lCategorie
            // 
            this._lCategorie.AutoSize = true;
            this._lCategorie.BackColor = System.Drawing.Color.Transparent;
            this._lCategorie.Font = new System.Drawing.Font("Calibri", 16F);
            this._lCategorie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(157)))), ((int)(((byte)(23)))));
            this._lCategorie.Location = new System.Drawing.Point(3, 230);
            this._lCategorie.Name = "_lCategorie";
            this._lCategorie.Size = new System.Drawing.Size(99, 27);
            this._lCategorie.TabIndex = 2;
            this._lCategorie.Text = "Categorie";
            // 
            // _lFilter
            // 
            this._lFilter.AutoSize = true;
            this._lFilter.BackColor = System.Drawing.Color.Transparent;
            this._lFilter.Font = new System.Drawing.Font("Calibri", 16F);
            this._lFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(157)))), ((int)(((byte)(23)))));
            this._lFilter.Location = new System.Drawing.Point(3, 17);
            this._lFilter.Name = "_lFilter";
            this._lFilter.Size = new System.Drawing.Size(58, 27);
            this._lFilter.TabIndex = 1;
            this._lFilter.Text = "Filter";
            // 
            // _cFilter
            // 
            this._cFilter.FormattingEnabled = true;
            this._cFilter.Location = new System.Drawing.Point(15, 47);
            this._cFilter.Name = "_cFilter";
            this._cFilter.Size = new System.Drawing.Size(171, 169);
            this._cFilter.TabIndex = 0;
            // 
            // AnxSampleBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.header;
            this.ClientSize = new System.Drawing.Size(934, 522);
            this.Controls.Add(this._pSamples);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "AnxSampleBrowser";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox _tSearch;
        private System.Windows.Forms.Button _bSearch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button _bClear;
        private System.Windows.Forms.Label _lFilter;
        private System.Windows.Forms.CheckedListBox _cFilter;
        private System.Windows.Forms.Label _lTitle;
        private System.Windows.Forms.Panel _pSamples;
        private System.Windows.Forms.ComboBox _dCategories;
        private System.Windows.Forms.Label _lCategorie;
    }
}

