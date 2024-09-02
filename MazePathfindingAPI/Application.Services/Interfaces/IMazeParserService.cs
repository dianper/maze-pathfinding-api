namespace Application.Services.Interfaces
{
    using Application.Services.Models;

    public interface IMazeParserService
    {
        char[,] Parse(string mazeString);

        (Coordinate start, Coordinate goal) FindStartAndGoal(char[,] grid);
    }
}
