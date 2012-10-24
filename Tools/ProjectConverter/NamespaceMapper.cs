#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
    internal class NamespaceMapping
    {
        public string Xna { get; set; }
        public string Anx { get; set; }
        public string XnaAssembly { get; set; }
        public string AnxAssembly { get; set; }

        public NamespaceMapping(string Xna, string Anx)
        {
            this.Xna = Xna;
            this.Anx = Anx;
        }

        public NamespaceMapping(string Xna, string Anx, string XnaAssembly, string AnxAssembly)
        {
            this.Xna = Xna;
            this.Anx = Anx;
            this.XnaAssembly = XnaAssembly;
            this.AnxAssembly = AnxAssembly;
        }

        public string GetInput(MappingDirection dir)
        {
            if (dir == MappingDirection.Xna2Anx)
            {
                return Xna;
            }
            else
            {
                return Anx;
            }
        }

        public string GetOutput(MappingDirection dir)
        {
            if (dir == MappingDirection.Xna2Anx)
            {
                return Anx;
            }
            else
            {
                return Xna;
            }
        }
    }

    public enum MappingDirection
    {
        Xna2Anx,
        Anx2Xna,
    }

    internal class NamespaceMapper
    {
        private static string xnaPrefix = "Microsoft.Xna.Framework";
        private static string anxPrefix = "ANX.Framework";

        private static NamespaceMapping[] mappings = new[] { new NamespaceMapping(xnaPrefix                    , anxPrefix                    , "Microsoft.Xna.Framework"              , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Avatar"        , anxPrefix + ".Avatar"        , "Microsoft.Xna.Framework.Avatar"       , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Graphics"      , anxPrefix + ".Graphics"      , "Microsoft.Xna.Framework.Graphics"     , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Input"         , anxPrefix + ".Input"         , "Microsoft.Xna.Framework"              , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Input.Touch"   , anxPrefix + ".Input.Touch"   , "Microsoft.Xna.Framework.Input.Touch"  , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Game"          , anxPrefix + ".Game"          , "Microsoft.Xna.Framework.Game"         , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".GamerServices" , anxPrefix + ".GamerServices" , "Microsoft.Xna.Framework.GamerServices", "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Net"           , anxPrefix + ".Net"           , "Microsoft.Xna.Framework.Net"          , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Storage"       , anxPrefix + ".Storage"       , "Microsoft.Xna.Framework.Storage"      , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Video"         , anxPrefix + ".Video"         , "Microsoft.Xna.Framework.Video"        , "ANX.Framework"),
                                                             new NamespaceMapping(xnaPrefix + ".Xact"          , anxPrefix + ".Xact"          , "Microsoft.Xna.Framework.Xact"         , "ANX.Framework"),
                                                             
                                                             new NamespaceMapping("", "ANX.InputDevices.OpenTK"),
                                                             new NamespaceMapping("", "ANX.InputDevices.PsVita"),
                                                             new NamespaceMapping("", "ANX.InputDevices.Test"),
                                                             new NamespaceMapping("", "ANX.InputDevices.Windows.Kinect"),
                                                             new NamespaceMapping("", "ANX.InputDevices.Windows.ModernUI"),
                                                             new NamespaceMapping("", "ANX.InputDevices.Windows.XInput"),
                                                             new NamespaceMapping("", "ANX.InputSystem.Recording"),
                                                             new NamespaceMapping("", "ANX.InputSystem.Standard"),

                                                             new NamespaceMapping("", "ANX.PlatformSystem.Linux"),
                                                             new NamespaceMapping("", "ANX.PlatformSystem.Metro"),
                                                             new NamespaceMapping("", "ANX.PlatformSystem.PsVita"),
                                                             new NamespaceMapping("", "ANX.PlatformSystem.Windows"),

                                                             new NamespaceMapping("", "ANX.RenderSystem.GL3"),
                                                             new NamespaceMapping("", "ANX.RenderSystem.PsVita"),
                                                             new NamespaceMapping("", "ANX.RenderSystem.Windows.DX10"),
                                                             new NamespaceMapping("", "ANX.RenderSystem.Windows.DX11"),
                                                             new NamespaceMapping("", "ANX.RenderSystem.Windows.Metro"),

                                                             new NamespaceMapping("", "ANX.SoundSystem.OpenAL"),
                                                             new NamespaceMapping("", "ANX.SoundSystem.PsVita"),
                                                             new NamespaceMapping("", "ANX.SoundSystem.Windows.XAudio"),

                                                           };

        public static string TryMapNamespace(MappingDirection dir, string input)
        {
            foreach (NamespaceMapping mapping in mappings)
            {
                if (String.Equals(input, mapping.GetInput(dir), StringComparison.InvariantCultureIgnoreCase))
                {
                    return mapping.GetOutput(dir);
                }
            }

            return input;
        }

        public static bool IsProjectReference(MappingDirection dir, string reference)
        {
            foreach (NamespaceMapping mapping in mappings)
            {
                if (String.Equals(reference, mapping.GetInput(dir), StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetReferencingAssemblyName(MappingDirection dir, string reference)
        {
            foreach (NamespaceMapping mapping in mappings)
            {
                if (String.Equals(reference, mapping.GetInput(dir), StringComparison.InvariantCultureIgnoreCase))
                {
                    if (dir == MappingDirection.Xna2Anx)
                    {
                        return mapping.AnxAssembly;
                    }
                    else
                    {
                        return mapping.XnaAssembly;
                    }
                }
            }

            return string.Empty;
        }
    }
}
