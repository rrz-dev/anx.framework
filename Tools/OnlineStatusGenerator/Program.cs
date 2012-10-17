using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ANX.Framework.NonXNA.Development;

namespace OnlineStatusGenerator
{
    class Program
    {
        private const string GreenColor = "#A5DE94";
        private const string RedColor = "#AD0000";
        private const string YellowColor = "#FFFF9C";

        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFile(Path.GetFullPath("ANX.Framework.dll"));
            Type[] allTypes = assembly.GetTypes();
            var namespaces = new Dictionary<string, List<Type>>();
            foreach (Type type in allTypes)
            {
                if (type.Namespace.Contains("NonXNA") || type.IsPublic == false)
                    continue;

                if (namespaces.ContainsKey(type.Namespace) == false)
                    namespaces.Add(type.Namespace, new List<Type>());

                namespaces[type.Namespace].Add(type);
            }

            var sortedKeys = new List<string>(namespaces.Keys);
            sortedKeys.Sort();
            foreach (string space in sortedKeys)
                namespaces[space].Sort((o1, o2) => String.Compare(o1.Name, o2.Name, StringComparison.Ordinal));

            string result = "<table border=1 cellspacing=0>";
            foreach (string space in sortedKeys)
            {
                result += "\n<tr><td style=\"background-color: silver;\"><strong>" + space + "</strong></td><td style=\"background-color: silver;\" align=center>%</td><td style=\"background-color: silver;\" align=center>Developer</td><td style=\"background-color: silver;\" align=center>Test State</td></tr>";
                foreach (Type type in namespaces[space])
                {
                    result += "\n<tr><td>" + type.Name + "</td>";
                    result += GetPercentageCompleteBox(type);
                    result += GetDeveloperBox(type);
                    result += GetTestStateBox(type);
                    result += "</tr>";
                }
            }
            result += "</table>";

            File.WriteAllText("Report.html", result);
            System.Diagnostics.Process.Start("Report.html");
        }

        private static string GetPercentageCompleteBox(Type type)
        {
            var percentageAttribute = GetAttribute<PercentageCompleteAttribute>(type);
            int percent = percentageAttribute != null ? percentageAttribute.Percentage : -1;
            string result = percent != -1 ? percent.ToString() : "---";
            string bgColor = percent == -1 ? RedColor : (percent == 100 ? GreenColor : YellowColor);
            return "<td align=\"center\" style=\"background-color:" + bgColor + ";\">" + result + "</td>";
        }

        private static string GetTestStateBox(Type type)
        {
            var testAttribute = GetAttribute<TestStateAttribute>(type);
            string result = testAttribute != null ? testAttribute.State.ToString() : "---";
            string bgColor = testAttribute == null ? RedColor :
                (testAttribute.State == TestStateAttribute.TestState.Tested ? GreenColor : YellowColor);
            return "<td align=\"center\" style=\"background-color:" + bgColor + ";\">" + result + "</td>";
        }

        private static string GetDeveloperBox(Type type)
        {
            var developerAttribute = GetAttribute<DeveloperAttribute>(type);
            string result = developerAttribute != null ? developerAttribute.Developer : "---";
            string bgColor = developerAttribute == null ? RedColor :
                (developerAttribute.Developer != "???" ? "white" : YellowColor);
            return "<td align=\"center\" style=\"background-color:" + bgColor + ";\">" + result + "</td>";
        }

        private static T GetAttribute<T>(Type type) where T : Attribute
        {
            object[] attributes = type.GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? attributes[0] as T : null;
        }
    }
}
