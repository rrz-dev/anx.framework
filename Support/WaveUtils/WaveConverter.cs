using System;
using System.IO;

// This file is part of the ANX.Framework and originally taken from
// the AC.AL OpenAL library, released under the MIT License.
// For details see: http://acal.codeplex.com/license

namespace WaveUtils
{
	public class WaveConverter
	{
		private WaveInfo loadedData;

		public WaveConverter(WaveInfo loadedData)
		{
			this.loadedData = loadedData;
		}

		public void ConvertToPcm()
		{
			ConvertToPcm(loadedData.Channels);
		}

		public void ConvertToPcm(int resultChannelCount)
		{
			switch (loadedData.WaveFormat)
			{
				case WaveFormat.ALAW:
					ALaw.ConvertToPcm(loadedData, resultChannelCount);
					break;

				case WaveFormat.MULAW:
					MuLaw.ConvertToPcm(loadedData, resultChannelCount);
					break;

				case WaveFormat.IEEE_FLOAT:
					IEEEFloat.ConvertToPcm(loadedData, resultChannelCount);
					break;

				case WaveFormat.MS_ADPCM:
					MsAdpcm.ConvertToPcm(loadedData);
					break;

                case WaveFormat.PCM:
                    //If it's already PCM, don't convert anything.
                    break;

				default:
					throw new NotSupportedException("The WAVE format " + loadedData.WaveFormat +
						" is not supported yet. Unable to load!");
			}
		}
	}
}
