#if WINDOWSMETRO

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace ANX.Framework.NonXNA.Windows8
{
    internal class MetroLogger : EventListener
    {
        /// <summary>
        /// Storage file to be used to write logs
        /// </summary>
        private StorageFile _storageFile = null;

        /// <summary>
        /// The format to be used by logging.
        /// </summary>
        private readonly string _format = "{0:yyyy-MM-dd HH\\:mm\\:ss\\:ffff}\tType: {1}\tId: {2}\tMessage: '{3}'";

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public async void Initialize(string fileName)
        {
            _storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName.Replace(" ", "_"), CreationCollisionOption.OpenIfExists);

            this.EnableEvents(ANX.Framework.NonXNA.Windows8.MetroEventSource.Log, System.Diagnostics.Tracing.EventLevel.Informational);
        }

        private async void WriteToFile(IEnumerable<string> lines)
        {
            await _semaphore.WaitAsync();

            Task.Run(async () =>
                {
                    try
                    {
                        await FileIO.AppendLinesAsync(_storageFile, lines);
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine(exc);
                    }
                });
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (_storageFile == null) 
                return;

            var lines = new List<string>();

            var newFormatedLine = string.Format(_format, DateTime.Now, eventData.Level, eventData.EventId, eventData.Payload[0]);

            Debug.WriteLine(newFormatedLine);

            lines.Add(newFormatedLine);

            WriteToFile(lines);
        }
    }
}

#endif
