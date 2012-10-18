using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ANX.Framework.Content;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Graphics;
using Timer = System.Windows.Forms.Timer;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    public partial class PreviewScreen : Form
    {
        #region Fields
        private Point _lastPos;
        private bool _mouseDown;
        private readonly BuildItem _item;
        private Thread _loaderThread;
        private readonly string _outputFile;
        private readonly string _outputDir;
        private volatile bool _started;
        private volatile bool _error;
        private volatile string _errorMessage;
        private string _processor;
        private Timer _checkTimer;
        private Timer _tickTimer;
        private readonly GraphicsDevice _graphicsDevice;
        private SpriteBatch _batch;
        #endregion

        public PreviewScreen(BuildItem item)
        {
            InitializeComponent();
            _item = item;
            _outputFile = Path.GetTempFileName();
            _outputDir = Path.GetTempPath();
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

        private void ButtonQuitClick(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }

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

        private void PreviewScreenLoad(object sender, EventArgs e)
        {
            _checkTimer = new Timer {Interval = 1000};
            _checkTimer.Tick += CheckThread;
            _checkTimer.Start();
        }

        void CheckThread(object sender, EventArgs e)
        {
            if (!_started)
            {
                ((Timer) sender).Interval = 100;
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
                _tickTimer = new Timer {Interval = 120};
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
    }
}
