namespace Application.Services.UnitTests.Algorithms
{
    using Application.Services.Algorithms;
    using Application.Services.Interfaces;
    using Moq;
    using System;

    [Trait("Category", "Application.Services.Algorithms.AStar.UnitTests")]
    public class AStarAlgorithmTests
    {
        [Fact]
        public void Solve_NotImplemented()
        {
            // Arrange
            var mazeParserMock = new Mock<IMazeParserService>();
            var algorithm = new AStarAlgorithm(mazeParserMock.Object);
            var mazeString = "S  X\nX  G";

            // Act
            Action act = () => algorithm.Solve(mazeString);

            // Assert
            Assert.Throws<NotImplementedException>(act);
        }
    }
}
