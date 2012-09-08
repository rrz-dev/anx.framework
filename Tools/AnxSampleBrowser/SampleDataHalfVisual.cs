using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AnxSampleBrowser
{
    public partial class SampleDataHalfVisual : UserControl
    {
        private SampleData _source;
        private AnxSampleBrowser _parent;

        public SampleDataHalfVisual(SampleData source, AnxSampleBrowser parent)
        {
         
            InitializeComponent();


            //until error is gone
           // _pImage.Visible = false;

            _rDescription.ReadOnly = true;
            _parent = parent;
            _source = source;
           
            this._lName.Text = _source.Name;
            this._rDescription.Text = _source.Description;
            if (source.ImagePath.Length > 0)
            {
                Bitmap b = new Bitmap(source.ImagePath);
                _pImage.BackgroundImage = b;
         
            }
        }

        private void _bLaunch_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_parent.SamplePath + _source.ExecPath);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show("Can´t find the specified file at " + _parent.SamplePath + _source.ExecPath + '\n' + '\n' + '\n' + ex.Message,"Sample file not found");
            }

        }



   
    }
}
