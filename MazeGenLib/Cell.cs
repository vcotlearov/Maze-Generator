using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGenLib
{
    public struct Cell
    {
        public int row;
        public int col;

        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
