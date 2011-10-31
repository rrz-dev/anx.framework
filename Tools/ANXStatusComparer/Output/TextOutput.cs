using System;
using ANXStatusComparer.Data;
using System.Collections.Generic;

namespace ANXStatusComparer.Output
{
	public static class TextOutput
	{
		/// <summary>
		/// Generate a text result summary.
		/// </summary>
		/// <param name="result">Result data to output.</param>
		/// <returns>Finished result output.</returns>
		public static string GenerateOutput(ResultData result)
		{
			string text = "missing namespaces\n---------------------\n";
			foreach (string missingNamespace in result.MissingNamespaces)
			{
				text += missingNamespace + "\n";
			}

			text += "\nimplemented namespaces\n---------------------\n";
			foreach (string implementedNamespace in result.ImplementedNamespaces)
			{
				text += implementedNamespace + "\n";
			}

			text += "\nmissing enums\n---------------------\n";
			foreach (EnumData missingEnum in result.MissingEnums)
			{
				text += missingEnum.Handle.Name + "\n";
			}

			text += "\nwrong enums\n---------------------\n";
			foreach (KeyValuePair<EnumData, EnumData> wrongEnum in result.WrongEnums)
			{
				text += wrongEnum.Key.Handle.Name + "\n";
			}

			text += "\nimplemented enums\n---------------------\n";
			foreach (EnumData implementedEnum in result.ImplementedEnums)
			{
				text += implementedEnum.Handle.Name + "\n";
			}
			return text;
		}
	}
}
