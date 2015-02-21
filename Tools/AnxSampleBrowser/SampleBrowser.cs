using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AnxSampleBrowser.Resources;

namespace AnxSampleBrowser
{
    /// <summary>
    /// WinForm project which let you browse through the ANX samples
    /// </summary>
    public partial class AnxSampleBrowser : Form
    {
        //list of all registered samples
        private List<SampleData> _sampleAtlas;
        private SortedList<SampleData> _filteredSamples;
        private Dictionary<SampleData, SampleDataHalfVisual> _sampleDataControlMap;
        private CancellationTokenSource _searchCancellationTokenSource;
        private object _filteredSamplesLock = new object();
        private volatile bool _alreadyRunInvoke = false;

        private int _previousPage = -1;
        private int _currentPage = 1;
        private int _pageCount;
        //default path to the samples folder
        public String SamplePath
        {
            get;
            private set;
        }

        DropDownElement _sortByName = new DropDownElement("name", StringResources.Name);
        DropDownElement _sortByCategory = new DropDownElement("category", StringResources.Category);
        DropDownElement _dontFilterAll = new DropDownElement("all", StringResources.All);
        DropDownElement _selectedCategory = null;

        /// <summary>
        /// Constructor of the browser
        /// parses the xml and set up lists
        /// </summary>
        public AnxSampleBrowser()
        {
            SamplePath = "../../Samples/";

            InitializeComponent();
            //make the form fixed size
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;

            _dSort.Items.Add(_sortByName);
            _dSort.Items.Add(_sortByCategory);
            _dSort.SelectedIndex = 0;
            _dSort.DropDownStyle = ComboBoxStyle.DropDownList;

            //add default categorie (everything)...
            _dCategories.Items.Add(_dontFilterAll);
            //...and select it
            _dCategories.SelectedIndex = 0;
            _dCategories.DropDownStyle = ComboBoxStyle.DropDownList;

            //init sample list
            _sampleAtlas = new List<SampleData>();
            _filteredSamples = new SortedList<SampleData>(SamplesComparer);
            _sampleDataControlMap = new Dictionary<SampleData, SampleDataHalfVisual>();

            Application.Idle += Application_Idle;

            //load out Samples.xml
            parseXMl();
            addAll();
            buildPages();

            Size originalSize = _pSamples.Size;
            int right = 0;
            int bottom = 0;
            foreach (Control c in _pSamples.Controls)
            {
                right = Math.Max(right, c.Location.X + c.Size.Width);
                bottom = Math.Max(bottom, c.Location.Y + c.Size.Height);
            }

            //_pSamples gets resized by changing the size of the window.
            this.ClientSize = new System.Drawing.Size(ClientSize.Width + right - originalSize.Width, ClientSize.Height + bottom - originalSize.Height);

            _cFilter.CheckOnClick = true;


            for (int i = 0; i < _cFilter.Items.Count; ++i)
            {
                _cFilter.SetItemChecked(i, true);
            }
            this._cFilter.ItemCheck += _cFilter_ItemCheck;
        }

        void _cFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            search();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            _alreadyRunInvoke = false;
        }

        private IComparer<SampleData> SamplesComparer
        {
            get
            {
                if (_dSort.SelectedItem == _sortByCategory)
                {
                    return SampleDataCategoryComparer.Instance;
                }
                else if (_dSort.SelectedItem == _sortByName)
                {
                    return SampleDataNameComparer.Instance;
                }
                else
                    throw new InvalidOperationException("No sort mode specified.");
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //Activating DoubleBuffered for form and child controls where possible.
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void buildPages()
        {
            Action body = () =>
                {
                    //Makes this async code on the main thread more performant by running it only once per update.
                    //It can happen that we trigger the buildPage several times before the app gets idle again, in which 
                    //it would usually work through the calls we gave by calling invoke. When that happens, it would run this 
                    //code multiple successive times, but in between these calls it doesn't have the chance to manipulate the
                    //data upon which we build these controls. Which means, it would be a wasted effort.
                    //But this check is also from the time were the controls weren't cached and the performance difference isn't
                    //as big anymore as it once was.
                    if (_alreadyRunInvoke)
                        return;


                    bool needsRebuild = false;
                    var controls = _pSamples.Controls.Cast<SampleDataHalfVisual>().ToArray();
                    if (_filteredSamples.Count != 0)
                    {
                        var sourceData = _filteredSamples.GetRange((_currentPage - 1) * 6, Math.Min(6, _filteredSamples.Count - (_currentPage - 1) * 6));
                        if (controls.Length == sourceData.Count)
                        {
                            for (int i = 0; i < controls.Length; i++)
                            {
                                var data = controls[i].SampleData;
                                if (data != sourceData[i])
                                {
                                    needsRebuild = true;
                                    break;
                                }
                            }
                        }
                        else
                            needsRebuild = true;
                    }
                    else
                        needsRebuild = true;

                    if (_previousPage != _currentPage)
                        needsRebuild = true;

                    _previousPage = _currentPage;

                    if (needsRebuild)
                    {
                        _pSamples.Controls.Clear();

                        _pageCount = (int)Math.Ceiling(_filteredSamples.Count / 6f);

                        _currentPage = Math.Min(_currentPage, _pageCount);
                        if (_currentPage < 1 && _pageCount >= 1)
                            _currentPage = 1;

                        _lCurrentPage.Text = _currentPage.ToString();
                        _lMaxPage.Text = _pageCount.ToString();

                        if (_filteredSamples.Count > 0)
                        {
                            for (int i = 6 * (_currentPage - 1); i < _currentPage * 6 && i < _filteredSamples.Count; ++i)
                            {
                                addSampleDataVisual(_filteredSamples[i]);
                            }
                        }
                    }

                    _alreadyRunInvoke = true;
                };

            if (this.InvokeRequired)
                this.Invoke(body);
            else
            {
                //Just making sure, that if we are running this code not on the idle event, it actually runs.
                _alreadyRunInvoke = false;
                body();
                _alreadyRunInvoke = false;
            }
        }

        private void parseXMl()
        {
            XmlDocument sampleDescription = new XmlDocument();
            try
            {
                sampleDescription.Load(SamplePath + "SampleCatalog.xml");
            }
            catch (FileNotFoundException)
            {
                OpenFileDialog openSampleXML = new OpenFileDialog();
                openSampleXML.Filter = "xml files (*.xml)|*.xml";
                openSampleXML.Title = "Pick SampleCatalog.xml";
                if (openSampleXML.ShowDialog() == DialogResult.OK)
                {
                    SamplePath = Path.GetDirectoryName(openSampleXML.FileName);
                    sampleDescription.Load(openSampleXML.FileName);
                }
                else
                {
                    MessageBox.Show("Samples.xml not found");
                    this.Close();
                    return;
                }

            }
            XmlNode sampleRoot = sampleDescription.ChildNodes[1];

            List<String> _categorieAtlas = new List<string>();

            List<String> _tagAtlas = new List<string>();

            foreach (XmlNode sample in sampleRoot.ChildNodes)
            {

                SampleData data = new SampleData()
                {
                    Name = sample["Name"].InnerText,
                    Description = sample["Description"].InnerText,
                    ExecPath = sample["ExecPath"].InnerText,
                    ProjectPath = sample["ProjectPath"].InnerText,
                    Category = sample["Category"].InnerText,
                };
                XmlNode imagePath = sample["ImagePath"];
                if (imagePath != null)
                    data.ImagePath = imagePath.InnerText;
                else
                    data.ImagePath = "";
                data.Validate();
                if (!_categorieAtlas.Contains(sample["Category"].InnerText))
                    _categorieAtlas.Add(sample["Category"].InnerText);
                XmlNode tags = sample["Tags"];
                List<String> dataTags = new List<string>();
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

            _dCategories.Items.AddRange(_categorieAtlas.Select((categoryName) => new DropDownElement(categoryName, categoryName)).ToArray());

            _cFilter.Items.AddRange(_tagAtlas.ToArray());
        }

        private void addAll()
        {
            lock (_filteredSamplesLock)
            {
                _filteredSamples.Clear();
                _filteredSamples.AddRange(_sampleAtlas);
                buildPages();
            }
        }

        private void addAll(List<SampleData> value)
        {
            lock (_filteredSamplesLock)
            {
                _filteredSamples.Clear();
                _filteredSamples.AddRange(value);
                buildPages();
            }
        }



        private bool isAllowed(SampleData data)
        {
            if (_selectedCategory != _dontFilterAll)
            {
                if (data.Category != _selectedCategory.Name)
                    return false;
            }

            if (data.Tags.Length == 0)
                return true;

            foreach (String tag in data.Tags)
            {
                if (_cFilter.CheckedItems.Contains(tag))
                    return true;
            }
            return false;
        }

        private void addSampleDataVisual(SampleData data)
        {
            SampleDataHalfVisual dataV;
            if (!_sampleDataControlMap.TryGetValue(data, out dataV))
            {
                dataV = new SampleDataHalfVisual(data, this);
                _sampleDataControlMap.Add(data, dataV);
            }
            
            if (this._pSamples.Controls.Count % 2 == 0)
                dataV.Location = new Point(0, (this._pSamples.Controls.Count / 2) * (dataV.Size.Height + 5));
            else
                dataV.Location = new Point(dataV.Width + 5, ((this._pSamples.Controls.Count - 1) / 2) * (dataV.Size.Height + 5));
            this._pSamples.Controls.Add(dataV);
        }


        private Task search()
        {
            if (_searchCancellationTokenSource != null)
            {
                _searchCancellationTokenSource.Cancel();
            }

            _searchCancellationTokenSource = new CancellationTokenSource();
            return Task.Factory.StartNew(() => search(_searchCancellationTokenSource.Token), _searchCancellationTokenSource.Token);
        }


        private void search(CancellationToken cancellationToken)
        {
            if (!this.IsHandleCreated)
                return;

            lock (_filteredSamplesLock)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                _filteredSamples.Clear();
                //Search by any of the specified words and remove empty entries to make sure that we don't just return everything if we end with one of the split characters.
                String[] phrases = _tSearch.Text.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                //If we have nothing or just the split characters, we return everything.
                if (phrases.Length == 0)
                    phrases = new string[] { "" };

                foreach (SampleData data in _sampleAtlas)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    if (!isAllowed(data))
                        continue;

                    bool add = false;
                    foreach (string phrase in phrases)
                    {
                        if (data.Name.Contains(phrase, StringComparison.OrdinalIgnoreCase) ||
                            data.Description.Contains(phrase, StringComparison.OrdinalIgnoreCase) ||
                            data.Category.Contains(phrase, StringComparison.OrdinalIgnoreCase))
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

                buildPages();
            }
        }



        private void _bSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void _tSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                search();
            }
        }

        private void _tSearch_TextChanged(object sender, EventArgs e)
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
            //Make sure our List has been initialized.
            if (_filteredSamples == null)
                return;

            lock (_filteredSamplesLock)
            {
                _filteredSamples.Comparer = SamplesComparer;

                buildPages();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            _currentPage = _currentPage + 1 > _pageCount ? _currentPage : _currentPage + 1;
            buildPages();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _currentPage = _currentPage - 1 <= 0 ? _currentPage : _currentPage - 1;
            buildPages();
        }

        private void _dCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedCategory = (DropDownElement)_dCategories.SelectedItem;
            search();
        }
    }
}
