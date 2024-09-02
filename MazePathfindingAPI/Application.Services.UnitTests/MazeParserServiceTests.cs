namespace Application.Services.UnitTests
{
    using Application.Services.Exceptions;
    using System;

    [Trait("Category", "Application.Services.MazeParser.UnitTests")]
    public class MazeParserServiceTests
    {
        [Fact]
        public void Parse_WhenMazeExceedsMaximumRows_ThrowsMazeExceedsMaximumRowException()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "S_____\n" +
                "XXX___\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXX_\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "G_____";

            // Act
            var act = () => mazeParserService.Parse(mazeString);

            // Assert
            Assert.Throws<MazeExceedsMaximumRowException>(act);
        }

        [Fact]
        public void Parse_WhenMazeExceedsMaximumColumns_ThrowsMazeExceedsMaximumColumnException()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "S____________________\n" +
                "XXX__________________\n" +
                "XXXXXX_______________\n" +
                "XXXXXX_______________\n" +
                "G____________________";

            // Act
            var act = () => mazeParserService.Parse(mazeString);

            // Assert
            Assert.Throws<MazeExceedsMaximumColumnException>(act);
        }

        [Fact]
        public void Parse_WhenMazeRowsHaveDifferentLengths_ThrowsInvalidMazeParserException()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "S___________________\n" +
                "XXX_____________\n" +
                "XXXXXX______________\n" +
                "XXXXXX____________\n" +
                "G___________________";

            // Act
            var act = () => mazeParserService.Parse(mazeString);

            // Assert
            Assert.Throws<InvalidMazeParserException>(act);
        }

        [Fact]
        public void Parser_WhenMazeIsCorrect_ReturnsMaze()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "S_____\n" +
                "XXX___\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "G_____";

            // Act
            var maze = mazeParserService.Parse(mazeString);

            // Assert
            Assert.Equal('S', maze[0, 0]);
            Assert.Equal('X', maze[1, 0]);
            Assert.Equal('G', maze[4, 0]);
        }

        [Fact]
        public void FindStartAndGoal_WhenMazeContainsMoreThanOneStart_ThrowsInvalidMazeParserException()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "S_____\n" +
                "XXS___\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "G_____";

            // Act
            var maze = mazeParserService.Parse(mazeString);
            Action act = () => mazeParserService.FindStartAndGoal(maze);

            // Assert
            Assert.Throws<InvalidMazeParserException>(act);
        }

        [Fact]
        public void FindStartAndGoal_WhenMazeContainsMoreThanOneGoal_ThrowsInvalidMazeParserException()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "S_____\n" +
                "XXG___\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "G_____";

            // Act
            var maze = mazeParserService.Parse(mazeString);
            Action act = () => mazeParserService.FindStartAndGoal(maze);

            // Assert
            Assert.Throws<InvalidMazeParserException>(act);
        }

        [Fact]
        public void FindStartAndGoal_WhenMazeContainsStartAndGoal_ReturnsStartAndGoal()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "S_____\n" +
                "XX____\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "G_____";

            // Act
            var maze = mazeParserService.Parse(mazeString);
            var (start, goal) = mazeParserService.FindStartAndGoal(maze);

            // Assert
            Assert.Equal(0, start.X);
            Assert.Equal(0, start.Y);
            Assert.Equal(0, goal.X);
            Assert.Equal(4, goal.Y);
        }

        [Fact]
        public void FindStartAndGoal_WhenMazeDoesNotContainStart_ThrowsInvalidMazeParserException()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "______\n" +
                "XX____\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "G_____";

            // Act
            var maze = mazeParserService.Parse(mazeString);
            Action act = () => mazeParserService.FindStartAndGoal(maze);

            // Assert
            Assert.Throws<InvalidMazeParserException>(act);
        }

        [Fact]
        public void FindStartAndGoal_WhenMazeDoesNotContainGoal_ThrowsInvalidMazeParserException()
        {
            // Arrange
            var mazeParserService = new MazeParserService();
            var mazeString =
                "S_____\n" +
                "XX____\n" +
                "XXXXXX\n" +
                "XXXXXX\n" +
                "______";

            // Act
            var maze = mazeParserService.Parse(mazeString);
            Action act = () => mazeParserService.FindStartAndGoal(maze);

            // Assert
            Assert.Throws<InvalidMazeParserException>(act);
        }
    }
}
