using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio.XactParser
{
	internal class XactGeneralSettingsVariable
	{
		public enum VariableFlags
		{
			Public = 0x01,
			ReadOnly = 0x02,
			CueInstance = 0x04,
			Reserved = 0x08,
		}

		public VariableFlags Flags
		{
			get;
			private set;
		}

		public float StartingValue
		{
			get;
			set;
		}

		public float MinValue
		{
			get;
			private set;
		}

		public float MaxValue
		{
			get;
			private set;
		}

		public string Name;

		public XactGeneralSettingsVariable(BinaryReader reader)
		{
			Flags = (XactGeneralSettingsVariable.VariableFlags)reader.ReadByte();
			StartingValue = reader.ReadSingle();
			MinValue = reader.ReadSingle();
			MaxValue = reader.ReadSingle();
		}
	}
}
