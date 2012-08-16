using System;

namespace HLSLParser
{
	public class ParseTextWalker
	{
		public string Text
		{
			get;
			private set;
		}

		public ParseTextWalker(string setText)
		{
			Text = setText;
			Seek(0);
		}

		public void Seek(int length)
		{
			Text = Text.Substring(length);
			string text = Text;
			text = text.TrimStart(' ', '\n', '\r', '\t');
			length = Text.Length - text.Length;
			Text = Text.Substring(length);
		}

		public string RemoveUnneededChars(string text)
		{
			text = text.Replace("\n", "");
			text = text.Replace("\r", "");
			text = text.Replace("\t", "");
			return text;
		}
	}
}
