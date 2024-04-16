using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    // Reference to the MazeGenerator script
    [SerializeField] MazeGenerator m_Generator;
    // Prefab for maze cells
    [SerializeField] GameObject MazeCellPrefab;
    // Prefab for point objects
    [SerializeField] GameObject PointPrefab;

    public float CellSize = 1f;

    private void Start()
    {
        // Obtain the maze from MazeGenerator
        MazeCell[,] maze = m_Generator.GetMaze();

        // Loop through each cell in the maze
        for (int x = 0; x < m_Generator.mazeWidth; x++)
        {
            for (int y = 0; y < m_Generator.mazeHeight; y++)
            {
                // Instantiate a maze cell prefab at the appropriate position
                GameObject newCell = Instantiate(MazeCellPrefab, new Vector3(x * CellSize, 0f, y * CellSize), Quaternion.identity, transform);

                // Get MazeCellObject component from the instantiated cell
                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

                // Determine wall configuration for the current cell
                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;
                bool right = false;
                bool bottom = false;

                // Check if the cell is at the maze boundary and adjust wall configuration accordingly
                if (x == m_Generator.mazeWidth - 1) right = true;
                if (y == 0) bottom = true;

                // Initialize the maze cell with wall configuration
                mazeCell.Init(top, bottom, right, left);

                //// Randomly instantiate a point object in the cell
                //if (Random.value < 0.1f)  // 10% chance to spawn a point in each cell
                //{
                //    Instantiate(PointPrefab, new Vector3(x * CellSize, 0.5f, y * CellSize), Quaternion.identity, transform);
                //}
            }
        }
    }
}
