using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProjectConverter
{
	public static class DefinesConverter
	{
		private static List<string> platformDefines;

		static DefinesConverter()
		{
			UpdateAllDefines();
		}

		#region UpdateAllDefines
		private static void UpdateAllDefines()
		{
			platformDefines = new List<string>();

			Assembly assembly = Assembly.GetExecutingAssembly();
			Type[] allTypes = assembly.GetTypes();
			foreach (Type type in allTypes)
			{
				if (typeof(Converter).IsAssignableFrom(type) &&
					type.IsAbstract == false)
				{
					Converter converter = (Converter)Activator.CreateInstance(type);
					platformDefines.Add(converter.Postfix.ToUpper());
				}
			}
		}
		#endregion

		#region ConvertDefines
		public static string ConvertDefines(string defines, string postfix)
		{
			var defineParts = new List<string>(defines.Split(';'));

			defineParts.Insert(0, postfix.ToUpper());

			string resultDefines = "";
			for (int index = 0; index < defineParts.Count; index++)
			{
				string define = defineParts[index];
				if (index == 0 ||
					platformDefines.Contains(define) == false)
				{
					resultDefines = define + ";" + resultDefines;
				}
			}

			return resultDefines;
		}
		#endregion
	}
}
