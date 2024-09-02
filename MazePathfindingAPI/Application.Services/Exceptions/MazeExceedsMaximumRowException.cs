namespace Application.Services.Exceptions
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class MazeExceedsMaximumRowException : Exception
    {
        public MazeExceedsMaximumRowException(int maxRow)
            : base($"Maze exceeds the maximum row count of {maxRow}.")
        {
        }

        public MazeExceedsMaximumRowException(int maxRow, Exception innerException)
            : base($"Maze exceeds the maximum row count of {maxRow}.", innerException)
        {
        }
    }
}
