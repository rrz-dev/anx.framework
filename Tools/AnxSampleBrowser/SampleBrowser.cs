using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace AnxSampleBrowser
{
    public partial class AnxSampleBrowser : Form
    {
        //list of all registered samples
        private List<SampleData> _sampleAtlas;
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
            //add default categorie (everything)...
            _dCategories.Items.Add("all");
            //...and select it
            _dCategories.SelectedIndex = 0;
            _dCategories.DropDownStyle = ComboBoxStyle.DropDownList;

            //load out Samples.xml
            parseXMl();
           

          
            _cFilter.CheckOnClick = true;
            for (int i = 0; i < _cFilter.Items.Count; ++i)
            {
                _cFilter.SetItemChecked(i, true);
            }
            this._cFilter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this._cFilter_ItemCheck);

            


          
         
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

        private void parseXMl()
        {
            XmlDocument sampleDescription = new XmlDocument();
            try
            {
                sampleDescription.Load(SamplePath+"SampleCatalog.xml");
            }
            catch (FileNotFoundException ex)
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
            _pSamples.Controls.Clear();
            foreach (SampleData data in _sampleAtlas)
            {
                addSampleDataVisual(data);
            }
        }

        private void addSampleDataVisual(SampleData data)
        {
            SampleDataVisual dataV = new SampleDataVisual(data,this);
            dataV.Location = new Point(0, this._pSamples.Controls.Count * (dataV.Size.Height + 5));
            this._pSamples.Controls.Add(dataV);
        }

     
        private void search()
        {
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
                    addSampleDataVisual(data);
            }
        }

        private void pickCategorie()
        {
            _pSamples.Controls.Clear();
            foreach (SampleData data in _sampleAtlas)
            {
                if (data.Categorie.Equals(_dCategories.SelectedItem))
                addSampleDataVisual(data);
            }
        }

        private void filter()
        {
            _pSamples.Controls.Clear();
            foreach (SampleData data in _sampleAtlas)
            {
                foreach (String tag in data.Tags)
                {
                    if (_cFilter.CheckedItems.Contains(tag))
                    {
                        addSampleDataVisual(data);
                        break;
                    }
                }

            }
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

        private void _dCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dCategories.Items.Count == 0)
                return;
            if (_dCategories.SelectedItem.Equals("all"))
                addAll();
            else
                pickCategorie();
        }

        private void _cFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate { filter(); });
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
