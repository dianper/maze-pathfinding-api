using Application.Services;
using Application.Services.Algorithms;
using Application.Services.Algorithms.Interfaces;
using Application.Services.Interfaces;
using Microsoft.OpenApi.Models;
using Presentation.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMazeParserService, MazeParserService>();
builder.Services.AddKeyedSingleton<IMazeAlgorithm, BFSAlgorithm>("bfs");
builder.Services.AddKeyedSingleton<IMazeAlgorithm, AStarAlgorithm>("astar");
builder.Services.AddSingleton<IMazeManagerService>(sp =>
{
    return new MazeManagerService(sp.GetRequiredKeyedService<IMazeAlgorithm>("bfs")); // Default to BFS
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Maze Pathfinding API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/maze/mazes", MazeEndpoints.Get).WithOpenApi();

app.MapPost("/api/maze/solve", MazeEndpoints.Solve).WithOpenApi();

app.Run();
