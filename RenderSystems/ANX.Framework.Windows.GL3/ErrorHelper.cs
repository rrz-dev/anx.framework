using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Reflection;

namespace ANX.Framework.Windows.GL3
{
	internal static class ErrorHelper
	{
		public static void Check(string extraInformation = "")
		{
			ErrorCode code = GL.GetError();
			if (code != ErrorCode.NoError)
			{
				string frameInfo = "";
				foreach (StackFrame frame in new StackTrace().GetFrames())
				{
					MethodBase method = frame.GetMethod();
					frameInfo += "\n\t" + "at " + method.DeclaringType + "." +
						method + ":" + frame.GetFileLineNumber();
				}

				string message = "OpenGL Error '" + code + "' Checked at: '" +
					extraInformation + "'" + frameInfo;

				Console.WriteLine(message);
				Debug.WriteLine(message);
			}
		}
	}
}
