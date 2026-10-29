using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
	[TestState(TestStateAttribute.TestState.Tested)]
    public struct KeyboardState
    {
        private readonly KeyState[] keyState;

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
            var results = new uint[8];
            for (int index = 0; index < keyState.Length; index++)
                if (keyState[index] == KeyState.Down)
                {
                    uint num = 1u << index;
                    int resultIndex = index >> 5;
                    results[resultIndex] |= num & 4294967295u;
                }

            return results.Aggregate(0, (current, u) => current ^ u.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            return obj is KeyboardState && this == (KeyboardState)obj;
        }

	    public static bool operator ==(KeyboardState lhs, KeyboardState rhs)
	    {
	        return lhs.keyState.Length == rhs.keyState.Length && !lhs.keyState.Where((t, i) => t != rhs.keyState[i]).Any();
	    }

	    public static bool operator !=(KeyboardState lhs, KeyboardState rhs)
        {
            return lhs.keyState.Length != rhs.keyState.Length || lhs.keyState.Where((t, i) => t != rhs.keyState[i]).Any();
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
