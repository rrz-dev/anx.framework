using System;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
    public struct KeyboardState
    {
        #region Private
        private readonly KeyState[] keyState;
		#endregion

	    public KeyState this[Keys key]
	    {
	        get { return keyState[(int)key]; }
	    }

	    public KeyboardState(params Keys[] keys)
        {
            keyState = new KeyState[255];
            for (int i = 0; i < keys.Length; i++)
                keyState[(int)keys[i]] = KeyState.Down;
        }

        public bool IsKeyDown(Keys key)
        {
            return keyState[(int)key] == KeyState.Down;
        }

        public bool IsKeyUp(Keys key)
        {
            return keyState[(int)key] == KeyState.Up;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is KeyboardState && this == (KeyboardState)obj;
        }

	    public static bool operator ==(KeyboardState lhs, KeyboardState rhs)
        {
            if (lhs.keyState.Length != rhs.keyState.Length)
                return false;

            for (int i = 0; i < lhs.keyState.Length; i++)
                if (lhs.keyState[i] != rhs.keyState[i])
                    return false;

            return true;
        }

        public static bool operator !=(KeyboardState lhs, KeyboardState rhs)
		{
			if (lhs.keyState.Length == rhs.keyState.Length)
			{
				for (int i = 0; i < lhs.keyState.Length; i++)
					if (lhs.keyState[i] != rhs.keyState[i])
						return true;

				return false;
			}

			return true;
        }

        public Keys[] GetPressedKeys()
        {
            var result = new List<Keys>();
            for (int i = 0; i < keyState.Length; ++i)
                if (keyState[i] == KeyState.Down)
                    result.Add((Keys)i);

            return result.ToArray();
        }

        internal void AddPressedKey(Keys key)
        {
            this.keyState[(int)key] = KeyState.Down;
        }

        internal void RemovePressedKey(Keys key)
        {
            this.keyState[(int)key] = KeyState.Up;
        }

    }
}
