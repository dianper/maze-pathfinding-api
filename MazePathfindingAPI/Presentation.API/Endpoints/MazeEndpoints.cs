namespace Presentation.API.Endpoints
{
    using Application.Services.Algorithms.Interfaces;
    using Application.Services.Exceptions;
    using Application.Services.Interfaces;

    public static class MazeEndpoints
    {
        public static IResult Get(IMazeManagerService mazeManager)
        {
            var mazes = mazeManager.GetAll();

            return Results.Ok(mazes);
        }

        public static IResult Solve(
            string mazeString,
            string algorithmType,
            IMazeManagerService mazeManager,
            IServiceProvider serviceProvider)
        {
            try
            {
                var algorithm = algorithmType.ToLower() switch
                {
                    "bfs" => serviceProvider.GetRequiredKeyedService<IMazeAlgorithm>("bfs"),
                    "astar" => serviceProvider.GetRequiredKeyedService<IMazeAlgorithm>("astar"),
                    _ => throw new Exception("Unsupported algorithm.")
                };

                mazeManager.SetAlgorithm(algorithm);

                var response = mazeManager.Solve(mazeString);

                if (!response.Solution.IsSolved)
                {
                    return Results.BadRequest("No solution found.");
                }

                return Results.Ok(response);
            }
            catch (InvalidMazeParserException ex)
            {
                // Return a BadRequest if the maze is invalid
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                return Results.Problem($"Internal server error: {ex.Message}");
            }
        }
    }
}
