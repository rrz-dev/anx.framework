using System;
using System.Collections.ObjectModel;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
	[PercentageComplete(100)]
    [Developer("Glatzemann")]
	[TestState(TestStateAttribute.TestState.Tested)]
    public interface IAvatarAnimation
    {
        ReadOnlyCollection<Matrix> BoneTransforms { get; }
        TimeSpan CurrentPosition { get; set; }
        AvatarExpression Expression { get; }
        TimeSpan Length { get; }
        
		void Update(TimeSpan elapsedAnimationTime, bool loop);
    }
}
