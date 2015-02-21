using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reflection;

namespace AnxSampleBrowser
{
    public partial class SampleDataHalfVisual : UserControl
    {
        private SampleData _source;
        private AnxSampleBrowser _parent;
        private StringBuilder _processOutput;

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

            _bOpen.Enabled = File.Exists(Path.GetFullPath(Path.Combine(_parent.SamplePath, _source.ProjectPath)));
            _bLaunch.Enabled = File.Exists(Path.GetFullPath(Path.Combine(_parent.SamplePath, _source.ExecPath)));
        }

        public SampleData SampleData
        {
            get { return _source; }
        }

        private void _bLaunch_Click(object sender, EventArgs e)
        {
            LaunchExternal();
        }

        private void _bOpen_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetFullPath(_parent.SamplePath + _source.ProjectPath));
        }

        private void LaunchExternal()
        {
            IntPtr handle = this.FindForm().Handle;

            _processOutput = new StringBuilder();
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(Path.GetFullPath(_parent.SamplePath + _source.ExecPath));
                startInfo.WorkingDirectory = Path.GetDirectoryName(Path.GetFullPath(_parent.SamplePath + _source.ExecPath));
                startInfo.ErrorDialog = true;
                startInfo.ErrorDialogParentHandle = handle;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;

                process.StartInfo = startInfo;

                process.EnableRaisingEvents = true;
                process.Exited += process_Exited;

                process.ErrorDataReceived += process_ErrorDataReceived;

                process.Start();

                process.BeginErrorReadLine();
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show("Can´t find the specified file at " + _parent.SamplePath + _source.ExecPath + '\n' + '\n' + '\n' + ex.Message, "Sample file not found");
            }
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            //Samples only output error data.
            _processOutput.Append(e.Data);
        }

        void process_Exited(object sender, EventArgs e)
        {
            Process process = (Process)sender;
            if (process.ExitCode != 0)
                MessageBox.Show("Process " + Path.GetFileNameWithoutExtension(process.StartInfo.FileName) + " exited with code " + process.ExitCode + " and message: " + _processOutput.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
