#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    [Developer("Glatzemann")]
    public interface IUpdateable
	{
		bool Enabled
		{
			get;
		}

		int UpdateOrder
		{
			get;
		}

		void Update(GameTime gameTime);

		event EventHandler<EventArgs> EnabledChanged;

		event EventHandler<EventArgs> UpdateOrderChanged;
	}
}
