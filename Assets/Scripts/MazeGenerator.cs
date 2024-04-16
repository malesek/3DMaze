using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // Define variables to control the size and starting position of the maze
    [Range(5, 500)]
    public int mazeWidth = 5, mazeHeight = 5;
    public int startX, startY;

    // Define variable to store maze data
    MazeCell[,] maze;

    // Define a list of possible movement directions
    List<Direction> Directions = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

    // Method to check if a cell is valid
    bool IsCellValid(int x, int y)
    {
        // Check if the cell is within bounds and not visited
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1 || maze[x, y].visited)
            return false;
        else
            return true;
    }

    // Method to generate the maze
    public MazeCell[,] GetMaze()
    {
        // Initialize the maze array
        maze = new MazeCell[mazeWidth, mazeHeight];

        // Create maze cells and populate the maze array
        for (int x = 0; x < mazeWidth; x++)
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = new MazeCell(x, y);
            }

        // Generate maze starting from startX, startY
        GenerateMazeRecursive(startX, startY);

        return maze;
    }

    // Method to get random movement directions
    List<Direction> GetRandomDirections()
    {
        List<Direction> directions = new List<Direction>(Directions);

        List<Direction> randomDirections = new List<Direction>();

        // Shuffle the list of directions
        while (directions.Count > 0)
        {
            int rnd = Random.Range(0, directions.Count);
            randomDirections.Add(directions[rnd]);
            directions.RemoveAt(rnd);
        }

        return randomDirections;
    }

    // Method to break walls between cells
    void BreakWalls(Vector2Int firstCell, Vector2Int secondCell)
    {
        // Determine the relative position of the cells and break the corresponding wall
        if (firstCell.x > secondCell.x)
        {
            maze[firstCell.x, firstCell.y].leftWall = false;
        }
        else if (firstCell.x < secondCell.x)
        {
            maze[secondCell.x, secondCell.y].leftWall = false;
        }
        else if (firstCell.y < secondCell.y)
        {
            maze[firstCell.x, firstCell.y].topWall = false;
        }
        else if (firstCell.y > secondCell.y)
        {
            maze[secondCell.x, secondCell.y].topWall = false;
        }
    }

    // Recursive method to generate the maze
    void GenerateMazeRecursive(int x, int y)
    {
        // Mark the current cell as visited
        maze[x, y].visited = true;

        // Get random directions to explore
        List<Direction> directions = GetRandomDirections();

        // Iterate over random directions
        foreach (Direction dir in directions)
        {
            // Calculate coordinates of the neighboring cell
            int nx = x, ny = y;
            switch (dir)
            {
                case Direction.Up: ny++; break;
                case Direction.Down: ny--; break;
                case Direction.Left: nx--; break;
                case Direction.Right: nx++; break;
            }

            // Check if the neighboring cell is valid and unvisited
            if (IsCellValid(nx, ny) && !maze[nx, ny].visited)
            {
                // Break the wall between the current cell and the neighbor
                BreakWalls(new Vector2Int(x, y), new Vector2Int(nx, ny));

                // Recursive call to generate the neighbor cell
                GenerateMazeRecursive(nx, ny);
            }
        }
    }
}

// Enum to represent movement directions
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

// Class to represent a cell in the maze
public class MazeCell
{
    // Properties of the maze cell
    public bool visited;
    public int x, y;
    public bool topWall;
    public bool leftWall;

    // Property to get the position of the cell
    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }
    }

    // Constructor to initialize the maze cell
    public MazeCell(int x, int y)
    {
        this.x = x;
        this.y = y;

        // Initialize walls and visited status
        topWall = true;
        leftWall = true;
        visited = false;
    }
}
