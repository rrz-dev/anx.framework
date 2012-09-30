using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using ANX.Framework.NonXNA.Development;

namespace OnlineStatusGenerator
{
    class Program
    {
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

            string result = "<table border=1 cellspacing=0>";
            foreach (string space in sortedKeys)
            {
                result += "\n<tr><td style=\"background-color: silver;\"><strong>" + space + "</strong></td><td style=\"background-color: silver;\" align=center>%</td><td style=\"background-color: silver;\" align=center>Developer</td><td style=\"background-color: silver;\" align=center>Test State</td></tr>";
                foreach (Type type in namespaces[space])
                {
                    result += "\n<tr><td>" + type.Name + "</td>";
                    object[] percentageAttributes = type.GetCustomAttributes(typeof(PercentageCompleteAttribute), false);
                    var percentageAttribute = percentageAttributes.Length > 0 ?
                        percentageAttributes[0] as PercentageCompleteAttribute : null;
                    result += "<td align=center>" + (percentageAttribute != null ? percentageAttribute.Percentage.ToString() : "---") + "</td>";

                    object[] developerAttributes = type.GetCustomAttributes(typeof(DeveloperAttribute), false);
                    var developerAttribute = developerAttributes.Length > 0 ?
                        developerAttributes[0] as DeveloperAttribute : null;
                    result += "<td align=center>" + (developerAttribute != null ? developerAttribute.Developer : "---") + "</td>";

                    object[] testAttributes = type.GetCustomAttributes(typeof(TestStateAttribute), false);
                    var testAttribute = testAttributes.Length > 0 ?
                        testAttributes[0] as TestStateAttribute : null;
                    result += "<td align=center>" + (testAttribute != null ? testAttribute.State.ToString() : "---") + "</td>";

                    result += "</tr>";
                }
            }
            result += "</table>";

            File.WriteAllText("Report.html", result);
            System.Diagnostics.Process.Start("Report.html");
        }
    }
}
