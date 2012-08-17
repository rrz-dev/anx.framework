using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParser
{
	public static class CommentRemover
	{
		#region Remove
		public static string Remove(string source)
		{
			if (source == null)
			{
				return null;
			}

			source = RemoveMultilineComments(source);
			source = RemoveSingleLineComments(source);

			return source;
		}
		#endregion

		#region RemoveMultilineComments
		private static string RemoveMultilineComments(string source)
		{
			int currentIndex = 0;
			int textLength = source.Length;
			while (currentIndex < textLength)
			{
				currentIndex = source.IndexOf("/*", currentIndex);
				if (currentIndex == -1)
				{
					break;
				}

				if (currentIndex != 0 &&
					source[currentIndex - 1] == '/')
				{
					currentIndex++;
					continue;
				}
				
				int endIndex = source.IndexOf("*/", currentIndex);
				source = source.Remove(currentIndex, endIndex - currentIndex + 2);
				textLength = source.Length;
			}

			return source;
		}
		#endregion

		#region RemoveSingleLineComments
		private static string RemoveSingleLineComments(string source)
		{
			string[] lines = SplitLines(source);

			for (int index = 0; index < lines.Length; index++)
			{
				string line = lines[index];
				int commentIndex = line.IndexOf("//");
				if (commentIndex == -1)
					continue;

				lines[index] = line.Substring(0, commentIndex);
			}

			return MergeLines(lines);
		}
		#endregion

		#region SplitLines
		internal static string[] SplitLines(string source)
		{
			source = source.Replace('\r', '\n');
			return source.Split(new char[] { '\n' },
				StringSplitOptions.RemoveEmptyEntries);
		}
		#endregion

		#region MergeLines
		internal static string MergeLines(string[] lines)
		{
			string result = "";

			foreach (string line in lines)
			{
				if (line == null)
					continue;

				if (String.IsNullOrEmpty(line.Trim()) == false)
					result += line + "\n";
			}

			return result.TrimEnd('\n');
		}
		#endregion
	}
}
