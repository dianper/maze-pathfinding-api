namespace Application.Services
{
    using Application.Services.Exceptions;
    using Application.Services.Interfaces;
    using Application.Services.Models;

    public sealed class MazeParserService : IMazeParserService
    {
        private const int MaxRows = 20;
        private const int MaxCols = 20;
        private static readonly char[] separator = ['\n', '\r', ';'];

        public char[,] Parse(string mazeString)
        {
            if (string.IsNullOrWhiteSpace(mazeString))
            {
                throw new InvalidMazeParserException("Maze string cannot be null or empty.");
            }

            var rows = mazeString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            
            if (rows.Length > MaxRows)
            {
                throw new MazeExceedsMaximumRowException(MaxRows);
            }

            var height = rows.Length;
            var width = rows[0].Length;
            
            if (width > MaxCols)
            {
                throw new MazeExceedsMaximumColumnException(MaxCols);
            }

            var grid = new char[height, width];

            for (var y = 0; y < height; y++)
            {
                if (rows[y].Length != width)
                {
                    throw new InvalidMazeParserException("All maze rows must have the same length.");
                }

                for (var x = 0; x < width; x++)
                {
                    grid[y, x] = rows[y][x];
                }
            }

            return grid;
        }

        public (Coordinate start, Coordinate goal) FindStartAndGoal(char[,] grid)
        {
            var start = new Coordinate(-1, -1);
            var goal = new Coordinate(-1, -1);

            for (var y = 0; y < grid.GetLength(0); y++) // GetLength(0) returns the number of rows
            {
                for (var x = 0; x < grid.GetLength(1); x++) // GetLength(1) returns the number of columns
                {
                    if (grid[y, x] == 'S')
                    {
                        if (start.X != -1)
                        {
                            throw new InvalidMazeParserException("Maze must contain exactly one start (S).");
                        }

                        start = new Coordinate(x, y);
                    }

                    if (grid[y, x] == 'G')
                    {
                        if (goal.X != -1)
                        {
                            throw new InvalidMazeParserException("Maze must contain exactly one goal (G).");
                        }

                        goal = new Coordinate(x, y);
                    }
                }
            }
            
            if (start.X == -1 || start.Y == -1)
            {
                throw new InvalidMazeParserException("Maze must contain exactly one start (S).");
            }

            if (goal.X == -1 || goal.Y == -1)
            {
                throw new InvalidMazeParserException("Maze must contain exactly one goal (G).");
            }

            return (start, goal);
        }
    }
}
