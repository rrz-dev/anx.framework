#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace ANX.Framework.Content
{
    public sealed class ContentTypeReaderManager
    {
        private ContentReader contentReader;
        private static Dictionary<string, ContentTypeReader> nameToReader = new Dictionary<string, ContentTypeReader>();
        private static Dictionary<Type, ContentTypeReader> readerTypeToReader = new Dictionary<Type, ContentTypeReader>();
        private static Dictionary<Type, ContentTypeReader> targetTypeToReader = new Dictionary<Type, ContentTypeReader>();

        private ContentTypeReaderManager(ContentReader contentReader)
        {
            this.contentReader = contentReader;
        }

        static ContentTypeReaderManager()
        {
            nameToReader = new Dictionary<string, ContentTypeReader>();
            targetTypeToReader = new Dictionary<Type, ContentTypeReader>();
            readerTypeToReader = new Dictionary<Type, ContentTypeReader>();
            ContentTypeReader value = new ObjectReader();
            targetTypeToReader.Add(typeof(object), value);
            readerTypeToReader.Add(typeof(ObjectReader), value);
        }

        private static void AddTypeReader(
            string readerTypeName, ContentReader contentReader, ContentTypeReader reader)
        {
            Type targetType = reader.TargetType;
            if (targetType != null)
            {
                if (targetTypeToReader.ContainsKey(targetType))
                {
                    throw new InvalidOperationException("Key already exist");
                }
                targetTypeToReader.Add(targetType, reader);
            }
            readerTypeToReader.Add(reader.GetType(), reader);
            nameToReader.Add(readerTypeName, reader);
        }

        internal static bool ContainsTypeReader(Type targetType)
        {
            bool result;
            lock (nameToReader)
            {
                result = targetTypeToReader.ContainsKey(targetType);
            }
            return result;
        }

        private static ContentTypeReader GetTypeReader(
            string readerTypeName, ContentReader contentReader, ref List<ContentTypeReader> newTypeReaders)
        {
            ContentTypeReader contentTypeReader;
            if (nameToReader.TryGetValue(readerTypeName, out contentTypeReader))
            {
                return contentTypeReader;
            }
            if (InstantiateTypeReader(readerTypeName, contentReader, out contentTypeReader))
            {
                AddTypeReader(readerTypeName, contentReader, contentTypeReader);
                if (newTypeReaders == null)
                {
                    newTypeReaders = new List<ContentTypeReader>();
                }
                newTypeReaders.Add(contentTypeReader);
            }
            return contentTypeReader;
        }

        internal static ContentTypeReader GetTypeReader(Type targetType, ContentReader contentReader)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException("targetType");
            }

            ContentTypeReader result;
            lock (nameToReader)
            {
                if (!targetTypeToReader.TryGetValue(targetType, out result))
                {
                    throw new ContentLoadException("TypeReaderNotRegistered");
                }
            }
            return result;
        }

        public ContentTypeReader GetTypeReader(Type targetType)
        {
            return GetTypeReader(targetType, this.contentReader);
        }

        private static bool InstantiateTypeReader(string readerTypeName, ContentReader contentReader, out ContentTypeReader reader)
        {
            bool result;
            try
            {
                Type type = Type.GetType(readerTypeName);
                if (type == null)
                {
                    throw new NotSupportedException(String.Format(
                        "Reader type not found {0}, reflective reader is not supported.", readerTypeName));
                }
                else
                {
                    if (ContentTypeReaderManager.readerTypeToReader.TryGetValue(type, out reader))
                    {
                        ContentTypeReaderManager.nameToReader.Add(readerTypeName, reader);
                        result = false;
                    }
                    else
                    {
                        reader = (ContentTypeReader)Activator.CreateInstance(type);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static ContentTypeReader[] ReadXnbTypeManifest(int numTypes, ContentReader reader)
        {
            ContentTypeReader[] typeReaders = new ContentTypeReader[numTypes];

            lock (ContentTypeReaderManager.nameToReader)
            {
                List<ContentTypeReader> readers = null;
                for (int i = 0; i < numTypes; i++)
                {
                    string readerTypeName = reader.ReadString();
                    int readerVersionNumber = reader.ReadInt32();
                    bool skipVersionCheck = false;
                    readerTypeName = XnaToAnxTypeName(readerTypeName, out skipVersionCheck);

                    ContentTypeReader typeReader = GetTypeReader(readerTypeName, reader, ref readers);

                    if (!skipVersionCheck && (readerVersionNumber != typeReader.TypeVersion))
                    {
                        throw new ContentLoadException("Bad reader type version");
                    }

                    typeReaders[i] = typeReader;
                }
                if (readers != null)
                {
                    var manager = new ContentTypeReaderManager(reader);
                    foreach (var item in readers)
                    {
                        item.Initialize(manager);
                    }
                }
            }

            return typeReaders;
        }

        private static string XnaToAnxTypeName(string name)
        {
            bool wasMapped;
            return XnaToAnxTypeName(name, out wasMapped);
        }

        private static string XnaToAnxTypeName(string name, out bool wasMapped)
        {
            //System.Diagnostics.Debugger.Launch();
            string xnaNamespace = "Microsoft.Xna";
            string anxNamespace = "ANX";
            wasMapped = false;

            // replace the namespace of the main type name
            if (name.StartsWith(xnaNamespace))
            {
                name = anxNamespace + name.Remove(0, xnaNamespace.Length);
                wasMapped = true;
            }
            
            // cut off the assembly declaration and the properties 
            // if the name was converted and has no generic type arguments
            if (!name.Contains("[[") && !name.Contains("]]"))
            {
                if (wasMapped && name.Contains(','))
                {
                    name = name.Remove(name.IndexOf(','));
                }
                return name;
            }

            // resolve generic type names and map each of them
            int genericStart = name.IndexOf("[[");
            int genericEnd = name.LastIndexOf("]]");
            string[] genericTypeNames = name.Substring(genericStart + 2, genericEnd - genericStart - 2).Split(new string[] { "],[" }, StringSplitOptions.None);
            List<string> mappedTypeNames = new List<string>();
            foreach (string item in genericTypeNames)
            {
                bool itemWasMapped;
                string mappedItem = XnaToAnxTypeName(item, out itemWasMapped);
                wasMapped = wasMapped || itemWasMapped;
                mappedTypeNames.Add(mappedItem);
            }

            // combine base type name with the generic type names
            name = name.Substring(0, genericStart + 2);
            for (int i = 0; i < mappedTypeNames.Count; i++)
            {
                name += mappedTypeNames[i];
                if (i < mappedTypeNames.Count - 1)
                {
                    name += "],[";
                }
                else
                {
                    name += "]]";
                }
            }

            return name;
        }
    }
}
