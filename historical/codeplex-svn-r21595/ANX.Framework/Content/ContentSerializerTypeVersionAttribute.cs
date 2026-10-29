#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    [PercentageComplete(100)]
    [Developer("GinieDP")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class ContentSerializerTypeVersionAttribute : Attribute
    {
        public int TypeVersion { get; private set; }

        public ContentSerializerTypeVersionAttribute(int typeVersion)
        {
            this.TypeVersion = typeVersion;
        }
    }
}
