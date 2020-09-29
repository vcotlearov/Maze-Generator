using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGenLib
{
    [Flags]
    public enum MazeDirection
    {
        Empty = 0,
        Down = 2,
        Right = 4,
        Up = 8,
        Left = 16
    }
}
