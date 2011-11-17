#region Using Statements
using System;
#endregion // Using Statements

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
	internal static class ShaderByteCode
	{
		#region SpriteBatchShader
		internal static byte[] SpriteBatchByteCode = new byte[]
		{
			224, 
			007, 117, 110, 105, 102, 111, 114, 109, 032, 109, 097, 116, 052, 032, 077, 097, 116, 114, 105, 120, 
			084, 114, 097, 110, 115, 102, 111, 114, 109, 059, 010, 118, 111, 105, 100, 032, 109, 097, 105, 110, 
			040, 118, 111, 105, 100, 041, 123, 010, 103, 108, 095, 080, 111, 115, 105, 116, 105, 111, 110, 061, 
			103, 108, 095, 077, 111, 100, 101, 108, 086, 105, 101, 119, 080, 114, 111, 106, 101, 099, 116, 105, 
			111, 110, 077, 097, 116, 114, 105, 120, 042, 103, 108, 095, 086, 101, 114, 116, 101, 120, 059, 125, 
			010, 035, 035, 033, 102, 114, 097, 103, 109, 101, 110, 116, 033, 035, 035, 010, 118, 111, 105, 100, 
			032, 109, 097, 105, 110, 040, 118, 111, 105, 100, 041, 123, 010, 103, 108, 095, 070, 114, 097, 103, 
			067, 111, 108, 111, 114, 061, 118, 101, 099, 052, 040, 049, 046, 048, 044, 049, 046, 048, 044, 049, 
			046, 048, 044, 049, 046, 048, 041, 059, 125, 010, 047, 042, 010, 084, 101, 120, 116, 117, 114, 101, 
			050, 068, 060, 102, 108, 111, 097, 116, 052, 062, 032, 084, 101, 120, 116, 117, 114, 101, 032, 058, 
			032, 114, 101, 103, 105, 115, 116, 101, 114, 040, 116, 048, 041, 059, 010, 115, 097, 109, 112, 108, 
			101, 114, 032, 084, 101, 120, 116, 117, 114, 101, 083, 097, 109, 112, 108, 101, 114, 032, 058, 032, 
			114, 101, 103, 105, 115, 116, 101, 114, 040, 115, 048, 041, 059, 010, 115, 116, 114, 117, 099, 116, 
			032, 086, 101, 114, 116, 101, 120, 083, 104, 097, 100, 101, 114, 073, 110, 112, 117, 116, 123, 010, 
			102, 108, 111, 097, 116, 052, 032, 112, 111, 115, 032, 058, 032, 080, 079, 083, 073, 084, 073, 079, 
			078, 059, 010, 102, 108, 111, 097, 116, 052, 032, 099, 111, 108, 032, 058, 032, 067, 079, 076, 079, 
			082, 059, 010, 102, 108, 111, 097, 116, 050, 032, 116, 101, 120, 032, 058, 032, 084, 069, 088, 067, 
			079, 079, 082, 068, 048, 059, 010, 125, 059, 010, 115, 116, 114, 117, 099, 116, 032, 080, 105, 120, 
			101, 108, 083, 104, 097, 100, 101, 114, 073, 110, 112, 117, 116, 123, 010, 102, 108, 111, 097, 116, 
			052, 032, 112, 111, 115, 032, 058, 032, 083, 086, 095, 080, 079, 083, 073, 084, 073, 079, 078, 059, 
			010, 102, 108, 111, 097, 116, 052, 032, 099, 111, 108, 032, 058, 032, 067, 079, 076, 079, 082, 059, 
			010, 102, 108, 111, 097, 116, 050, 032, 116, 101, 120, 032, 058, 032, 084, 069, 088, 067, 079, 079, 
			082, 068, 048, 059, 010, 125, 059, 010, 080, 105, 120, 101, 108, 083, 104, 097, 100, 101, 114, 073, 
			110, 112, 117, 116, 032, 083, 112, 114, 105, 116, 101, 086, 101, 114, 116, 101, 120, 083, 104, 097, 
			100, 101, 114, 040, 032, 086, 101, 114, 116, 101, 120, 083, 104, 097, 100, 101, 114, 073, 110, 112, 
			117, 116, 032, 105, 110, 112, 117, 116, 032, 041, 123, 010, 080, 105, 120, 101, 108, 083, 104, 097, 
			100, 101, 114, 073, 110, 112, 117, 116, 032, 111, 117, 116, 112, 117, 116, 061, 040, 080, 105, 120, 
			101, 108, 083, 104, 097, 100, 101, 114, 073, 110, 112, 117, 116, 041, 048, 059, 010, 111, 117, 116, 
			112, 117, 116, 046, 112, 111, 115, 061, 109, 117, 108, 040, 105, 110, 112, 117, 116, 046, 112, 111, 
			115, 044, 077, 097, 116, 114, 105, 120, 084, 114, 097, 110, 115, 102, 111, 114, 109, 041, 059, 010, 
			111, 117, 116, 112, 117, 116, 046, 099, 111, 108, 061, 105, 110, 112, 117, 116, 046, 099, 111, 108, 
			059, 010, 111, 117, 116, 112, 117, 116, 046, 116, 101, 120, 061, 105, 110, 112, 117, 116, 046, 116, 
			101, 120, 059, 010, 114, 101, 116, 117, 114, 110, 032, 111, 117, 116, 112, 117, 116, 059, 125, 010, 
			102, 108, 111, 097, 116, 052, 032, 083, 112, 114, 105, 116, 101, 080, 105, 120, 101, 108, 083, 104, 
			097, 100, 101, 114, 040, 032, 080, 105, 120, 101, 108, 083, 104, 097, 100, 101, 114, 073, 110, 112, 
			117, 116, 032, 105, 110, 112, 117, 116, 032, 041, 032, 058, 032, 083, 086, 095, 084, 097, 114, 103, 
			101, 116, 123, 010, 114, 101, 116, 117, 114, 110, 032, 084, 101, 120, 116, 117, 114, 101, 046, 083, 
			097, 109, 112, 108, 101, 040, 084, 101, 120, 116, 117, 114, 101, 083, 097, 109, 112, 108, 101, 114, 
			044, 105, 110, 112, 117, 116, 046, 116, 101, 120, 041, 042, 105, 110, 112, 117, 116, 046, 099, 111, 
			108, 059, 125, 010, 116, 101, 099, 104, 110, 105, 113, 117, 101, 049, 048, 032, 083, 112, 114, 105, 
			116, 101, 084, 101, 099, 104, 110, 105, 113, 117, 101, 123, 010, 112, 097, 115, 115, 032, 083, 112, 
			114, 105, 116, 101, 067, 111, 108, 111, 114, 080, 097, 115, 115, 123, 010, 083, 101, 116, 071, 101, 
			111, 109, 101, 116, 114, 121, 083, 104, 097, 100, 101, 114, 040, 032, 048, 032, 041, 059, 010, 083, 
			101, 116, 086, 101, 114, 116, 101, 120, 083, 104, 097, 100, 101, 114, 040, 032, 067, 111, 109, 112, 
			105, 108, 101, 083, 104, 097, 100, 101, 114, 040, 032, 118, 115, 095, 052, 095, 048, 044, 083, 112, 
			114, 105, 116, 101, 086, 101, 114, 116, 101, 120, 083, 104, 097, 100, 101, 114, 040, 041, 032, 041, 
			032, 041, 059, 010, 083, 101, 116, 080, 105, 120, 101, 108, 083, 104, 097, 100, 101, 114, 040, 032, 
			067, 111, 109, 112, 105, 108, 101, 083, 104, 097, 100, 101, 114, 040, 032, 112, 115, 095, 052, 095, 
			048, 044, 083, 112, 114, 105, 116, 101, 080, 105, 120, 101, 108, 083, 104, 097, 100, 101, 114, 040, 
			041, 032, 041, 032, 041, 059, 125, 010, 125, 010, 042, 047, 010, 117, 140, 064, 187, 232, 054, 126, 
			209, 103, 145, 103, 013, 029, 122, 079, 091, 098, 241, 220, 158, 076, 040, 008, 035, 172, 166, 125, 
			213, 214, 199, 171, 220, 149, 178, 155, 164, 238, 077, 175, 178, 130, 078, 077, 229, 197, 006, 010, 
			071, 037, 113, 028, 110, 096, 111, 079, 211, 240, 031, 083, 045, 193, 025, 040, 058
		};
		#endregion //SpriteBatchShader

	}
}
