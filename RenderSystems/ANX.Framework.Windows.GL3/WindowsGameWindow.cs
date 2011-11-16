﻿using System;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Platform.Windows;

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

namespace ANX.Framework.Windows.GL3
{
	internal class WindowsGameWindow : ANX.Framework.GameWindow
	{
		#region Public
		internal static Form Form
		{
			get;
			private set;
		}

		public override IntPtr Handle
		{
			get
			{
				return Form.Handle;
			}
		}

		public override bool IsMinimized
		{
			get
			{
				return Form.WindowState == FormWindowState.Minimized;
			}
		}

		#region AllowUserResizing
		public override bool AllowUserResizing
		{
			get
			{
				return Form.FormBorderStyle == FormBorderStyle.Sizable;
			}
			set
			{
				Form.FormBorderStyle = value ?
					FormBorderStyle.Sizable :
					FormBorderStyle.Fixed3D;
			}
		}
		#endregion

		#region ClientBounds
		public override Rectangle ClientBounds
		{
			get
			{
				System.Drawing.Rectangle rect = Form.ClientRectangle;
				return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
			}
		}
		#endregion

		#region CurrentOrientation
		public override DisplayOrientation CurrentOrientation
		{
			get
			{
				return DisplayOrientation.Default;
			}
		}
		#endregion
		#endregion

		#region Constructor
		internal WindowsGameWindow()
		{
			Form = new Form()
			{
				Text = "ANX Framework",
				MaximizeBox = false,
				FormBorderStyle = FormBorderStyle.Fixed3D,
				ClientSize = new System.Drawing.Size(800, 600),
			};
		}
		#endregion

		#region Close
		public void Close()
		{
			if (Form != null)
			{
				Form.Close();
				Form.Dispose();
				Form = null;
			}
		}
		#endregion

		#region SetTitle
		protected override void SetTitle(string title)
		{
			Form.Text = title;
		}
		#endregion

		public override void BeginScreenDeviceChange(bool willBeFullScreen)
		{
			throw new NotImplementedException();
		}

		public override void EndScreenDeviceChange(string screenDeviceName,
			int clientWidth, int clientHeight)
		{
			throw new NotImplementedException();
		}

		public override string ScreenDeviceName
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}