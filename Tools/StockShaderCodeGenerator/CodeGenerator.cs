#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Windows.DX10;
using System.IO;

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

namespace StockShaderCodeGenerator
{
    public static class CodeGenerator
    {
        public static void Generate()
        {
            Console.WriteLine("generating output...");

            using (StreamWriter target = new StreamWriter(Configuration.Target, false))
            {
                //
                // write header
                //
                target.WriteLine("#region Using Statements");
                target.WriteLine("using System;");
                target.WriteLine("#endregion // Using Statements");
                target.WriteLine();
                target.WriteLine("#region License");

                foreach (string line in System.IO.File.ReadAllLines(Configuration.LicenseFile))
                {
                    target.WriteLine(line);
                }

                target.WriteLine("#endregion // License");
                target.WriteLine();
                target.WriteLine("namespace {0}", Configuration.Namespace);
                target.WriteLine("{");
                target.WriteLine("\tinternal static class ShaderByteCode");
                target.WriteLine("\t{");

                for (int i = 0; i < Configuration.Shaders.Count; i++)
                {
                    Shader s = Configuration.Shaders[i];

                    target.WriteLine("\t\t#region {0}Shader", s.Type);
                    target.WriteLine("\t\tinternal static byte[] {0}ByteCode = new byte[]", s.Type);
                    target.WriteLine("\t\t{");

                    target.Write("\t\t\t");
                    for (int j = 0; j < s.ByteCode.Length; j++)
                    {
                        target.Write(s.ByteCode[j].ToString("D3"));
                        if (j < s.ByteCode.Length - 1)
                        {
                            target.Write(", ");
                        }

                        if (j % 20 == 0)
                        {
                            target.WriteLine();
                            target.Write("\t\t\t");
                        }
                    }

                    target.WriteLine();
                    target.WriteLine("\t\t};");
                    target.WriteLine("\t\t#endregion //{0}Shader", s.Type);
                    target.WriteLine();
                }

                target.WriteLine("\t}");
                target.WriteLine("}");
            }

            Console.WriteLine("finished generating output...");
        }
    }
}
