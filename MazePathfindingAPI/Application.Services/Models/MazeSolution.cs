namespace Application.Services.Models
{
    public sealed record MazeSolution
    {
        public List<Coordinate> Path { get; set; } = [];

        public bool IsSolved { get; set; }

        public MazeSolution()
        {
        }

        public MazeSolution(List<Coordinate> path, bool isSolved)
        {
            Path = path;
            IsSolved = isSolved;
        }
    }
}
