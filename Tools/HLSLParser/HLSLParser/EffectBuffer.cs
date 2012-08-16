using System;

namespace HLSLParser
{
	public class EffectBuffer
	{
		#region Public
		public string Name
		{
			get;
			private set;
		}

		public string Type
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		public EffectBuffer(ParseTextWalker walker)
		{
			string text = walker.Text;

			int typeStartIndex = text.IndexOf('<') + 1;
			int typeEndIndex = text.IndexOf('>');
			Type = text.Substring(typeStartIndex, typeEndIndex - typeStartIndex);

			typeEndIndex++;

			int semicolonIndex = text.IndexOf(';');
			Name = text.Substring(typeEndIndex, semicolonIndex - typeEndIndex).Trim();

			walker.Seek(semicolonIndex + 1);
		}
		#endregion

		#region ParseIfBuffer
		public static EffectBuffer ParseIfBuffer(ParseTextWalker walker)
		{
			if (walker.Text.StartsWith("Buffer"))
			{
				return new EffectBuffer(walker);
			}

			return null;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "Buffer<" + Type + "> " + Name + ";";
		}
		#endregion
	}
}
