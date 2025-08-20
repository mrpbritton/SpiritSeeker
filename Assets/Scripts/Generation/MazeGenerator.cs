using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Tooltip("Z-Axis")][SerializeField] private int width;
    [Tooltip("X-Axis")][SerializeField] private int depth;
    [SerializeField] private int tileSize;
    [SerializeField] private MazeCell cellPrefab;

    [SerializeField] private GameObject objectivePrefab;
    [SerializeField] private bool useWidthForObjective;

    [SerializeField] private Vector2 entranceCoordinates;

    [SerializeField] private int numberOfClearings;
    [SerializeField] private int clearingSize;

    private MazeCell[,] cellGrid;

    private void Start()
    {
        depth = GetSize();
        width = GetSize();

        numberOfClearings = depth / 50;
        clearingSize = depth / 20;

        cellGrid = new MazeCell[width / tileSize, depth / tileSize];

        for (int z = 0; z < width / tileSize; z++)
        {
            for (int x = 0; x < depth / tileSize; x++)
            {
                cellGrid[z, x] = Instantiate(cellPrefab, transform.position + new Vector3(x * tileSize, 0, z * tileSize), Quaternion.identity, transform);
            }
        }

        GenerateMaze(null, cellGrid[0, 0]);

        // Subtract one from the proper coordinates to access the proper cell in the array (arrays are 0-indexed)
        if (((int)entranceCoordinates.x / tileSize) == (width / tileSize))
        {
            DisableEntranceWall(cellGrid[(int)entranceCoordinates.x / tileSize - 1, (int)entranceCoordinates.y / tileSize]);
        }
        else if(((int)entranceCoordinates.y / tileSize) == (depth / tileSize))
        {
            DisableEntranceWall(cellGrid[(int)entranceCoordinates.x / tileSize - 1, (int)entranceCoordinates.y / tileSize - 1]);
        }
        else
        {
            DisableEntranceWall(cellGrid[(int)entranceCoordinates.x / tileSize, (int)entranceCoordinates.y / tileSize]);
        }

            SpawnObjective();
        CreateClearings(numberOfClearings, clearingSize);
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        DisableWalls(previousCell, currentCell);

        MazeCell nextCell;
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while(nextCell != null );
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var UnvisitedCells = GetUnvisitedCells(currentCell);

        return UnvisitedCells.OrderBy(_=> Random.value).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        // Convert world position to local position relative to the maze generator
        Vector3 localPosition = currentCell.transform.position - transform.position;
        
        // Calculate grid coordinates using local position
        int x = Mathf.RoundToInt(localPosition.x / tileSize);
        int z = Mathf.RoundToInt(localPosition.z / tileSize);

        // Check if east cell exists and is unvisited
        if (x + 1 < depth / tileSize)
        {
            var eastCell = cellGrid[z, x + 1];

            if(!eastCell.isVisited)
            {
                yield return eastCell;
            }
        }

        // Check if west cell exists and is unvisited
        if (x - 1 >= 0)
        {
            var westCell = cellGrid[z, x - 1];
            if(!westCell.isVisited)
            {
                yield return westCell;
            }
        }

        // Check if north cell exists and is unvisited
        if (z + 1 < width / tileSize)
        {
            var northCell = cellGrid[z + 1, x];
            if(!northCell.isVisited)
            {
                yield return northCell;
            }
        }

        // Check if south cell exists and is unvisited
        if (z - 1 >= 0)
        {
            var southCell = cellGrid[z - 1, x];
            if(!southCell.isVisited)
            {
                yield return southCell;
            }
        }
    }

    private void DisableWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null) return;
            
        // Convert world positions to local positions
        Vector3 prevLocal = previousCell.transform.position - transform.position;
        Vector3 currLocal = currentCell.transform.position - transform.position;

        // Calculate grid coordinates using local position
        int prevX = Mathf.RoundToInt(prevLocal.x / tileSize);
        int prevZ = Mathf.RoundToInt(prevLocal.z / tileSize);
        int currX = Mathf.RoundToInt(currLocal.x / tileSize);
        int currZ = Mathf.RoundToInt(currLocal.z / tileSize);

        // Compare grid coordinates to determine direction
        if (prevX > currX)
        {
            currentCell.DisableWestWall();
            previousCell.DisableEastWall();
            return;
        }
        if (prevX < currX)
        {
            currentCell.DisableEastWall();
            previousCell.DisableWestWall();
            return;
        }
        if (prevZ < currZ)
        {
            currentCell.DisableSouthWall();
            previousCell.DisableNorthWall();
            return;
        }
        if (prevZ > currZ)
        {
            currentCell.DisableNorthWall();
            previousCell.DisableSouthWall();
            return;
        }
    }

    private void DisableEntranceWall(MazeCell cell)
    {
        // Convert world position to local position relative to the maze generator
        Vector3 localPosition = cell.transform.position - transform.position;

        // Calculate grid coordinates using local position
        int x = Mathf.RoundToInt(localPosition.x / tileSize);
        int z = Mathf.RoundToInt(localPosition.z / tileSize);

        if (x == 0)
        {
            cell.DisableEastWall();
        }
        else if (x == (depth / tileSize) - 1)
        {
            cell.DisableWestWall();
        }
        else if (z == 0)
        {
            cell.DisableSouthWall();
        }
        else if (z == (width / tileSize) - 1)
        {
            cell.DisableNorthWall();
        }
    }

    private void SpawnObjective()
    {
        int adjustedWidth = (width / tileSize) - 1;
        int adjustedDepth = (depth / tileSize) - 1;

        List<(int z, int x)> candidates = new List<(int z, int x)>();

        if (useWidthForObjective)
        {
            for (int z = adjustedWidth / 3; z < adjustedWidth; z++)
            {
                for (int x = 1; x < adjustedDepth; x++)
                {
                    // Exclude border cells
                    if (z > 0 && z < adjustedWidth && x > 0 && x < adjustedDepth)
                    {
                        candidates.Add((z, x));
                    }
                }
            }
        }
        else
        {
            for (int z = 1; z < adjustedWidth; z++)
            {
                for (int x = adjustedDepth / 3; x < adjustedDepth; x++)
                {
                    // Exclude border cells
                    if (z > 0 && z < adjustedWidth && x > 0 && x < adjustedDepth)
                    {
                        candidates.Add((z, x));
                    }
                }
            }
        }                

        if (candidates.Count == 0)
        {
            Debug.LogWarning("No valid cells found for objective placement.");
            return;
        }

        // Pick a random candidate
        var (objZ, objX) = candidates[Random.Range(0, candidates.Count)];
        MazeCell cell = cellGrid[objZ, objX];

        // Instantiate the objective at the cell's position
        Instantiate(objectivePrefab, cell.transform.position, Quaternion.identity, transform);

        // Destroy the maze cell GameObject and remove it from the grid
        Destroy(cell.gameObject);
        cellGrid[objZ, objX] = null;
    }

    private void CreateClearings(int clearingCount = 3, int clearingSize = 6)
    {
        int widthMax = (width / tileSize) - 1;
        int depthMax = (depth / tileSize) - 1;
        System.Random rng = new System.Random();

        for (int c = 0; c < clearingCount; c++)
        {
            // Find a random non-border, non-null cell as the starting point
            List<(int z, int x)> candidates = new List<(int z, int x)>();
            for (int z = 1; z < widthMax; z++)
            {
                for (int x = 1; x < depthMax; x++)
                {
                    if (cellGrid[z, x] != null)
                        candidates.Add((z, x));
                }
            }
            if (candidates.Count == 0) break;

            var (startZ, startX) = candidates[rng.Next(candidates.Count)];

            // Flood fill to select a chunk of adjacent cells
            HashSet<(int z, int x)> toClear = new HashSet<(int z, int x)>();
            Queue<(int z, int x)> queue = new Queue<(int z, int x)>();
            queue.Enqueue((startZ, startX));
            toClear.Add((startZ, startX));

            while (queue.Count > 0 && toClear.Count < clearingSize)
            {
                var (cz, cx) = queue.Dequeue();
                foreach (var (nz, nx) in new[] { (cz + 1, cx), (cz - 1, cx), (cz, cx + 1), (cz, cx - 1) })
                {
                    if (nz > 0 && nz < widthMax && nx > 0 && nx < depthMax && cellGrid[nz, nx] != null && !toClear.Contains((nz, nx)))
                    {
                        queue.Enqueue((nz, nx));
                        toClear.Add((nz, nx));
                        if (toClear.Count >= clearingSize) break;
                    }
                }
            }

            // Destroy and remove the selected cells
            foreach (var (z, x) in toClear)
            {
                Destroy(cellGrid[z, x].gameObject);
                cellGrid[z, x] = null;
            }
        }
    }

    /* private int GetSize()
    {
        if(LevelSettingsData.instance == null)
        {
            return 200;
        }
        switch (LevelSettingsData.instance.MazeSize)
        {
            case 1:
                return 200;
            case 2:
                return 300;
            case 3:
                return 400;
            default:
                return 200;
        }
    } */

    private int GetSize()
    {
        switch (DataManager.mazeSize)
        {
            case 1:
                return 200;
            case 2:
                return 300;
            case 3:
                return 400;
            default:
                return 200;
        }
    }
}
