using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio.XactParser
{
	internal class XactGeneralSettingsRpcCurvePoint
	{
		public enum CurveType
		{
			Linear = 0x00,
			Fast = 0x01,
			Slow = 0x02,
			SinCos = 0x03,
		}

		public float X
		{
			get;
			private set;
		}

		public float Y
		{
			get;
			private set;
		}

		public CurveType Type
		{
			get;
			private set;
		}

		public XactGeneralSettingsRpcCurvePoint(BinaryReader reader)
		{
			X = reader.ReadSingle();
			Y = reader.ReadSingle();
			Type = (XactGeneralSettingsRpcCurvePoint.CurveType)reader.ReadByte();
		}
	}
}
