# Maze Pathfinding API

## Overview

The Maze Pathfinding API is a .NET 8 application designed to solve mazes using the Breadth-First Search (BFS) algorithm. It provides endpoints for retrieving previously submitted mazes and solving new mazes.

## Features

- **Maze Solving**: Uses BFS to find a possible solution to a given maze.
- **Maze Retrieval**: Retrieve a list of all previously submitted mazes and their solutions.
- **Swagger Documentation**: Interactive API documentation for easy testing and exploration.
- **Unit Testing**: Includes xUnit tests to validate API functionality.

## Endpoints

### 1. `GET /api/maze/mazes`

Retrieve a list of all previously submitted mazes and their solutions.

**Response:**

- **200 OK**: Returns a JSON array of `MazeResponse` objects. Each `MazeResponse` contains the maze configuration and its solution.

**Example Request:**

```http
GET /api/maze/mazes
```

**Example Response:**

```json
[
    {
        "maze": "S___G;XXXX_;_____;",
        "solution": {
            "path": [
                {"x": 0, "y": 0},
                {"x": 0, "y": 1},
                {"x": 0, "y": 2},
                {"x": 1, "y": 2},
                {"x": 2, "y": 2},
                {"x": 2, "y": 3},
                {"x": 2, "y": 4}
            ],
            "isSolved": true
        }
    }
]
```

### 2. `POST /api/maze/solve`

Submit a new maze configuration and receive a `MazeResponse` with the maze and its solution, or an error if no solution exists.

**Request Body:**

- **maze**: A string representation of the maze where:
  - `S` denotes the start point.
  - `G` denotes the goal point.
  - `_` denotes an empty space.
  - `X` denotes a wall.
  - Each row is separated by a semicolon (`;`).

**Response:**

- **200 OK**: Returns a `MazeResponse` object containing the submitted maze and its solution.
- **400 Bad Request**: Returns an error if the maze configuration is invalid or unsolvable.

**Example Request:**

```http
POST /api/maze/solve
Content-Type: application/json

{
    "mazeString": "S___G;XXXX_;_____;",
    "algorithmType": "bfs"
}
```

**Example Response:**

```json
{
    "maze": "S___G;XXXX_;_____;",
    "solution": {
        "path": [
            {"x": 0, "y": 0},
            {"x": 0, "y": 1},
            {"x": 0, "y": 2},
            {"x": 1, "y": 2},
            {"x": 2, "y": 2},
            {"x": 2, "y": 3},
            {"x": 2, "y": 4}
        ],
        "isSolved": true
    }
}
```

## BFS Algorithm

**Breadth-First Search (BFS)** is a fundamental graph traversal algorithm used to explore nodes and edges in a graph. BFS is particularly useful for finding the shortest path in unweighted graphs or grids, such as mazes.

### How BFS Works

1. **Initialization**: Start at the source node (the maze start point) and initialize a queue with this node. Mark it as visited.
2. **Exploration**: While there are nodes in the queue:
   - Dequeue the front node.
   - Explore its neighbors (adjacent nodes).
   - If a neighbor is the target node (the end point), return the path from start to end.
   - Otherwise, mark the neighbor as visited and enqueue it for further exploration.
3. **Termination**: If the queue is empty and the target node hasn't been reached, the maze is unsolvable.

BFS explores nodes level by level, ensuring that the shortest path is found.

## A* Algorithm (Planned)

The A* algorithm is an advanced pathfinding algorithm that combines BFS with heuristic-based search. It uses a cost function to estimate the shortest path more efficiently in complex mazes.

### Planned Features for A* Algorithm

- **Heuristic Function**: Estimates the cost to reach the goal node from the current node.
- **Priority Queue**: Prioritizes nodes with the lowest estimated total cost.
- **Efficiency**: Generally faster than BFS for large mazes due to heuristic-based optimization.

## Testing

The project includes xUnit tests to ensure the functionality and correctness of the API. To run the tests:

```bash
dotnet test
```

## Setup and Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/dianper/maze-pathfinding-api.git
    ```

2. **Navigate to the project directory:**

    ```bash
    cd maze-pathfinding-api
    ```

3. **Restore dependencies:**

    ```bash
    dotnet restore
    ```

4. **Run the application:**

    ```bash
    dotnet run
    ```

5. **Access Swagger Documentation:**

Open your browser and navigate to `https://localhost:7073/swagger/index.html` to explore and test the API.