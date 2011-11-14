using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Input;

namespace ANX.Framework.NonXNA
{
    public interface IKeyboard
    {
        KeyboardState GetState();
        KeyboardState GetState(PlayerIndex playerIndex);
    }
}
