using System;
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

namespace ANX.Framework.Net
{
	public class PacketReader : BinaryReader
	{
		public int Length
		{
			get
			{
				return (int)BaseStream.Length;
			}
		}

		public int Position
		{
			get
			{
				return (int)BaseStream.Position;
			}
			set
			{
				BaseStream.Position = (long)value;
			}
		}

		public PacketReader()
			: this(0)
		{
		}

		public PacketReader(int capacity)
			: base(new MemoryStream(capacity))
		{
		}

		public Vector2 ReadVector2()
		{
			return new Vector2
			{
				X = ReadSingle(),
				Y = ReadSingle()
			};
		}

		public Vector3 ReadVector3()
		{
			return new Vector3
			{
				X = ReadSingle(),
				Y = ReadSingle(),
				Z = ReadSingle()
			};
		}

		public Vector4 ReadVector4()
		{
			return new Vector4
			{
				X = ReadSingle(),
				Y = ReadSingle(),
				Z = ReadSingle(),
				W = ReadSingle()
			};
		}

		public Matrix ReadMatrix()
		{
			return new Matrix
			{
				M11 = ReadSingle(),
				M12 = ReadSingle(),
				M13 = ReadSingle(),
				M14 = ReadSingle(),
				M21 = ReadSingle(),
				M22 = ReadSingle(),
				M23 = ReadSingle(),
				M24 = ReadSingle(),
				M31 = ReadSingle(),
				M32 = ReadSingle(),
				M33 = ReadSingle(),
				M34 = ReadSingle(),
				M41 = ReadSingle(),
				M42 = ReadSingle(),
				M43 = ReadSingle(),
				M44 = ReadSingle()
			};
		}

		public Quaternion ReadQuaternion()
		{
			return new Quaternion
			{
				X = ReadSingle(),
				Y = ReadSingle(),
				Z = ReadSingle(),
				W = ReadSingle()
			};
		}

		public Color ReadColor()
		{
			return new Color
			{
				PackedValue = ReadUInt32()
			};
		}
	}
}
