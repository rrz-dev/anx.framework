using System;
using System.Windows.Forms;
using System.IO;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace XNAToANXConverter
{
	static class Program
	{
		/// <summary>
		/// Starting the tool via cmd:
		/// 
		/// Converting into the same file:
		/// "XNAToANXConverter.exe anx C:\code\testproject.csproj"
		/// 
		/// Converting into different file:
		/// "XNAToANXConverter.exe anx C:\code\testproject.csproj C:\converted\test.csproj"
		/// 
		/// Converting ANX to XNA:
		/// "XNAToANXConverter.exe xna C:\code\testproject.csproj"
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				#region Cmd starting
				string target = args[0].ToLower();
				if (target != "anx" &&
					target != "xna")
				{
					Console.WriteLine("Unknown target '" + target +
						"'. Valid targets are 'xna' or 'anx'.");
					return;
				}
				string sourcePath = args[1];
				if (File.Exists(sourcePath) == false)
				{
					Console.WriteLine("Failed to convert. The sourcefile doesn't exist!");
					return;
				}
				string destPath = args.Length > 2 ? args[2] : sourcePath;

				Console.WriteLine("Starting conversion...");
				ProjectData.Convert(target, sourcePath, destPath);
				Console.WriteLine("Conversion complete!");
				#endregion
			}
			else
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new ConverterForm());
			}
		}
	}
}
