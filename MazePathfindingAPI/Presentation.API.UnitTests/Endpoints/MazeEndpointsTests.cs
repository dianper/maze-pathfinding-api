namespace Presentation.API.UnitTests.Endpoints
{
    using Application.Services;
    using Application.Services.Algorithms;
    using Application.Services.Algorithms.Interfaces;
    using Application.Services.Exceptions;
    using Application.Services.Interfaces;
    using Application.Services.Models;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Presentation.API.Endpoints;
    using System.Collections.Generic;

    [Trait("Category", "Presentation.API.Endpoints.Maze.UnitTests")]
    public class MazeEndpointsTests
    {
        [Fact]
        public void Get_WhenCalled_ShouldReturnAllMazes()
        {
            // Arrange
            var mazeManagerMock = new Mock<IMazeManagerService>();
            var mazesResponse = new List<MazeResponse>
            {
                new()
                {
                    Maze = "S_____\n_XXXX_\n____G_",
                    Solution = new()
                    {
                        IsSolved = true
                    }
                },
                new()
                {
                    Maze = "S_____\n_XXXXX\nG_____",
                    Solution = new()
                    {
                        IsSolved = true
                    }
                }
            };

            mazeManagerMock
                .Setup(x => x.GetAll())
                .Returns(mazesResponse);

            // Act
            var result = MazeEndpoints.Get(mazeManagerMock.Object);
            var resultOk = result as Ok<IEnumerable<MazeResponse>>;

            // Assert
            Assert.NotNull(resultOk);
            Assert.Equal(200, resultOk.StatusCode);
            Assert.Equal(mazesResponse, resultOk.Value);
        }

        [Fact]
        public void Solve_WhenMazeIsValid_ShouldReturnOk()
        {
            // Arrange
            var mazeString = "S_____\n_XXXX_\n____G_";
            var algorithmType = "bfs";
            var mazeResponse = new MazeResponse
            {
                Maze = mazeString,
                Solution = new()
                {
                    IsSolved = true
                }
            };

            var mazeManagerMock = new Mock<IMazeManagerService>();
            mazeManagerMock
                .Setup(x => x.Solve(mazeString))
                .Returns(mazeResponse);

            var sp = SetupServiceProvider(mazeManagerMock.Object);
            var mazeManager = sp.GetRequiredService<IMazeManagerService>();

            // Act
            var result = MazeEndpoints.Solve(mazeString, algorithmType, mazeManager, sp);
            var resultOk = result as Ok<MazeResponse>;

            // Assert
            Assert.NotNull(resultOk);
            Assert.Equal(200, resultOk.StatusCode);
            Assert.Equal(mazeResponse, resultOk.Value);
        }

        [Fact]
        public void Solve_WhenMazeIsInValid_ShouldReturnBadRequest()
        {
            // Arrange
            var mazeString = "S_____\nXXXXXX\n____G_";
            var algorithmType = "bfs";
            var mazeResponse = new MazeResponse
            {
                Maze = mazeString,
                Solution = new()
                {
                    IsSolved = false
                }
            };

            var mazeManagerMock = new Mock<IMazeManagerService>();
            mazeManagerMock
                .Setup(x => x.Solve(mazeString))
                .Returns(mazeResponse);

            var sp = SetupServiceProvider(mazeManagerMock.Object);
            var mazeManager = sp.GetRequiredService<IMazeManagerService>();

            // Act
            var result = MazeEndpoints.Solve(mazeString, algorithmType, mazeManager, sp);
            var resultBadRequest = result as BadRequest<string>;

            // Assert
            Assert.NotNull(resultBadRequest);
            Assert.Equal(400, resultBadRequest.StatusCode);
            Assert.Equal("No solution found.", resultBadRequest.Value);
        }

        [Fact]
        public void Solve_WhenParserThrowsParserException_ShouldReturnBadRequest()
        {
            // Arrange
            var mazeString = "S_____\nXXXXXX\n____G_";
            var algorithmType = "bfs";
            var mazeResponse = new MazeResponse
            {
                Maze = mazeString,
                Solution = new()
                {
                    IsSolved = false
                }
            };

            var mazeManagerMock = new Mock<IMazeManagerService>();
            mazeManagerMock
                .Setup(x => x.Solve(mazeString))
                .Throws(new InvalidMazeParserException("error"));

            var sp = SetupServiceProvider(mazeManagerMock.Object);
            var mazeManager = sp.GetRequiredService<IMazeManagerService>();

            // Act
            var result = MazeEndpoints.Solve(mazeString, algorithmType, mazeManager, sp);
            var resultBadRequest = result as BadRequest<string>;

            // Assert
            Assert.NotNull(resultBadRequest);
            Assert.Equal(400, resultBadRequest.StatusCode);
            Assert.Equal("error", resultBadRequest.Value);
        }

        [Fact]
        public void Solve_WhenParserThrowsAnyException_ShouldReturnInternalServerError()
        {
            // Arrange
            var mazeString = "S_____\nXXXXXX\n____G_";
            var algorithmType = "bfs";
            var mazeResponse = new MazeResponse
            {
                Maze = mazeString,
                Solution = new()
                {
                    IsSolved = false
                }
            };

            var mazeManagerMock = new Mock<IMazeManagerService>();
            mazeManagerMock
                .Setup(x => x.Solve(mazeString))
                .Throws(new Exception("error"));

            var sp = SetupServiceProvider(mazeManagerMock.Object);
            var mazeManager = sp.GetRequiredService<IMazeManagerService>();

            // Act
            var result = MazeEndpoints.Solve(mazeString, algorithmType, mazeManager, sp);
            var resultInternalServerError = result as ProblemHttpResult;

            // Assert
            Assert.NotNull(resultInternalServerError);
            Assert.Equal(500, resultInternalServerError.StatusCode);
            Assert.NotNull(resultInternalServerError.ProblemDetails);
            Assert.NotNull(resultInternalServerError.ProblemDetails.Detail);            
            Assert.Contains("Internal server error", resultInternalServerError.ProblemDetails.Detail);
        }

        private IServiceProvider SetupServiceProvider(IMazeManagerService mazeManager)
        {
            var services = new ServiceCollection();

            services
                .AddSingleton<IMazeParserService, MazeParserService>()
                .AddKeyedSingleton<IMazeAlgorithm, BFSAlgorithm>("bfs")
                .AddKeyedSingleton<IMazeAlgorithm, AStarAlgorithm>("astar")
                .AddSingleton(mazeManager);

            return services.BuildServiceProvider();
        }
    }
}
