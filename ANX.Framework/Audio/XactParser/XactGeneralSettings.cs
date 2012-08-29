using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio.XactParser
{
	/// <summary>
	/// http://code.google.com/p/monoxna/source/browse/wiki/XnaFrameworkAudio.wiki?r=347
	/// </summary>
	internal class XactGeneralSettings
	{
		public class InvalidMagicException : Exception { }
		public class InvalidVersionException : Exception { }

		#region Public
		public AudioCategory[] Categories
		{
			get;
			private set;
		}

		public XactGeneralSettingsVariable[] Variables
		{
			get;
			private set;
		}

		public XactGeneralSettingsRpcCurve[] Curves
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		public XactGeneralSettings(Stream stream)
		{
			BinaryReader reader = new BinaryReader(stream);
			ValidateMagic(reader);
			ValidateToolVersion(reader);
			ValidateFormatVersion(reader);

			// unknown, maybe something to do with the last modified values
			reader.ReadUInt16();

			DateTime lastModifiedDate = DateTime.FromFileTime(reader.ReadInt64());

			// seems to stay 0x03, probably platform
			reader.ReadByte();

			Categories = new AudioCategory[reader.ReadUInt16()];
			Variables = new XactGeneralSettingsVariable[reader.ReadUInt16()];

			// unknown, seems to always be 0x16
			reader.ReadUInt16();
			// unknown, seems to always be 0x16
			reader.ReadUInt16();

			Curves = new XactGeneralSettingsRpcCurve[reader.ReadUInt16()];
			ushort dspEffectPresetsCount = reader.ReadUInt16();
			ushort dspEffectParametersCount = reader.ReadUInt16();

			int firstCategorySettingsPosition = reader.ReadInt32();
			int firstVariableSettingsPosition = reader.ReadInt32();

			// unknown
			stream.Seek(16, SeekOrigin.Current);

			int positionOfFirstCategoryName = reader.ReadInt32();
			int positionOfFirstVariableName = reader.ReadInt32();
			int positionOfFirstRpcCurve = reader.ReadInt32();
			int positionOfFirstDspEffectPreset = reader.ReadInt32();
			int positionOfFirstDspEffectParameters = reader.ReadInt32();

			stream.Seek(firstCategorySettingsPosition, SeekOrigin.Begin);
			for (int categoryIndex = 0; categoryIndex < Categories.Length; categoryIndex++)
				Categories[categoryIndex] = new AudioCategory(reader);

			stream.Seek(firstVariableSettingsPosition, SeekOrigin.Begin);
			for (int variableIndex = 0; variableIndex < Variables.Length; variableIndex++)
				Variables[variableIndex] = new XactGeneralSettingsVariable(reader);

			if (positionOfFirstRpcCurve > -1)
			{
				stream.Seek(positionOfFirstRpcCurve, SeekOrigin.Begin);
				for (int curveIndex = 0; curveIndex < Curves.Length; curveIndex++)
					Curves[curveIndex] = new XactGeneralSettingsRpcCurve(reader);
			}

			stream.Seek(positionOfFirstCategoryName, SeekOrigin.Begin);
			string[] names = ParseNames(Categories.Length, reader);
			for (int categoryIndex = 0; categoryIndex < Categories.Length; categoryIndex++)
				Categories[categoryIndex].Name = names[categoryIndex];

			stream.Seek(positionOfFirstVariableName, SeekOrigin.Begin);
			names = ParseNames(Variables.Length, reader);
			for (int variableIndex = 0; variableIndex < Variables.Length; variableIndex++)
				Variables[variableIndex].Name = names[variableIndex];
		}
		#endregion

		#region ValidateMagic
		private static void ValidateMagic(BinaryReader reader)
		{
			char[] magicChars = reader.ReadChars(4);
			if (magicChars[0] != 'X' || magicChars[1] != 'G' || magicChars[2] != 'S' || magicChars[3] != 'F')
				throw new InvalidMagicException();
		}
		#endregion

		#region ValidateToolVersion
		private static void ValidateToolVersion(BinaryReader reader)
		{
			ushort version = reader.ReadUInt16();
			if (version != 47 && version != 46 && version != 45)
				throw new InvalidVersionException();
		}
		#endregion

		#region ValidateFormatVersion
		private static void ValidateFormatVersion(BinaryReader reader)
		{
			ushort version = reader.ReadUInt16();
			if (version != 42)
				throw new InvalidVersionException();
		}
		#endregion

		#region ParseNames
		private static string[] ParseNames(int count, BinaryReader reader)
		{
			string[] result = new string[count];
			for (int index = 0; index < count; index++)
			{
				result[index] = "";
				char readChar = '\0';
				while ((readChar = (char)reader.ReadByte()) != '\0')
					result[index] += readChar;
			}

			return result;
		}
		#endregion
	}
}
