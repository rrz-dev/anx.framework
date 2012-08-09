using System;
using System.Windows.Forms;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace WindowsFormsEditor
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Form1 form = new Form1();
			form.Show();

			form.Initialize();

			while (form.Visible)
			{
				Application.DoEvents();

				form.Tick();
			}
		}
	}
}
