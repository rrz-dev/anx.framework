#region Using Statements
using System;
using System.Collections.Generic;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    public struct KeyboardState
    {
        #region Private Members
        private KeyState[] keyState;
        private List<Keys> pressedKeys; 

        #endregion // Private Members

        public KeyboardState(params Keys[] keys)
        {
            pressedKeys = new List<Keys>();
            pressedKeys.AddRange(keys);

            keyState = new KeyState[255];
            keyState.Initialize();

            for (int i = 0; i < keys.Length; i++)
            {
                keyState[(int)keys[i]] = KeyState.Down;
            }
        }

        public bool IsKeyDown(Keys key)
        {
            return keyState != null ? keyState[(int)key] == KeyState.Down : false;
        }

        public bool IsKeyUp(Keys key)
        {
            return keyState != null ? keyState[(int)key] == KeyState.Up : true;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(KeyboardState))
            {
                return this == (KeyboardState)obj;
            }

            return false;
        }

        public static bool operator ==(KeyboardState lhs, KeyboardState rhs)
        {
            if (lhs.keyState.Length != rhs.keyState.Length)
            {
                return false;
            }

            for (int i = 0; i < lhs.keyState.Length; i++)
            {
                if (lhs.keyState[i] != rhs.keyState[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(KeyboardState lhs, KeyboardState rhs)
        {
            return !(lhs == rhs);
        }

        public KeyState this[Keys key]
        {
            get
            {
                return keyState[(int)key];
            }
        }

        public Keys[] GetPressedKeys()
        {
            List<Keys> value = new List<Keys>();
            for (int i = 0; i < keyState.Length; ++i)
            {
                if (keyState[i] == KeyState.Down)
                {
                    value.Add((Keys)i);
                }
            }
            return value.ToArray();
        }

        internal void AddPressedKey(Keys key)
        {
            this.pressedKeys.Add(key);
            this.keyState[(int)key] = KeyState.Down;
        }

        internal void RemovePressedKey(Keys key)
        {
            this.pressedKeys.Remove(key);
            this.keyState[(int)key] = KeyState.Up;
        }

    }
}
