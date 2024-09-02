namespace Application.Services.Exceptions
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class MazeExceedsMaximumColumnException : Exception
    {
        public MazeExceedsMaximumColumnException(int maxCols) 
            : base($"Maze exceeds maximum column count of {maxCols}.")
        {
        }

        public MazeExceedsMaximumColumnException(int maxCols, Exception innerException) 
            : base($"Maze exceeds maximum column count of {maxCols}.", innerException)
        {
        }
    }
}
