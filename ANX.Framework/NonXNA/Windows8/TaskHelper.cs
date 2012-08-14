using System;
using System.Threading.Tasks;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.Windows8
{
	public static class TaskHelper
	{
		public static T WaitForAsyncOperation<T>(Task<T> task)
		{
			task.Wait();
			return task.Result;
		}
	}
}
