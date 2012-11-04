using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Linq;

namespace AnxSampleBrowser
{
    /// <summary>
    /// WinForm project which let you browse through the ANX samples
    /// </summary>
    public partial class AnxSampleBrowser : Form
    {
        //list of all registered samples
        private List<SampleData> _sampleAtlas;
        private List<SampleData> _filteredSamples;

        private int _currentPage = 1;
        private int _pageCount;
        //default path to the samples folder
        public String SamplePath { get { return "../Samples/"; } }

        /// <summary>
        /// Constructor of the browser
        /// parses the xml and set up lists
        /// </summary>
        public AnxSampleBrowser()
        {
            
            InitializeComponent();
            //make the form fixed size
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;

            //init sample list
            _sampleAtlas = new List<SampleData>();
            _filteredSamples = new List<SampleData>();
            //add default categorie (everything)...
            _dCategories.Items.Add("all");
            //...and select it
            _dCategories.SelectedIndex = 0;
            _dCategories.DropDownStyle = ComboBoxStyle.DropDownList;

          
        
            //load out Samples.xml
            parseXMl();
            addAll();
            calculatePages();

            _dSort.SelectedIndex = 0;
            _dSort.DropDownStyle = ComboBoxStyle.DropDownList;
          
            _cFilter.CheckOnClick = true;

           
            for (int i = 0; i < _cFilter.Items.Count; ++i)
            {
                _cFilter.SetItemChecked(i, true);
            }
           // this._cFilter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this._cFilter_ItemCheck);

            


          
         
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp; 
            }
        }

        private void calculatePages()
        {
            _pSamples.Controls.Clear();
            if (_filteredSamples.Count == 0)
                return;
            _pageCount = (int)Math.Ceiling(_filteredSamples.Count / 6f);
            while (_currentPage > _pageCount)
                _currentPage--;

            _lCurrentPage.Text = _currentPage.ToString();
            _lMaxPage.Text = _pageCount.ToString();
          
            for (int i = 6 * (_currentPage - 1); i < _currentPage * 6 && i < _filteredSamples.Count; ++i)
            {

                addSampleDataVisual(_filteredSamples[i]);
            }
        }

        private void parseXMl()
        {
            XmlDocument sampleDescription = new XmlDocument();
            try
            {
                sampleDescription.Load(SamplePath+"SampleCatalog.xml");
            }
            catch (FileNotFoundException)
            {
                OpenFileDialog openSampleXML = new OpenFileDialog();
                openSampleXML.Filter = "xml files (*.xml)|*.xml";
                openSampleXML.Title="Pick SampleCatalog.xml";
                if (openSampleXML.ShowDialog() == DialogResult.OK)
                {

                    sampleDescription.Load(openSampleXML.FileName);
                }
                else
                {
                    MessageBox.Show("Samples.xml not found");
                    this.Close();
                }

            }
            XmlNode sampleRoot = sampleDescription.ChildNodes[1];

            List<String> _categorieAtlas = new List<string>();
          
         List<String> _tagAtlas=new List<string>();
          
            foreach (XmlNode sample in sampleRoot.ChildNodes)
            {
               
                SampleData data = new SampleData(){
                    Name = sample["Name"].InnerText,
                    Description = sample["Description"].InnerText,
                    ExecPath = sample["ExecPath"].InnerText,
                    Categorie = sample["Categorie"].InnerText                                     
                };
                XmlNode imagePath = sample["ImagePath"];
                if (imagePath != null)
                    data.ImagePath = imagePath.InnerText;
                else
                    data.ImagePath = "";
                data.Validate();
                if (!_categorieAtlas.Contains(sample["Categorie"].InnerText))
                    _categorieAtlas.Add(sample["Categorie"].InnerText);
                XmlNode tags = sample["Tags"];
                List<String> dataTags=new List<string>();
                foreach (XmlNode tag in tags.ChildNodes)
                {
                    if (!_tagAtlas.Contains(tag.InnerText))
                        _tagAtlas.Add(tag.InnerText);
                    dataTags.Add(tag.InnerText);
                }
                data.Tags = dataTags.ToArray();
                _sampleAtlas.Add(data);
                addSampleDataVisual(data);
            }

            _dCategories.Items.AddRange(_categorieAtlas.ToArray());

           _cFilter.Items.AddRange(_tagAtlas.ToArray());

        
      

        }

        private void addAll()
        {
            _filteredSamples.Clear();
            _filteredSamples.AddRange(_sampleAtlas);
            calculatePages();
        }

        private void addAll(List<SampleData> value)
        {
            _filteredSamples.Clear();
            _filteredSamples.AddRange(value);
            calculatePages();
        }

      

        private bool isFiltered(SampleData data)
        {
            foreach (String tag in data.Tags)
            {
                if(_cFilter.CheckedItems.Contains(tag))
                    return true;
            }
            return false;
        }

        private void addSampleDataVisual(SampleData data)
        {
            SampleDataHalfVisual dataV = new SampleDataHalfVisual(data, this);
            if (this._pSamples.Controls.Count % 2 == 0)
            dataV.Location = new Point(0, (this._pSamples.Controls.Count/2) * (dataV.Size.Height + 5));
            else
                dataV.Location = new Point(dataV.Width+5, ((this._pSamples.Controls.Count-1)/2) * (dataV.Size.Height + 5));
            this._pSamples.Controls.Add(dataV);
        }

     
        private void search()
        {
            _filteredSamples.Clear();
            String[] phrases=_tSearch.Text.Split(new char[]{' ',',',';'});
            this._pSamples.Controls.Clear();
            foreach (SampleData data in _sampleAtlas)
            {
                bool add=false;
                foreach (string phrase in phrases)
                {
                    if (data.Name.Contains(phrase, StringComparison.OrdinalIgnoreCase) || 
                        data.Description.Contains(phrase, StringComparison.OrdinalIgnoreCase) || 
                        data.Categorie.Contains(phrase, StringComparison.OrdinalIgnoreCase))
                    {
                        add = true;
                        break;
                    }     
                    foreach (String tag in data.Tags)
                    {
                        if (tag.ToLower().Equals(phrase.ToLower()))
                        {
                            add = true;
                            break;
                        }
                    }                        
                }
                if (add)
                    _filteredSamples.Add(data);
            }
            calculatePages();
        }

  

        private void _bSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void _bClear_Click(object sender, EventArgs e)
        {
            this._tSearch.Text = "";
            addAll();
        }

        private void AnxSampleBrowser_Load(object sender, EventArgs e)
        {
            this.Text += " v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void _cSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dSort.SelectedItem.Equals("categorie"))
            {
                _sampleAtlas.Sort((x, y) => x.Categorie.CompareTo(y.Categorie));
                _filteredSamples.Sort((x, y) => x.Categorie.CompareTo(y.Categorie));  
                return;
            }
            if (_dSort.SelectedItem.Equals("name"))
            {
                _sampleAtlas.Sort((x, y) => x.Name.CompareTo(y.Name));
                _filteredSamples.Sort((x, y) => x.Name.CompareTo(y.Name));
                return;
            }
            

        }

        private void _bApply_Click(object sender, EventArgs e)
        {
            _filteredSamples.Clear();
            foreach (SampleData data in _sampleAtlas)
            {
                if (_dCategories.SelectedItem.Equals("all") || data.Categorie.Equals(_dCategories.SelectedItem))
                {
                    if(isFiltered(data))
                    {
                        _filteredSamples.Add(data);
                    }
                }
            }
            calculatePages() ;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            _currentPage = _currentPage+1 > _pageCount ? _currentPage : _currentPage+1;
            calculatePages();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _currentPage = _currentPage - 1 <= 0 ? _currentPage : _currentPage - 1;
            calculatePages();
        }

    }

    static class StringExtender
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }

}
