using MazeGenLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MazeGenerator
{
    class Program
    {
        static int rowNumber = 10000, colNumber = 10000;
        static void Main(string[] args)
        {
            var timer = Stopwatch.StartNew();
            Maze maze = new Maze(rows: rowNumber, cols: colNumber);
            var d = maze.PrepareMaze();
            timer.Stop();

            //DrawMaze(d, rowNumber, colNumber);
            
            Console.WriteLine($"All done. Your time: {timer.Elapsed}");
            Console.ReadKey();
        }

        private static void DrawMaze(MazeDirection[,] maze, int rows, int cols)
        {
            char[,] textMaze = new char[3, cols * 3];

            var path = Environment.CurrentDirectory + @"\maze.txt";
            using (var stream = File.Create(path))
            {
                StringBuilder upperRow = new StringBuilder();
                StringBuilder middleRow = new StringBuilder();
                StringBuilder lowerRow = new StringBuilder();

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        var cell = maze[i, j];

                        upperRow.Append('#');
                        upperRow.Append(cell.HasFlag(MazeDirection.Up) ? '-' : '#');
                        upperRow.Append('#');

                        middleRow.Append(cell.HasFlag(MazeDirection.Left) ? '-' : '#');
                        middleRow.Append('-');
                        middleRow.Append(cell.HasFlag(MazeDirection.Right) ? '-' : '#');

                        lowerRow.Append('#');
                        lowerRow.Append(cell.HasFlag(MazeDirection.Down) ? '-' : '#');
                        lowerRow.Append('#');
                    }

                    Byte[] text = new UTF8Encoding(true).GetBytes(upperRow.AppendLine().ToString());
                    stream.Write(text, 0, text.Length);

                    text = new UTF8Encoding(true).GetBytes(middleRow.AppendLine().ToString());
                    stream.Write(text, 0, text.Length);

                    text = new UTF8Encoding(true).GetBytes(lowerRow.AppendLine().ToString());
                    stream.Write(text, 0, text.Length);

                    upperRow.Clear();
                    middleRow.Clear();
                    lowerRow.Clear();
                }
            }
        }
    }
}
