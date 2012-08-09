// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public interface ICreator
	{
		void RegisterCreator(AddInSystemFactory factory);

		string Name { get; }

		int Priority { get; }

		bool IsSupported { get; }
	}
}
