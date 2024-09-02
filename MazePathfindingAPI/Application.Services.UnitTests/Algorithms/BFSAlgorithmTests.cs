namespace Application.Services.UnitTests.Algorithms
{
    using Application.Services.Algorithms;
    using Application.Services.Models;

    [Trait("Category", "Application.Services.Algorithms.BFS.UnitTests")]
    public class BFSAlgorithmTests
    {
        [Fact]
        public void Solve_WhenMazeIsValid_ShouldReturnCorrectPath()
        {
            // Arrange
            var mazeParserMock = new MazeParserService();
            var algorithm = new BFSAlgorithm(mazeParserMock);
            var mazeString = "S_____\n_XXXX_\n____G_";
            var expectedPath = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(1, 2),
                new Coordinate(2, 2),
                new Coordinate(3, 2),
                new Coordinate(4, 2),
            };

            // Act
            var result = algorithm.Solve(mazeString);

            // Assert
            Assert.True(result.IsSolved);
            Assert.Equal(expectedPath, result.Path);
        }

        [Fact]
        public void Solve_WhenMazeIsInvalid_ShouldReturnEmptyPath()
        {
            // Arrange
            var mazeParserMock = new MazeParserService();
            var algorithm = new BFSAlgorithm(mazeParserMock);
            var mazeString = "S_____\nXXXXXX\n__G___";
            var expectedPath = new List<Coordinate>();

            // Act
            var result = algorithm.Solve(mazeString);

            // Assert
            Assert.False(result.IsSolved);
            Assert.Equal(expectedPath, result.Path);
        }
    }
}
