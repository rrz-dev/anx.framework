using System;
using System.Windows.Forms;
using System.IO;

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
