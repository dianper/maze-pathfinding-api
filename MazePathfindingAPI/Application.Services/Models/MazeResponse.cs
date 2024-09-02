namespace Application.Services.Models
{
    public sealed record MazeResponse
    {
        public required string Maze { get; set; }

        public MazeSolution Solution { get; set; } = new();
    }
}
