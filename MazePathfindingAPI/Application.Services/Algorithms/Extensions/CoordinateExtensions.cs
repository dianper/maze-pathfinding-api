namespace Application.Services.Algorithms.Extensions
{
    using Application.Services.Models;
    using System.Collections.Generic;

    public static class CoordinateExtensions
    {
        private readonly static (int, int)[] Directions =
        {
            (0, 1),   // Right
            (1, 0),   // Down
            (0, -1),  // Left
            (-1, 0)   // Up
        };

        public static int GetHeuristic(this Coordinate nodeA, Coordinate nodeB)
        {
            return Math.Abs(nodeA.X - nodeB.X) + Math.Abs(nodeA.Y - nodeB.Y);
        }

        public static IEnumerable<Coordinate> GetNeighbors(this Coordinate node, char[,] grid)
        {
            foreach (var (dx, dy) in Directions)
            {
                var newX = node.X + dx;
                var newY = node.Y + dy;

                var neighbor = new Coordinate(newX, newY);

                if (IsValidMove(neighbor, grid))
                {
                    yield return neighbor;
                }
            }
        }

        public static List<Coordinate> ReconstructPath(this Dictionary<Coordinate, Coordinate> cameFrom, Coordinate current)
        {
            var path = new List<Coordinate> { current };

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            path.Reverse();
            return path;
        }

        private static bool IsValidMove(Coordinate coord, char[,] maze)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            return coord.Y >= 0 && coord.Y < rows && coord.X >= 0 && coord.X < cols && maze[coord.Y, coord.X] != 'X';
        }
    }
}
