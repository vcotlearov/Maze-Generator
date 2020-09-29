using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGenLib
{
    [Flags]
    public enum MazeDirection : byte
    {
        Empty = 0b_0000_0000,
        Down = 0b_0000_0010,
        Right = 0b_0000_0100,
        Up = 0b_0000_1000,
        Left = 0b_0001_0000
    }
}
