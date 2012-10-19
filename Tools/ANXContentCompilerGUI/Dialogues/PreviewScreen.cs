#region Using Statements
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;
using Timer = System.Windows.Forms.Timer;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI.Dialogues
{
    [PercentageComplete(50)]
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public partial class PreviewScreen : Form
    {
        #region Fields
        private Point _lastPos;
        private bool _mouseDown;
        private BuildItem _item;
        private Thread _loaderThread;
        private string _outputFile;
        private string _outputDir;
        private volatile bool _started;
        private volatile bool _error;
        private volatile string _errorMessage;
        private string _processor;
        private Timer _checkTimer;
        private Timer _tickTimer;
        private readonly GraphicsDevice _graphicsDevice;
        private SpriteBatch _batch;
        #endregion

        #region Constructor
        public PreviewScreen()
        {
            InitializeComponent();
            _graphicsDevice = new GraphicsDevice(
                GraphicsAdapter.DefaultAdapter,
                GraphicsProfile.HiDef,
                new PresentationParameters
                {
                    BackBufferWidth = drawSurface.Width,
                    BackBufferHeight = drawSurface.Height,
                    BackBufferFormat = SurfaceFormat.Color,
                    DeviceWindowHandle = drawSurface.Handle,
                    PresentationInterval = PresentInterval.Default,
                });
        }
        #endregion

        #region WindowMoveMethods

        private void LabelTitleMouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown) return;
            int xoffset = MousePosition.X - _lastPos.X;
            int yoffset = MousePosition.Y - _lastPos.Y;
            Left += xoffset;
            Top += yoffset;
            _lastPos = MousePosition;
        }

        private void LabelTitleMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastPos = MousePosition;
        }

        private void LabelTitleMouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        #endregion

        #region Events
        private void ButtonQuitClick(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }


        void CheckThread(object sender, EventArgs e)
        {
            if (!_started)
            {
                ((Timer)sender).Interval = 100;
                _loaderThread = new Thread(CompileFile);
                _loaderThread.Start();
                _started = true;
            }
            else
            {
                if (_loaderThread.IsAlive)
                    return;
                if (_error)
                {
                    labelStatus.Text = "Loading of Preview failed with: \n" + _errorMessage;
                    return;
                }
                labelStatus.Text = "Loading successful";
                labelStatus.Hide();
                _tickTimer = new Timer { Interval = 120 };
                _tickTimer.Tick += Tick;
                _batch = new SpriteBatch(_graphicsDevice);
                _tickTimer.Start();
            }
        }

        void Tick(object sender, EventArgs e)
        {
            _graphicsDevice.Clear(Framework.Color.CornflowerBlue);
            if (_processor == "TextureProcessor")
            {
                _batch.Begin();

                _batch.End();
            }
        }
        #endregion

        #region Public methods
        public void CompileFile()
        {
            var builderTask = new BuildContent
            {
                BuildLogger = new FakeBuildLogger(),
                OutputDirectory = _outputDir,
                TargetPlatform = TargetPlatform.Windows,
                TargetProfile = GraphicsProfile.HiDef,
                CompressContent = false
            };
            if (String.IsNullOrEmpty(_item.ImporterName))
            {
                _item.ImporterName = ImporterManager.GuessImporterByFileExtension(_item.SourceFilename);
            }
            if (String.IsNullOrEmpty(_item.ProcessorName))
            {
                _item.ProcessorName =
                    new ProcessorManager().GetProcessorForImporter(new ImporterManager().GetInstance(_item.ImporterName));
            }
            _processor = _item.ProcessorName;
            _item.OutputFilename = _outputFile;
            try
            {
                builderTask.Execute(new[] { _item });
            }
            catch (Exception ex)
            {
                _error = true;
                _errorMessage = ex.Message;
            }
        }

        public void SetFile(BuildItem item)
        {
            labelStatus.Text = "Loading Preview...";
            _item = item;
            _outputFile = Path.GetTempFileName();
            _outputDir = Path.GetTempPath();
            _checkTimer = new Timer { Interval = 1000 };
            _checkTimer.Tick += CheckThread;
            _checkTimer.Start();
        }
        #endregion
    }
}
