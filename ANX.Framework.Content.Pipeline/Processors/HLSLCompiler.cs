#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

#endregion

namespace ANX.Framework.Content.Pipeline.Processors
{
    public class HLSLCompiler : IComparable<HLSLCompiler>, IEquatable<HLSLCompiler>
    {
        private string executable;
        private string version;
        private string helpOutput;
        private string[] profiles;

        public HLSLCompiler(string executable)
        {
            if (!String.IsNullOrEmpty(executable) && File.Exists(executable))
            {
                this.executable = executable;
            }
            else
            {
                throw new ArgumentNullException("executable", "fxc.exe does not exist");
            }
        }

        public string Version
        {
            get
            {
                if (String.IsNullOrEmpty(version))
                {
                    version = CompilerVersion;
                }

                return version;
            }
        }

        public IEnumerable<string> SupportedProfiles
        {
            get
            {
                if (profiles == null || profiles.Length <= 0)
                {
                    const string profileStart = @"(cs|ds|fx|ps|hs|gs|vs|tx)(_)(\S+)\s?";

                    Regex pattern = new Regex(profileStart, RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                    MatchCollection m = pattern.Matches(CompilerHelpOutput);

                    HashSet<String> tempProfiles = new HashSet<string>();
                    foreach (Match profileMatch in m)
                    {
                        tempProfiles.Add(profileMatch.Value.Trim());
                    }

                    profiles = tempProfiles.ToArray<String>();
                }

                return profiles;
            }
        }

        public byte[] Compile(string source, EffectProcessorDebugMode debugMode, string targetProfile)
        {
            string tempInputFile = CreateTemporaryShaderFile(source);
            string tempOutputFile = Path.GetTempFileName();
            byte[] byteCode;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = executable,
                Arguments = String.Format("{0} {1} {2} {3}", "/Fo " + tempOutputFile, GetCompilerTargetProfile(targetProfile), GetCompilerDebugFlags(debugMode), tempInputFile),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var proc = Process.Start(startInfo))
            {
                helpOutput = proc.StandardOutput.ReadToEnd();
            }

            byteCode = File.ReadAllBytes(tempOutputFile);

            if (File.Exists(tempInputFile))
            {
                File.Delete(tempInputFile);
            }

            if (File.Exists(tempOutputFile))
            {
                File.Delete(tempOutputFile);
            }

            return byteCode;
        }

        public int CompareTo(HLSLCompiler other)
        {
            return Version.CompareTo(other.Version);
        }

        public bool Equals(HLSLCompiler other)
        {
            return String.Equals(Version, other.Version, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Version.GetHashCode();
        }

        private string GetCompilerDebugFlags(EffectProcessorDebugMode debugMode)
        {
            if ((debugMode == EffectProcessorDebugMode.Auto && System.Diagnostics.Debugger.IsAttached) || debugMode == EffectProcessorDebugMode.Debug)
            {
                return "/Od /Op /Zi";
            }
            else
            {
                return "/O3 /Qstrip_debug";
            }
        }

        private string GetCompilerTargetProfile(string targetProfile)
        {
            foreach (string profile in SupportedProfiles)
            {
                if (string.Equals(profile, targetProfile, StringComparison.InvariantCultureIgnoreCase))
                {
                    return String.Format("/T {0}", targetProfile);
                }
            }

            throw new Exception(String.Format("fxc.exe version {0} does not support profile {1}", CompilerVersion, targetProfile));
        }

        private string CreateTemporaryShaderFile(string shaderSourceCode)
        {
            string file = Path.GetTempFileName();
            File.WriteAllText(file, shaderSourceCode);
            return file;
        }

        private string CompilerHelpOutput
        {
            get
            {
                if (string.IsNullOrEmpty(helpOutput))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = executable,
                        Arguments = @"/help",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };

                    using (var proc = Process.Start(startInfo))
                    {
                        helpOutput = proc.StandardOutput.ReadToEnd();
                    }
                }

                return helpOutput;
            }
        }

        private string CompilerVersion
        {
            get
            {
                Regex pattern = new Regex(@"\d+(\.\d+)+");
                Match m = pattern.Match(CompilerHelpOutput);
                if (m.Length > 0)
                {
                    return m.Value;
                }

                return "";
            }
        }
    }
}
