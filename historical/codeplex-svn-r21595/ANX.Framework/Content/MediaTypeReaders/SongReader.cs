using ANX.Framework.Media;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    internal class SongReader : ContentTypeReader<Song>
    {
        protected internal override Song Read(ContentReader input, Song existingInstance)
        {
            string text = input.ReadString();
            text = input.GetAbsolutePathToReference(text);
            int duration = input.ReadObject<int>();
            return new Song(input.AssetName, text, duration);
        }
    }
}
