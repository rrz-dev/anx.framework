using System;
using System.Windows.Forms;
using System.IO;
using ProjectConverter.Platforms;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Converter converter = new LinuxConverter();
			converter.ConvertAllProjects("../../ANX.Framework.sln");

			converter = new PsVitaConverter();
			converter.ConvertAllProjects("../../ANX.Framework.sln");

			converter = new MetroConverter();
			converter.ConvertAllProjects("../../ANX.Framework.sln");
		}
	}
}
