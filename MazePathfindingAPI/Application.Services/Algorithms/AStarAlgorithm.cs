namespace Application.Services.Algorithms
{
    using Application.Services.Algorithms.Interfaces;
    using Application.Services.Interfaces;
    using Application.Services.Models;

    public sealed class AStarAlgorithm(IMazeParserService mazeParser) : IMazeAlgorithm
    {
        public MazeSolution Solve(string mazeString)
        {
            throw new NotImplementedException();
        }
    }
}
