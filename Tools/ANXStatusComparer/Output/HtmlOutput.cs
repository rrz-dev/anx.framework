using System;
using System.Collections.Generic;
using System.IO;
using ANXStatusComparer.Data;

namespace ANXStatusComparer.Output
{
	public static class HtmlOutput
	{
		#region Constants
		public const string HtmlFilepath = "Summary.html";

		private const string EmptyCell = @"<td class=""emptyCell"">&nbsp;</td>";
		#endregion

		#region GenerateOutput
		/// <summary>
		/// Generate an html result summary.
		/// </summary>
		/// <param name="result">Result data to output.</param>
		/// <param name="stylesheetFile">Filepath to the stylesheet.</param>
		/// <returns>Finished result output.</returns>
		public static void GenerateOutput(ResultData result, string stylesheetFile)
		{
			string text =
@"<html>
<head>
	<link rel=""stylesheet"" type=""text/css"" href=""" + stylesheetFile + @""">
</head>
<body>";

			#region Summary
			text += String.Format(@"
<table>
	<tr>
		<td class=""elementHeader"">Summary</td>
		<td class=""missingColor"">Missing</td>
		<td class=""wrongColor"">Wrong</td>
		<td class=""implementedColor"">Implemented</td>
	</tr>
	<tr>
		<td>Namespace</td>
		<td><a href=""#MissNamespaces"">{0}</a></td>
		<td>&nbsp;</td>
		<td><a href=""#ImplNamespaces"">{1}</a></td>
	</tr>
	<tr>
		<td>Class</td>
		<td><a href=""#MissClasses"">{2}</a></td>
		<td><a href=""#WrongClasses"">{3}</a></td>
		<td><a href=""#ImplClasses"">{4}</td>
	</tr>
	<tr>
		<td>Struct</td>
		<td><a href=""#MissStructs"">{5}</td>
		<td><a href=""#WrongStructs"">{6}</td>
		<td><a href=""#ImplStructs"">{7}</td>
	</tr>
	<tr>
		<td>Interface</td>
		<td><a href=""#MissInterfaces"">{8}</td>
		<td><a href=""#WrongInterfaces"">{9}</td>
		<td><a href=""#ImplInterfaces"">{10}</td>
	</tr>
	<tr>
		<td>Enum</td>
		<td><a href=""#MissEnums"">{11}</td>
		<td><a href=""#WrongEnums"">{12}</td>
		<td><a href=""#ImplEnums"">{13}</td>
	</tr>
</table>",
				result.MissingNamespaces.Count,
				result.ImplementedNamespaces.Count,

				result.MissingClasses.Count,
				result.WrongClasses.Count,
				result.ImplementedClasses.Count,

				result.MissingStructs.Count,
				result.WrongStructs.Count,
				result.ImplementedStructs.Count,

				result.MissingInterfaces.Count,
				result.WrongInterfaces.Count,
				result.ImplementedInterfaces.Count,

				result.MissingEnums.Count,
				result.WrongEnums.Count,
				result.ImplementedEnums.Count);
			#endregion

			#region Implemented/Missing Namespaces
			text += @"
<a name=""MissNamespaces"" />
<table>
	<tr>
		<td colspan=""2"" class=""missingColor"">Missing Namespaces</td>
	</tr>";
			foreach (string missingNamespace in result.MissingNamespaces)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, missingNamespace);
			}

			text += @"
</table>
<a name=""ImplNamespaces"" />
<table>
	<tr>
		<td colspan=""2"" class=""implementedColor"">Implemented Namespaces</td>
	</tr>";
			foreach (string implementedNamespace in result.ImplementedNamespaces)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, implementedNamespace);
			}

			text += "\n</table>";
			#endregion

			text += CreateWrongOutput("Classes", result.WrongClasses);

			#region Implemented/Missing Classes
			text += @"
<a name=""MissClasses"" />
<table>
	<tr>
		<td colspan=""2"" class=""missingColor"">Missing Classes</td>
	</tr>";
			foreach (BaseObject classData in result.MissingClasses)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, classData.Handle.Name);
			}

			text += @"
</table>
<a name=""ImplClasses"" />
<table>
	<tr>
		<td colspan=""2"" class=""implementedColor"">Implemented Classes</td>
	</tr>";
			foreach (BaseObject classData in result.ImplementedClasses)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, classData.Handle.Name);
			}

			text += "\n</table>";
			#endregion

			text += CreateWrongOutput("Structs", result.WrongStructs);

			#region Implemented/Missing Structs
			text += @"
<a name=""MissStructs"" />
<table>
	<tr>
		<td colspan=""2"" class=""missingColor"">Missing Structs</td>
	</tr>";
			foreach (BaseObject structData in result.MissingStructs)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, structData.Handle.Name);
			}

			text += @"
</table>
<a name=""ImplStructs"" />
<table>
	<tr>
		<td colspan=""2"" class=""implementedColor"">Implemented Structs</td>
	</tr>";
			foreach (BaseObject structData in result.ImplementedStructs)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, structData.Handle.Name);
			}

			text += "\n</table>";
			#endregion

			text += CreateWrongOutput("Interfaces", result.WrongInterfaces);

			#region Implemented/Missing Interfaces
			text += @"
<a name=""MissInterfaces"" />
<table>
	<tr>
		<td colspan=""2"" class=""missingColor"">Missing Interfaces</td>
	</tr>";
			foreach (BaseObject interfaceData in result.MissingInterfaces)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, interfaceData.Handle.Name);
			}

			text += @"
</table>
<a name=""ImplInterfaces"" />
<table>
	<tr>
		<td colspan=""2"" class=""implementedColor"">Implemented Interfaces</td>
	</tr>";
			foreach (BaseObject interfaceData in result.ImplementedInterfaces)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, interfaceData.Handle.Name);
			}

			text += "\n</table>";
			#endregion

			#region Wrong Enums
			text += @"
<a name=""WrongEnums"" />
<table>
	<tr>
		<td colspan=""4"" class=""wrongColor"">Wrong Enums</td>
	</tr>";
			foreach (KeyValuePair<EnumData, EnumData> wrongEnum in result.WrongEnums)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td class=""elementHeader"">{1}</td>
		<td>XNA</td>
		<td>ANX</td>
	</tr>",
				EmptyCell, wrongEnum.Key.Handle.Name);

				string col1 = "";
				for (int index = 0; index < wrongEnum.Key.Names.Count; index++)
				{
					col1 += wrongEnum.Key.Names[index] + "=" +
						wrongEnum.Key.Values[index] + "<br />\n";
				}
				string col2 = "";
				for (int index = 0; index < wrongEnum.Value.Names.Count; index++)
				{
					col2 += wrongEnum.Value.Names[index] + "=" +
						wrongEnum.Value.Values[index] + "<br />\n";
				}
				text += String.Format(@"
	<tr>
		{0}
		{0}
		<td nowrap=""nowrap"">{1}</td>
		<td nowrap=""nowrap"">{2}</td>
	</tr>",
				EmptyCell, col1, col2);
			}

			text += "\n</table>";
			#endregion

			#region Implemented/Missing Enums
			text += @"
<a name=""MissEnums"" />
<table>
	<tr>
		<td colspan=""2"" class=""missingColor"">Missing Enums</td>
	</tr>";
			foreach (EnumData enumeration in result.MissingEnums)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, enumeration.Handle.Name);
			}

			text += @"
</table>
<a name=""ImplEnums"" />
<table>
	<tr>
		<td colspan=""2"" class=""implementedColor"">Implemented Enums</td>
	</tr>";
			foreach (EnumData enumeration in result.ImplementedEnums)
			{
				text += String.Format(@"
	<tr>
		{0}
		<td>{1}</td>
	</tr>",
				EmptyCell, enumeration.Handle.Name);
			}

			text += "\n</table>";
			#endregion

			text += @"
</body>
</html>";

			File.WriteAllText(HtmlFilepath, text.Replace("\t", "  "));
		}
		#endregion

		#region CreateWrongOutput
		private static string CreateWrongOutput(string type,
			List<ResultData.WrongObjectPair> pairs)
		{
			string result = @"
<a name=""Wrong" + type + @""" />
<table>
	<tr>
		<td colspan=""5"" class=""wrongColor"">Wrong " + type + @"</td>
	</tr>";
			foreach (ResultData.WrongObjectPair wrongPair in pairs)
			{
				#region Missing parents
				string missingParents = "";
				if (wrongPair.MissingParents.Count > 0)
				{
					foreach (string parent in wrongPair.MissingParents)
					{
						missingParents += parent + ", ";
					}

					missingParents = String.Format(@"
	<tr>
		{0}
		{0}
		<td colspan=""4"">Missing Parents: {1}</td>
	</tr>",
						EmptyCell, missingParents);
				}
				#endregion

				#region Wrong access
				string wrongAccess = "";
				if (wrongPair.WrongAccesses.Count > 0)
				{
					foreach (string access in wrongPair.WrongAccesses)
					{
						wrongAccess += access;
					}

					wrongAccess = String.Format(@"
	<tr>
		{0}
		{0}
		<td colspan=""4"">Wrong Access: {1}</td>
	</tr>",
						EmptyCell, wrongAccess);
				}
				#endregion

				result += String.Format(@"
	<tr>
		{0}
		<td colspan=""4"" class=""elementHeader"">{1}</td>
	</tr>
	<tr>
		{0}
		{0}
		<td>Type</td>
		<td>XNA</td>
		<td>ANX</td>
	</tr>
	{2}
	{3}",
				EmptyCell, wrongPair.XnaObject.Handle.FullName,
				missingParents, wrongAccess);

				string lastType = "";
				string col1 = "";

				string col2 = "";
				foreach (BaseObjectElement element in wrongPair.XnaElements)
				{
					string elementTypeString = element is FieldElement
					                           	? "Field"
					                           	: element is PropertyElement
					                           	  	? "Property"
					                           	  	: element is MethodElement
					                           	  	  	? "Method"
					                           	  	  	: element is ConstructorElement
					                           	  	  	  	? "Constructor"
					                           	  	  	  	: "Event";
					if (lastType != elementTypeString)
					{
						lastType = elementTypeString;
						col1 += elementTypeString;
					}
					col1 += "<br />\n";

					col2 += element.GetDescription() + "<br />\n";
				}
				string col3 = "";
				foreach (BaseObjectElement element in wrongPair.AnxElements)
				{
					col3 += (element != null ? element.GetDescription() :
						"&lt;Missing&gt;") +
						"<br />\n";
				}

				result += String.Format(@"
	<tr>
		{0}
		{0}
		<td nowrap=""nowrap"">{1}</td>
		<td nowrap=""nowrap"">{2}</td>
		<td nowrap=""nowrap"">{3}</td>
	</tr>",
				EmptyCell, col1, col2, col3);
			}

			result += "\n</table>";

			return result;
		}
		#endregion
	}
}
