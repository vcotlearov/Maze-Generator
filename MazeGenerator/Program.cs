using MazeGenLib;
using System;
using System.IO;
using System.Text;

namespace MazeGenerator
{
    class Program
    {
        static int rowNumber = 100, colNumber = 205;
        static void Main(string[] args)
        {
            Maze maze = new Maze(rows: rowNumber, cols: colNumber);
            var d = maze.PrepareMaze();
            DrawMaze(d, rowNumber, colNumber);
            Console.ReadKey();
        }

        private static void DrawMaze(MazeDirection[,] maze, int rows, int cols)
        {
            char[,] textMaze = new char[rows * 3, cols * 3];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cell = maze[i, j];
                    textMaze[i * 3, j * 3] = '#';
                    textMaze[i * 3, j * 3 + 1] = cell.HasFlag(MazeDirection.Up) ? '-' : '#'; // UP
                    textMaze[i * 3, j * 3 + 2] = '#';

                    textMaze[i * 3 + 1, j * 3] = cell.HasFlag(MazeDirection.Left) ? '-' : '#'; // LEFT
                    textMaze[i * 3 + 1, j * 3 + 1] = '-';
                    textMaze[i * 3 + 1, j * 3 + 2] = cell.HasFlag(MazeDirection.Right) ? '-' : '#'; // RIGHT

                    textMaze[i * 3 + 2, j * 3] = '#';
                    textMaze[i * 3 + 2, j * 3 + 1] = cell.HasFlag(MazeDirection.Down) ? '-' : '#'; // DOWN
                    textMaze[i * 3 + 2, j * 3 + 2] = '#';
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows * 3; i++)
            {
                for (int j = 0; j < cols * 3; j++)
                {
                    sb.Append(textMaze[i, j]);
                }
                sb.AppendLine();
            }

            File.WriteAllText(Environment.CurrentDirectory +  @"\maze.txt", sb.ToString());
        }
    }
}
