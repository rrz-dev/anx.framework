using System;
using System.IO;
using ANX.Framework.Content.Pipeline.Audio;
using WaveUtils;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Importer
{
	[ContentImporter(new string[] { ".wav" }, DefaultProcessor = "SoundEffectProcessor")]
	public class WavImporter : ContentImporter<AudioContent>
	{
		public override AudioContent Import(string filename, ContentImporterContext context)
		{
			WaveInfo loadedData;
			using (Stream filestream = File.OpenRead(filename))
				loadedData = WaveFile.LoadData(filestream);

			return new AudioContent(loadedData)
			{
				FileName = filename,
				Identity = new ContentIdentity(filename, null, null),
				FileType = AudioFileType.Wav,
			};
		}

	}
}
