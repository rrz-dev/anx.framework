using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	[ANX.Framework.NonXNA.Development.PercentageComplete(100)]
	public interface IDrawable
	{
		event EventHandler<EventArgs> DrawOrderChanged;
		event EventHandler<EventArgs> VisibleChanged;

		int DrawOrder
		{
			get;
		}

		bool Visible
		{
			get;
		}

		void Draw(GameTime gameTime);
	}
}
