namespace Application.Services.UnitTests
{
    using Application.Services.Algorithms.Interfaces;
    using Application.Services.Models;
    using Moq;

    [Trait("Category", "Application.Services.MazeManager.UnitTests")]
    public class MazeManagerServiceTests
    {
        [Fact]
        public void Solve_WhenMazeIsSolved_AddsMazeToMazes()
        {
            // Arrange
            var algorithm = new Mock<IMazeAlgorithm>();
            var mazeManagerService = new MazeManagerService(algorithm.Object);
            var mazeString = "S_____\nXXXXX\nG_____";

            var solution = new MazeSolution();
            solution.Path = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
            };

            solution.IsSolved = true;

            algorithm
                .Setup(x => x.Solve(mazeString))
                .Returns(solution);

            // Act
            var response = mazeManagerService.Solve(mazeString);

            // Assert
            Assert.True(response.Solution.IsSolved);
            Assert.Equal(mazeString, response.Maze);
        }

        [Fact]
        public void SetAlgorithm_WhenCalled_SetsAlgorithm()
        {
            // Arrange
            var algorithm = new Mock<IMazeAlgorithm>();
            var mazeManagerService = new MazeManagerService(algorithm.Object);
            var newAlgorithm = new Mock<IMazeAlgorithm>();

            // Act
            mazeManagerService.SetAlgorithm(newAlgorithm.Object);

            // Assert
            Assert.Equal(newAlgorithm.Object, mazeManagerService.GetAlgorithm());
        }

        [Fact]
        public void GetAll_WhenMazesExist_ReturnsMazes()
        {
            // Arrange
            var algorithm = new Mock<IMazeAlgorithm>();
            var mazeManagerService = new MazeManagerService(algorithm.Object);
            var mazeString = "S_____\nXXXXX\nG_____";

            var solution = new MazeSolution();
            solution.Path = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
            };

            solution.IsSolved = true;

            algorithm
                .Setup(x => x.Solve(mazeString))
                .Returns(solution);

            mazeManagerService.Solve(mazeString);

            // Act
            var mazes = mazeManagerService.GetAll();

            // Assert
            Assert.Single(mazes);
        }
    }
}
