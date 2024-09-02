namespace Application.Services.UnitTests.Algorithms.Extensions
{
    using Application.Services.Algorithms.Extensions;
    using Application.Services.Models;


    [Trait("Category", "Application.Services.Algorithms.Extensions.Coordinate.UnitTests")]
    public class CoordinateExtensionsTests
    {
        [Fact]
        public void GetHeuristic_WhenCalled_ShouldReturnCorrectHeuristic()
        {
            // Arrange
            var nodeA = new Coordinate(0, 0);
            var nodeB = new Coordinate(3, 4);
            var expectedHeuristic = 7;

            // Act
            var result = nodeA.GetHeuristic(nodeB);

            // Assert
            Assert.Equal(expectedHeuristic, result);
        }

        [Fact]
        public void GetNeighbors_WhenCalled_ShouldReturnCorrectNeighbors()
        {
            // Arrange
            var node = new Coordinate(1, 1);
            var grid = new char[,]
            {
                { '_', '_', '_' },
                { '_', 'X', '_' },
                { '_', '_', '_' }
            };
            var expectedNeighbors = new List<Coordinate>
            {
                new Coordinate(1, 2),
                new Coordinate(2, 1),
                new Coordinate(1, 0),
                new Coordinate(0, 1)
            };

            // Act
            var result = node.GetNeighbors(grid);

            // Assert
            Assert.Equal(expectedNeighbors, result);
        }

        [Fact]
        public void ReconstructPath_WhenCalled_ShouldReturnCorrectPath()
        {
            // Arrange
            var cameFrom = new Dictionary<Coordinate, Coordinate>
            {
                { new Coordinate(1, 1), new Coordinate(1, 0) },
                { new Coordinate(1, 0), new Coordinate(0, 0) },
                { new Coordinate(0, 0), new Coordinate(0, 1) }
            };
            var current = new Coordinate(1, 1);
            var expectedPath = new List<Coordinate>
            {
                new Coordinate(0, 1),
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(1, 1)
            };

            // Act
            var result = cameFrom.ReconstructPath(current);

            // Assert
            Assert.Equal(expectedPath, result);
        }
    }
}
