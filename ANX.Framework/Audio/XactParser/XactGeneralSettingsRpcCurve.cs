using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio.XactParser
{
	internal class XactGeneralSettingsRpcCurve
	{
		// what variable this curve involves
	    public ushort VariableIndex { get; private set; }

	    // which parameter the curve affects refer to the above constants
	    public short Parameters { get; private set; }

	    public XactGeneralSettingsRpcCurvePoint[] Points { get; private set; }

	    public XactGeneralSettingsRpcCurve(BinaryReader reader)
		{
			VariableIndex = reader.ReadUInt16();
			byte numberOfCurvePoints = reader.ReadByte();
			Parameters = reader.ReadInt16();

			Points = new XactGeneralSettingsRpcCurvePoint[numberOfCurvePoints];

			for (int pointIndex = 0; pointIndex < numberOfCurvePoints; pointIndex++)
				Points[pointIndex] = new XactGeneralSettingsRpcCurvePoint(reader);
		}
	}
}
