#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(10)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class GamerServicesComponent : GameComponent
    {
        public GamerServicesComponent(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            // nothing more to do
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // nothing more to do
        }


    }
}
