using System;
using System.Collections.Generic;

namespace MazeGenLib
{
    public class Maze
    {
        private int _rowNumber;
        private int _colNumber;

        MazeDirection[,] maze;

        private static Random _r = new Random();

        public Maze() { }

        public Maze(int rows, int cols)
        {
            if (cols <= 0)
                throw new ArgumentException("Column number should be positive");
            if (rows <= 0)
                throw new ArgumentException("Row number should be positive");
            
            _colNumber = cols;
            _rowNumber = rows;
        }

        public MazeDirection[,] PrepareMaze()
        {
            getEmptyBoard();
            (int row, int col) entry = getRandomEntryPoint();

            var res = TryMakePassage(entry);

            if(res)
                return maze;

            return null;
        }

        private bool TryMakePassage((int row, int col) cell)
        {
            if (!checkIfHasEmptyBoard())
                return true;

            while (true)
            {
                var availableDirections = getAvailableDirections(cell);
                if (availableDirections.Count == 0) return false;

                MazeDirection direction = availableDirections[_r.Next(availableDirections.Count)];
                var nextPosition = SetMaze(cell, direction);

                availableDirections.Remove(direction);
                var res = TryMakePassage(nextPosition);

                if (res)
                    return true;
            }
        }

        private (int row, int col) SetMaze((int row, int col) cell, MazeDirection direction)
        {
            (int row, int col) next;

            maze[cell.row, cell.col] |= direction;
            switch (direction)
            {
                case (MazeDirection.Right):
                    {
                        maze[cell.row, cell.col + 1] |= MazeDirection.Left;
                        next = (cell.row, cell.col + 1);
                        break;
                    }
                case (MazeDirection.Left):
                    {
                        maze[cell.row, cell.col - 1] |= MazeDirection.Right;
                        next = (cell.row, cell.col - 1);
                        break;
                    }
                case (MazeDirection.Up):
                    {
                        maze[cell.row - 1, cell.col] |= MazeDirection.Down;
                        next = (cell.row - 1, cell.col);
                        break;
                    }
                case (MazeDirection.Down):
                    {
                        maze[cell.row + 1, cell.col] |= MazeDirection.Up;
                        next = (cell.row + 1, cell.col);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException("Invaid direction");
            }

            return next;
        }

        private List<MazeDirection> getAvailableDirections((int row, int col) cell)
        {
            var directions = new List<MazeDirection>();

            if (cell.row < 0 || cell.row >= _rowNumber) throw new ArgumentOutOfRangeException("row is out of bounds");
            if (cell.row > 0 && maze[cell.row - 1, cell.col] == 0) 
                directions.Add(MazeDirection.Up);
            if (cell.row + 1 < _rowNumber && maze[cell.row + 1, cell.col] == 0) 
                directions.Add(MazeDirection.Down);

            if (cell.col < 0 || cell.col >= _colNumber) throw new ArgumentOutOfRangeException("col is out of bounds");
            if (cell.col > 0 && maze[cell.row, cell.col - 1] == 0) 
                directions.Add(MazeDirection.Left);
            if (cell.col + 1 < _colNumber && maze[cell.row, cell.col + 1] == 0) 
                directions.Add(MazeDirection.Right);

            return directions;
        }

        private bool checkIfHasEmptyBoard()
        {
            for (int i = 0; i < _rowNumber; i++)
                for (int j = 0; j < _colNumber; j++)
                {
                    if (maze[i, j] == MazeDirection.Empty)
                        return true;
                }
            return false;
        }

        /// <summary>
        /// Gets random entry point
        /// </summary>
        /// <returns></returns>
        private (int row, int col) getRandomEntryPoint()
        {
            return (0, 0); // randomize this
        }

        /// <summary>
        /// Prepare empty board
        /// </summary>
        /// <returns></returns>
        private void getEmptyBoard()
        {
            maze = new MazeDirection[_rowNumber, _colNumber];

            for(int i=0;i<_rowNumber;i++)
                for (int j = 0; j < _colNumber; j++)
                {
                    maze[i, j] = MazeDirection.Empty;
                }
        }
    }
}
