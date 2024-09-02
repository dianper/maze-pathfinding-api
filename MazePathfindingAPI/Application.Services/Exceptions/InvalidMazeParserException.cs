namespace Application.Services.Exceptions
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class InvalidMazeParserException : Exception
    {
        public InvalidMazeParserException(string message)
            : base(message)
        {
        }

        public InvalidMazeParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
