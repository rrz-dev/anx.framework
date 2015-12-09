#region Using Statements
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ANX.Framework;
using ANX.Framework.Content;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;
using Point = System.Drawing.Point;
using Timer = System.Windows.Forms.Timer;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI.Dialogues
{
    [PercentageComplete(60)]
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
        private ContentManager _contentManager;
        private GameServiceContainer _services;
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
                    DeviceWindowHandle = new Framework.NonXNA.WindowHandle(drawSurface.Handle),
                    PresentationInterval = PresentInterval.Default,
                });
            _services = new GameServiceContainer();
            _services.AddService(typeof(GraphicsDevice), _graphicsDevice);
            _contentManager = new ContentManager(_services);

            _checkTimer = new Timer { Interval = 1000 };
            _checkTimer.Tick += CheckThread;

            _tickTimer = new Timer { Interval = 120 };
            _tickTimer.Tick += Tick;
            _batch = new SpriteBatch(_graphicsDevice);
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
            Hide();
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
                _tickTimer.Start();
            }
        }

        void Tick(object sender, EventArgs e)
        {
            try
            {
                if (!_outputFile.EndsWith(".xnb") && !File.Exists(_outputFile + ".xnb"))
                {
                    File.Move(_outputFile, _outputFile + ".xnb");
                }
                _graphicsDevice.Clear(Framework.Color.CornflowerBlue);
                if (_processor == "TextureProcessor")
                {
                    _batch.Begin();
                    try
                    {
                        var tex = _contentManager.Load<Texture2D>(_outputFile);
                        _batch.Draw(tex, Vector2.Zero, Color.White);
                    }
                    finally
                    {
                        _batch.End();
                    }
                }
                else if (_processor == "FontDescriptionProcessor")
                {
                    _batch.Begin();
                    try
                    {
                        var font = _contentManager.Load<SpriteFont>(_outputFile);
                        _batch.DrawString(font, "The quick brown fox jumps over the lazy dog. äöü@", Vector2.One, Color.DarkRed);
                    }
                    finally
                    {
                        _batch.End();
                    }
                }
                _graphicsDevice.Present();
            }
            catch (Exception exc)
            {
                _error = true;
                _errorMessage = exc.Message;
            }
        }
        #endregion

        #region Public methods
        public void CompileFile()
        {
            //TODO: use ContentBuilder instead

            var builderTask = new BuildContentTask
            {
                TargetPlatform = TargetPlatform.Windows,
                TargetProfile = GraphicsProfile.HiDef,
                CompressContent = false,
                BaseDirectory = new Uri(MainWindow.Instance.ProjectFolder, _item.AssetName)
            };

            builderTask.PrepareAssetBuildCallback = (BuildContentTask sender, BuildItem item, out ContentImporterContext importerContext, out ContentProcessorContext processorContext) =>
            {
                importerContext = new DefaultContentImporterContext(new CCompilerBuildLogger(), _outputDir, _outputDir);
                processorContext = new DefaultContentProcessorContext(sender, MainWindow.Instance.ActiveConfiguration.Name, _outputDir, _outputDir, _outputFile);
            };

            var importerManager = new ImporterManager();

            if (String.IsNullOrEmpty(_item.ImporterName))
            {
                _item.ImporterName = importerManager.GuessImporterByFileExtension(_item.SourceFilename);
            }
            if (String.IsNullOrEmpty(_item.ProcessorName))
            {
                _item.ProcessorName =
                    new ProcessorManager().GetProcessorForImporter(importerManager.GetInstance(_item.ImporterName));
            }
            _processor = _item.ProcessorName;
            
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
            _tickTimer.Stop();
            _checkTimer.Stop();
            _started = false;

            if (item != null)
            {
                _item = item;
                _outputFile = Path.GetTempFileName();
                _outputDir = Path.GetTempPath();
                _checkTimer.Start();

                labelStatus.Text = "Loading Preview...";
            }
            else
            {
                labelStatus.Text = "No item selected";
            }

            labelStatus.Show();
        }
        #endregion
    }
}
