namespace Application.Services.Algorithms
{
    using Application.Services.Algorithms.Extensions;
    using Application.Services.Algorithms.Interfaces;
    using Application.Services.Interfaces;
    using Application.Services.Models;

    public sealed class BFSAlgorithm(IMazeParserService mazeParser) : IMazeAlgorithm
    {
        public MazeSolution Solve(string mazeString)
        {
            var solution = new MazeSolution();

            var grid = mazeParser.Parse(mazeString);
            var (start, goal) = mazeParser.FindStartAndGoal(grid);

            var queue = new Queue<Coordinate>();
            var cameFrom = new Dictionary<Coordinate, Coordinate>();
            var visited = new HashSet<Coordinate>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.Equals(goal))
                {
                    solution.Path = cameFrom.ReconstructPath(current);
                    solution.IsSolved = true;
                    return solution;
                }

                foreach (var neighbor in current.GetNeighbors(grid))
                {
                    if (!visited.Contains(neighbor) && grid[neighbor.Y, neighbor.X] != 'X')
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        cameFrom[neighbor] = current;
                    }
                }
            }

            solution.IsSolved = false;
            return solution;
        }
    }
}
