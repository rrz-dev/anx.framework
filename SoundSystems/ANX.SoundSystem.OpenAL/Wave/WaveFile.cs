using System;
using System.IO;
using ANX.Framework.NonXNA;
using OpenTK.Audio;

// This file is part of the ANX.Framework and originally taken from
// the AC.AL OpenAL library, released under the MIT License.
// For details see: http://acal.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
	/// <summary>
	/// This class contains all the loading process of a wave file.
	/// <para />
	/// http://www-mmsp.ece.mcgill.ca/documents/audioformats/wave/wave.html
	/// <para />
	/// Chunk information: http://www.sonicspot.com/guide/wavefiles.html
	/// <para />
	/// Audio Codecs: http://wiki.multimedia.cx/index.php?title=Category:Audio_Codecs
	/// <para />
	/// http://netghost.narod.ru/gff/vendspec/micriff/ms_riff.txt
	/// <para />
	/// Most interesting file about many formats:
	/// http://icculus.org/SDL_sound/downloads/external_documentation/wavecomp.htm
	/// <para />
	/// http://sharkysoft.com/archive/lava/docs/javadocs/lava/riff/wave/doc-files/riffwave-content.htm
	/// </summary>
	public static class WaveFile
	{
		#region LoadData
		/// <summary>
		/// Load all information from a wave file.
		/// </summary>
		/// <param name="stream">The stream containing the wave file data.</param>
		public static WaveInfo LoadData(Stream stream)
		{
			WaveInfo result = new WaveInfo();

			using (BinaryReader reader = new BinaryReader(stream))
			{
				if (CheckHeader(reader) == false)
				{
					throw new FormatException("The provided stream is not a valid WAVE file. Unable to load!");
				}

				#region Read Chunks
				while (stream.Position < stream.Length - 8)
				{
					string identifier = new string(reader.ReadChars(4));
					int chunkLength = reader.ReadInt32();

					if (stream.Position + chunkLength > stream.Length)
					{
						break;
					}

					int startPosition = (int)reader.BaseStream.Position;

					switch (identifier.ToLower())
					{
						case "fmt ":
							{
								#region Load fmt chunk
								result.WaveFormat = (WaveFormat)reader.ReadInt16();
								result.Channels = reader.ReadInt16();
								result.SampleRate = reader.ReadInt32();
								int avgBytesPerSec = reader.ReadInt32();
								result.BlockAlign = reader.ReadInt16();
								result.BitsPerSample = reader.ReadInt16();

								if (chunkLength > 16)
								{
									short extensionSize = reader.ReadInt16();

									if (chunkLength > 18)
									{
										result.ExtSamplesPerBlock = reader.ReadInt16();
										int speakerPositionMask = reader.ReadInt32();
										WaveFormat extFormat = (WaveFormat)reader.ReadInt16();
										if (result.WaveFormat < 0)
										{
											result.WaveFormat = extFormat;
										}
										byte[] subFormat = reader.ReadBytes(14);
									}
								}

								result.OpenALFormat = (result.Channels == 1 ?
									(result.BitsPerSample == 8 ?
									ALFormat.Mono8 :
									ALFormat.Mono16) :
									(result.BitsPerSample == 8 ?
									ALFormat.Stereo8 :
									ALFormat.Stereo16));
								#endregion
							}
							break;

						case "fact":
							{
								#region Load fact chunk
								// per channel
								int numberOfSamples = reader.ReadInt32();
								// TODO: more
								#endregion
							}
							break;

						case "data":
							result.Data = reader.ReadBytes(chunkLength);
							break;
					}

					// If some chunks are incorrect in data length, we ensure that we
					// end up in the right position.
					int lengthRead = (int)reader.BaseStream.Position - startPosition;
					if (lengthRead != chunkLength)
					{
						reader.BaseStream.Seek(chunkLength - lengthRead,
							SeekOrigin.Current);
					}
				}
				#endregion
			}

			if (result.Data == null)
			{
				Logger.Error("There was no data chunk available. Unable to load!");
				return null;
			}

			ConvertFormat(result);

			return result;
		}
		#endregion

		#region CheckHeader
		private static bool CheckHeader(BinaryReader reader)
		{
			string RIFFmagic = new string(reader.ReadChars(4));
			if(RIFFmagic != "RIFF")
			{
				return false;
			}

			// filesize
			reader.ReadInt32();

			string identifierWAVE = new string(reader.ReadChars(4));
			if(identifierWAVE != "WAVE")
			{
				return false;
			}

			return true;
		}
		#endregion

		#region ConvertFormat
		private static void ConvertFormat(WaveInfo info)
		{
			switch (info.WaveFormat)
			{
				case WaveFormat.PCM:
					#region Convert 32 to 16 bps (TODO)
					//if (info.BitsPerSample == 32)
					//{
					//  BinaryReader sourceReader =
					//    new BinaryReader(new MemoryStream(info.Data));
					//  MemoryStream destStream = new MemoryStream();
					//  BinaryWriter destWriter = new BinaryWriter(destStream);

					//  int length = info.Data.Length / 4;
					//  for (int index = 0; index < length; index++)
					//  {
					//    int value = sourceReader.ReadInt32();
					//    destWriter.Write((short)(value / 2));
					//  }
					//  sourceReader.Close();
					//  destWriter.Close();
					//  info.Data = destStream.ToArray();
					//  destStream.Dispose();
					//}
					#endregion
					break;

				case WaveFormat.ALAW:
					ALaw.ConvertToPcm(info);
					break;

				case WaveFormat.MULAW:
					MuLaw.ConvertToPcm(info);
					break;

				case WaveFormat.IEEE_FLOAT:
					{
						#region Convert float to pcm
						bool is64BitFloat = info.BitsPerSample == 64;

						BinaryReader sourceReader =
							new BinaryReader(new MemoryStream(info.Data));
						MemoryStream destStream = new MemoryStream();
						BinaryWriter destWriter = new BinaryWriter(destStream);

						int length = info.Data.Length / (is64BitFloat ? 8 : 4);
						for (int index = 0; index < length; index++)
						{
							double value = is64BitFloat ?
								sourceReader.ReadDouble() :
								sourceReader.ReadSingle();

							destWriter.Write((short)(value * 32767));
						}

						sourceReader.Close();
						destWriter.Close();
						info.Data = destStream.ToArray();
						destStream.Dispose();
						#endregion
					}
					break;

				case WaveFormat.MS_ADPCM:
					MsAdpcm.ConvertToPcm(info);
					break;

				default:
					throw new NotSupportedException("The WAVE format " +
						info.WaveFormat + " is not supported yet. Unable to load!");
			}
		}
		#endregion
	}
}
