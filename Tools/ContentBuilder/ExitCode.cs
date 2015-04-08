using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentBuilder
{
    public enum ExitCode : int
    {
        OK = 0,
        NothingToDo = 1,
        NotUpToDate = 2,
    }
}
