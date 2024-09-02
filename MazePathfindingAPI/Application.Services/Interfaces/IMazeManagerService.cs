namespace Application.Services.Interfaces
{
    using Application.Services.Algorithms.Interfaces;
    using Application.Services.Models;

    public interface IMazeManagerService
    {
        MazeResponse Solve(string mazeString);

        IEnumerable<MazeResponse> GetAll();

        void SetAlgorithm(IMazeAlgorithm algorithm);

        IMazeAlgorithm GetAlgorithm();
    }
}
