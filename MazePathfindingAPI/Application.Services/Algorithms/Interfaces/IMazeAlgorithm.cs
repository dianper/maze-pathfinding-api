namespace Application.Services.Algorithms.Interfaces
{
    using Application.Services.Models;

    public interface IMazeAlgorithm
    {
        MazeSolution Solve(string mazeString);
    }
}
