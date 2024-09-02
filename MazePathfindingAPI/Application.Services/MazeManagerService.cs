namespace Application.Services
{
    using Application.Services.Algorithms.Interfaces;
    using Application.Services.Interfaces;
    using Application.Services.Models;
    using System.Collections.Generic;

    public sealed class MazeManagerService(IMazeAlgorithm algorithm) : IMazeManagerService
    {
        private IMazeAlgorithm _algorithm = algorithm;
        private readonly List<MazeResponse> _mazes = [];

        public IEnumerable<MazeResponse> GetAll()
        {
            return _mazes;
        }

        public void SetAlgorithm(IMazeAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public MazeResponse Solve(string mazeString)
        {
            var solution = _algorithm.Solve(mazeString);

            var response = new MazeResponse
            {
                Maze = mazeString,
                Solution = solution,
            };

            _mazes.Add(response);

            return response;
        }

        public IMazeAlgorithm GetAlgorithm()
        {
            return _algorithm;
        }
    }
}
