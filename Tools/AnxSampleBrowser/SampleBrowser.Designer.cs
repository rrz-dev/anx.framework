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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this._pLogo = new System.Windows.Forms.Panel();
            this._lTitle = new System.Windows.Forms.Label();
            this._tSearch = new System.Windows.Forms.TextBox();
            this._lCurrentPage = new System.Windows.Forms.Label();
            this._lMaxPage = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _pSamples
            // 
            resources.ApplyResources(this._pSamples, "_pSamples");
            this._pSamples.BackColor = System.Drawing.Color.Transparent;
            this._pSamples.Name = "_pSamples";
            // 
            // _cFilter
            // 
            resources.ApplyResources(this._cFilter, "_cFilter");
            this._cFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._cFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._cFilter.ForeColor = System.Drawing.Color.White;
            this._cFilter.FormattingEnabled = true;
            this._cFilter.Name = "_cFilter";
            // 
            // _lFilter
            // 
            resources.ApplyResources(this._lFilter, "_lFilter");
            this._lFilter.BackColor = System.Drawing.Color.Transparent;
            this._lFilter.ForeColor = System.Drawing.Color.White;
            this._lFilter.Name = "_lFilter";
            // 
            // _lCategorie
            // 
            resources.ApplyResources(this._lCategorie, "_lCategorie");
            this._lCategorie.BackColor = System.Drawing.Color.Transparent;
            this._lCategorie.ForeColor = System.Drawing.Color.White;
            this._lCategorie.Name = "_lCategorie";
            // 
            // _dCategories
            // 
            resources.ApplyResources(this._dCategories, "_dCategories");
            this._dCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._dCategories.ForeColor = System.Drawing.Color.White;
            this._dCategories.FormattingEnabled = true;
            this._dCategories.Name = "_dCategories";
            this._dCategories.SelectedIndexChanged += new System.EventHandler(this._dCategories_SelectedIndexChanged);
            // 
            // _lSortBy
            // 
            resources.ApplyResources(this._lSortBy, "_lSortBy");
            this._lSortBy.BackColor = System.Drawing.Color.Transparent;
            this._lSortBy.ForeColor = System.Drawing.Color.White;
            this._lSortBy.Name = "_lSortBy";
            // 
            // _dSort
            // 
            resources.ApplyResources(this._dSort, "_dSort");
            this._dSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._dSort.ForeColor = System.Drawing.Color.White;
            this._dSort.FormattingEnabled = true;
            this._dSort.Name = "_dSort";
            this._dSort.SelectedIndexChanged += new System.EventHandler(this._cSort_SelectedIndexChanged);
            // 
            // _lPageIndex
            // 
            resources.ApplyResources(this._lPageIndex, "_lPageIndex");
            this._lPageIndex.BackColor = System.Drawing.Color.Transparent;
            this._lPageIndex.ForeColor = System.Drawing.Color.White;
            this._lPageIndex.Name = "_lPageIndex";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this._pLogo);
            this.panel1.Controls.Add(this._lTitle);
            this.panel1.Controls.Add(this._tSearch);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.magnifying_glass;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // _pLogo
            // 
            this._pLogo.BackColor = System.Drawing.Color.Transparent;
            this._pLogo.BackgroundImage = global::AnxSampleBrowser.Properties.Resources.ANX_Framework_Logo_120x32;
            resources.ApplyResources(this._pLogo, "_pLogo");
            this._pLogo.Name = "_pLogo";
            // 
            // _lTitle
            // 
            resources.ApplyResources(this._lTitle, "_lTitle");
            this._lTitle.BackColor = System.Drawing.Color.Transparent;
            this._lTitle.ForeColor = System.Drawing.Color.White;
            this._lTitle.Name = "_lTitle";
            // 
            // _tSearch
            // 
            this._tSearch.AcceptsReturn = true;
            resources.ApplyResources(this._tSearch, "_tSearch");
            this._tSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._tSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._tSearch.ForeColor = System.Drawing.Color.White;
            this._tSearch.Name = "_tSearch";
            this._tSearch.TextChanged += new System.EventHandler(this._tSearch_TextChanged);
            this._tSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this._tSearch_KeyDown);
            // 
            // _lCurrentPage
            // 
            resources.ApplyResources(this._lCurrentPage, "_lCurrentPage");
            this._lCurrentPage.BackColor = System.Drawing.Color.Transparent;
            this._lCurrentPage.ForeColor = System.Drawing.Color.White;
            this._lCurrentPage.Name = "_lCurrentPage";
            // 
            // _lMaxPage
            // 
            resources.ApplyResources(this._lMaxPage, "_lMaxPage");
            this._lMaxPage.BackColor = System.Drawing.Color.Transparent;
            this._lMaxPage.ForeColor = System.Drawing.Color.White;
            this._lMaxPage.Name = "_lMaxPage";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::AnxSampleBrowser.Properties.Resources.appbar_arrow_right;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.DoubleClick += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::AnxSampleBrowser.Properties.Resources.appbar_arrow_left;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_Click);
            // 
            // AnxSampleBrowser
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this._lMaxPage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._dSort);
            this.Controls.Add(this._lPageIndex);
            this.Controls.Add(this._pSamples);
            this.Controls.Add(this._lCurrentPage);
            this.Controls.Add(this._lFilter);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._lSortBy);
            this.Controls.Add(this._cFilter);
            this.Controls.Add(this._lCategorie);
            this.Controls.Add(this._dCategories);
            this.Name = "AnxSampleBrowser";
            this.Load += new System.EventHandler(this.AnxSampleBrowser_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel _pLogo;
        private System.Windows.Forms.TextBox _tSearch;
        private System.Windows.Forms.Label _lTitle;
        private System.Windows.Forms.Panel _pSamples;
        private System.Windows.Forms.CheckedListBox _cFilter;
        private System.Windows.Forms.Label _lFilter;
        private System.Windows.Forms.Label _lCategorie;
        private System.Windows.Forms.ComboBox _dCategories;
        private System.Windows.Forms.Label _lSortBy;
        private System.Windows.Forms.ComboBox _dSort;
        private System.Windows.Forms.Label _lPageIndex;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label _lCurrentPage;
        private System.Windows.Forms.Label _lMaxPage;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

