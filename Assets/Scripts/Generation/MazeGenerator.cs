using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Tooltip("Z-Axis")][SerializeField] private int width;
    [Tooltip("X-Axis")][SerializeField] private int depth;
    [SerializeField] private int tileSize = 10;

    [SerializeField] private MazeCell cellPrefab;

    private MazeCell[,] cellGrid;

    private void Start()
    {
        cellGrid = new MazeCell[width / tileSize, depth / tileSize];

        for (int z = 0; z < width / tileSize; z++)
        {
            for (int x = 0; x < depth / tileSize; x++)
            {
                cellGrid[z, x] = Instantiate(cellPrefab, transform.position + new Vector3(x * tileSize, 0, z * tileSize), Quaternion.identity, transform);
            }
        }

        GenerateMaze(null, cellGrid[0, 0]);
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
            Debug.Log("cellGrid[ " + z + " ] , [ " + (x + 1) + " ]");
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

        // Calculate grid coordinates
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
}
