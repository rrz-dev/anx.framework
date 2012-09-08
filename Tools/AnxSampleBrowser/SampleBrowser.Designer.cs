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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnxSampleBrowser));
            this._pSamples = new System.Windows.Forms.Panel();
            this._cFilter = new System.Windows.Forms.CheckedListBox();
            this._lFilter = new System.Windows.Forms.Label();
            this._lCategorie = new System.Windows.Forms.Label();
            this._dCategories = new System.Windows.Forms.ComboBox();
            this._lSortBy = new System.Windows.Forms.Label();
            this._dSort = new System.Windows.Forms.ComboBox();
            this._lPageIndex = new System.Windows.Forms.Label();
            this._bApply = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this._pLogo = new System.Windows.Forms.Panel();
            this._lTitle = new System.Windows.Forms.Label();
            this._bClear = new System.Windows.Forms.Button();
            this._tSearch = new System.Windows.Forms.TextBox();
            this._bSearch = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // _pSamples
            // 
            this._pSamples.BackColor = System.Drawing.Color.Transparent;
            this._pSamples.Location = new System.Drawing.Point(3, 68);
            this._pSamples.Name = "_pSamples";
            this._pSamples.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._pSamples.Size = new System.Drawing.Size(730, 442);
            this._pSamples.TabIndex = 2;
            // 
            // _cFilter
            // 
            this._cFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._cFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._cFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._cFilter.ForeColor = System.Drawing.Color.White;
            this._cFilter.FormattingEnabled = true;
            this._cFilter.Location = new System.Drawing.Point(751, 91);
            this._cFilter.Name = "_cFilter";
            this._cFilter.Size = new System.Drawing.Size(171, 160);
            this._cFilter.TabIndex = 0;
            // 
            // _lFilter
            // 
            this._lFilter.AutoSize = true;
            this._lFilter.BackColor = System.Drawing.Color.Transparent;
            this._lFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lFilter.ForeColor = System.Drawing.Color.White;
            this._lFilter.Location = new System.Drawing.Point(739, 61);
            this._lFilter.Name = "_lFilter";
            this._lFilter.Size = new System.Drawing.Size(47, 21);
            this._lFilter.TabIndex = 1;
            this._lFilter.Text = "Filter";
            // 
            // _lCategorie
            // 
            this._lCategorie.AutoSize = true;
            this._lCategorie.BackColor = System.Drawing.Color.Transparent;
            this._lCategorie.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lCategorie.ForeColor = System.Drawing.Color.White;
            this._lCategorie.Location = new System.Drawing.Point(739, 274);
            this._lCategorie.Name = "_lCategorie";
            this._lCategorie.Size = new System.Drawing.Size(82, 21);
            this._lCategorie.TabIndex = 2;
            this._lCategorie.Text = "Categorie";
            // 
            // _dCategories
            // 
            this._dCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._dCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dCategories.ForeColor = System.Drawing.Color.White;
            this._dCategories.FormattingEnabled = true;
            this._dCategories.Location = new System.Drawing.Point(751, 304);
            this._dCategories.Name = "_dCategories";
            this._dCategories.Size = new System.Drawing.Size(171, 21);
            this._dCategories.TabIndex = 3;
            // 
            // _lSortBy
            // 
            this._lSortBy.AutoSize = true;
            this._lSortBy.BackColor = System.Drawing.Color.Transparent;
            this._lSortBy.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lSortBy.ForeColor = System.Drawing.Color.White;
            this._lSortBy.Location = new System.Drawing.Point(739, 389);
            this._lSortBy.Name = "_lSortBy";
            this._lSortBy.Size = new System.Drawing.Size(67, 21);
            this._lSortBy.TabIndex = 4;
            this._lSortBy.Text = "Sort by:";
            // 
            // _dSort
            // 
            this._dSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._dSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._dSort.ForeColor = System.Drawing.Color.White;
            this._dSort.FormattingEnabled = true;
            this._dSort.Items.AddRange(new object[] {
            "Name",
            "Categorie"});
            this._dSort.Location = new System.Drawing.Point(751, 419);
            this._dSort.Name = "_dSort";
            this._dSort.Size = new System.Drawing.Size(171, 23);
            this._dSort.TabIndex = 5;
            this._dSort.SelectedIndexChanged += new System.EventHandler(this._cSort_SelectedIndexChanged);
            // 
            // _lPageIndex
            // 
            this._lPageIndex.AutoSize = true;
            this._lPageIndex.BackColor = System.Drawing.Color.Transparent;
            this._lPageIndex.Font = new System.Drawing.Font("Segoe UI", 15F);
            this._lPageIndex.ForeColor = System.Drawing.Color.White;
            this._lPageIndex.Location = new System.Drawing.Point(807, 464);
            this._lPageIndex.Name = "_lPageIndex";
            this._lPageIndex.Size = new System.Drawing.Size(64, 28);
            this._lPageIndex.TabIndex = 11;
            this._lPageIndex.Text = "99/99";
            // 
            // _bApply
            // 
            this._bApply.BackColor = System.Drawing.SystemColors.Desktop;
            this._bApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bApply.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this._bApply.ForeColor = System.Drawing.Color.White;
            this._bApply.Location = new System.Drawing.Point(751, 341);
            this._bApply.Name = "_bApply";
            this._bApply.Size = new System.Drawing.Size(171, 34);
            this._bApply.TabIndex = 5;
            this._bApply.Text = "Apply";
            this._bApply.UseVisualStyleBackColor = false;
            this._bApply.Click += new System.EventHandler(this._bApply_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.panel1.Controls.Add(this._pLogo);
            this.panel1.Controls.Add(this._lTitle);
            this.panel1.Controls.Add(this._bClear);
            this.panel1.Controls.Add(this._tSearch);
            this.panel1.Controls.Add(this._bSearch);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 50);
            this.panel1.TabIndex = 0;
            // 
            // _pLogo
            // 
            this._pLogo.BackColor = System.Drawing.Color.Transparent;
            this._pLogo.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.ANX_Framework_Logo_120x32;
            this._pLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._pLogo.Location = new System.Drawing.Point(13, 4);
            this._pLogo.Name = "_pLogo";
            this._pLogo.Size = new System.Drawing.Size(119, 43);
            this._pLogo.TabIndex = 3;
            // 
            // _lTitle
            // 
            this._lTitle.AutoSize = true;
            this._lTitle.BackColor = System.Drawing.Color.Transparent;
            this._lTitle.Font = new System.Drawing.Font("Segoe UI", 16F);
            this._lTitle.ForeColor = System.Drawing.Color.White;
            this._lTitle.Location = new System.Drawing.Point(138, 9);
            this._lTitle.Name = "_lTitle";
            this._lTitle.Size = new System.Drawing.Size(170, 30);
            this._lTitle.TabIndex = 2;
            this._lTitle.Text = "Sample Browser";
            // 
            // _bClear
            // 
            this._bClear.BackColor = System.Drawing.SystemColors.Desktop;
            this._bClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._bClear.ForeColor = System.Drawing.Color.White;
            this._bClear.Location = new System.Drawing.Point(838, 12);
            this._bClear.Name = "_bClear";
            this._bClear.Size = new System.Drawing.Size(84, 33);
            this._bClear.TabIndex = 4;
            this._bClear.Text = "Clear";
            this._bClear.UseVisualStyleBackColor = false;
            this._bClear.Click += new System.EventHandler(this._bClear_Click);
            // 
            // _tSearch
            // 
            this._tSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._tSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._tSearch.Font = new System.Drawing.Font("Segoe UI", 18F);
            this._tSearch.ForeColor = System.Drawing.Color.White;
            this._tSearch.Location = new System.Drawing.Point(446, 12);
            this._tSearch.Name = "_tSearch";
            this._tSearch.Size = new System.Drawing.Size(299, 32);
            this._tSearch.TabIndex = 1;
            // 
            // _bSearch
            // 
            this._bSearch.BackColor = System.Drawing.SystemColors.Desktop;
            this._bSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._bSearch.ForeColor = System.Drawing.Color.White;
            this._bSearch.Location = new System.Drawing.Point(751, 12);
            this._bSearch.Name = "_bSearch";
            this._bSearch.Size = new System.Drawing.Size(84, 33);
            this._bSearch.TabIndex = 0;
            this._bSearch.Text = "Search";
            this._bSearch.UseVisualStyleBackColor = false;
            this._bSearch.Click += new System.EventHandler(this._bSearch_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AnxSampleBrowser.Properties.Resources.appbar_arrow_left;
            this.pictureBox1.Location = new System.Drawing.Point(751, 457);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 50);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AnxSampleBrowser.Properties.Resources.appbar_arrow_right;
            this.pictureBox2.Location = new System.Drawing.Point(880, 457);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 50);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // AnxSampleBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(934, 522);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._bApply);
            this.Controls.Add(this._lPageIndex);
            this.Controls.Add(this._dSort);
            this.Controls.Add(this._pSamples);
            this.Controls.Add(this._lFilter);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._lSortBy);
            this.Controls.Add(this._cFilter);
            this.Controls.Add(this._lCategorie);
            this.Controls.Add(this._dCategories);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnxSampleBrowser";
            this.Text = "ANX.Framework Sample Browser";
            this.Load += new System.EventHandler(this.AnxSampleBrowser_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel _pLogo;
        private System.Windows.Forms.TextBox _tSearch;
        private System.Windows.Forms.Button _bSearch;
        private System.Windows.Forms.Button _bClear;
        private System.Windows.Forms.Label _lTitle;
        private System.Windows.Forms.Panel _pSamples;
        private System.Windows.Forms.CheckedListBox _cFilter;
        private System.Windows.Forms.Label _lFilter;
        private System.Windows.Forms.Label _lCategorie;
        private System.Windows.Forms.ComboBox _dCategories;
        private System.Windows.Forms.Label _lSortBy;
        private System.Windows.Forms.ComboBox _dSort;
        private System.Windows.Forms.Label _lPageIndex;
        private System.Windows.Forms.Button _bApply;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

