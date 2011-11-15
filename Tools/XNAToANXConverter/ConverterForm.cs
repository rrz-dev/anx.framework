using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

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
	public partial class ConverterForm : Form
	{
		#region Constructor
		public ConverterForm()
		{
			InitializeComponent();

			destPath.Enabled = false;
			browsePath2.Enabled = false;

			listBox1.SelectedIndex = 0;
		}
		#endregion

		#region OnPaintBackground
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			e.Graphics.FillRectangle(new LinearGradientBrush(
				Point.Empty, new Point(0, ClientSize.Height),
				Color.CornflowerBlue, Color.White),
				ClientRectangle);
		}
		#endregion

		#region checkBox1_CheckedChanged
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			destPath.Enabled = checkBox1.Checked;
			browsePath2.Enabled = checkBox1.Checked;
		}
		#endregion

		#region browsePath1_Click
		private void browsePath1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Title = "Select a .csproj file to convert...";
				dialog.InitialDirectory = "C:\\";
				dialog.Filter = "csproj file|*.csproj";
				dialog.CheckFileExists = true;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					sourcePath.Text = dialog.FileName;
				}
			}
		}
		#endregion

		#region browsePath2_Click
		private void browsePath2_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				dialog.Title = "Select where to save the converted files...";
				dialog.InitialDirectory = "C:\\";
				dialog.Filter = "csproj file|*.csproj";
				dialog.CheckFileExists = false;
				dialog.CheckPathExists = false;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					destPath.Text = dialog.FileName;
				}
			}
		}
		#endregion

		#region convertButton_Click
		private void convertButton_Click(object sender, EventArgs e)
		{
			string source = sourcePath.Text;
			string dest = checkBox1.Checked ? destPath.Text : sourcePath.Text;

			if (String.IsNullOrEmpty(source))
			{
				MessageBox.Show("Failed to convert because you need to enter a " +
					"source filepath!");
				return;
			}

			if (File.Exists(source) == false)
			{
				MessageBox.Show("Failed to convert because the source project file " +
					"doesn't exist!");
				return;
			}

			if (String.IsNullOrEmpty(dest))
			{
				MessageBox.Show("Failed to convert because you need to enter a " +
					"destination filepath!");
				return;
			}

			string target = listBox1.SelectedIndex == 0 ?
				"anx" : "xna";

			ProjectData.Convert(target, source, dest);
			MessageBox.Show("Finished conversion!", "Conversion");
		}
		#endregion
	}
}
