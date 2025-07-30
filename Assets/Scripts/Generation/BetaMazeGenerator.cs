using Unity.VisualScripting;
using UnityEngine;

public class BetaMazeGenerator : MonoBehaviour
{
    public struct Coordinates
    {
        public int x;
        public int z;
    }

    [Tooltip("How far the maze extends in the x direction. The number of tiles in that direction will be (this number) divided by 10.")] public float xValue;
    [Tooltip("How far the maze extends in the z direction. The number of tiles in that direction will be (this number) divided by 10.")] public float zValue;
    public GameObject[] mazePieces;
    public GameObject goalPrefab;
    public GameObject[] enemies;
    [Tooltip("The chance of an enemy squad spawning on a given tile will be 1 in (this number)")] public float spawnProbability = 11;

    private int xTiles;
    private int zTiles;
    private Coordinates[] occupiedCoordinates;

    private void OnEnable()
    {
        xTiles = Mathf.RoundToInt(xValue / 10);
        zTiles = Mathf.RoundToInt(zValue / 10);

        int midpoint = Mathf.RoundToInt(zTiles / 2);

        int goalXCoord = Random.Range(1, (xTiles - 1) / 2);
        int goalZCoord = Random.Range(1, (zTiles - 1));

        for (int i = 0; i < xTiles; i++)
        {
            for (int j = 0; j < zTiles; j++)
            {
                // Ensure corner walls
                if (i == 0 && j == 0)
                {
                    Instantiate(mazePieces[0], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), transform.rotation, this.gameObject.transform);

                    spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                }
                else if (i == 0 && j == zTiles - 1)
                {
                    Instantiate(mazePieces[0], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), Quaternion.Euler(0, 90, 0), this.gameObject.transform);

                    spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                }
                else if (i == xTiles - 1 && j == 0)
                {
                    Instantiate(mazePieces[0], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), Quaternion.Euler(0, 270, 0), this.gameObject.transform);

                    spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                }
                else if (i == xTiles - 1 && j == zTiles - 1)
                {
                    Instantiate(mazePieces[0], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), Quaternion.Euler(0, 180, 0), this.gameObject.transform);

                    spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                }
                // Ensure side walls
                else if (i == 0)
                {
                    Instantiate(mazePieces[1], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), Quaternion.Euler(0, 270, 0), this.gameObject.transform);

                    spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                }
                else if (i == xTiles - 1)
                {
                    // Leaves one opening for an entrance
                    if(j != midpoint)
                    {
                        Instantiate(mazePieces[1], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), Quaternion.Euler(0, 90, 0), this.gameObject.transform);

                        spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                    }
                }
                else if (j == 0)
                {
                    Instantiate(mazePieces[1], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), Quaternion.Euler(0, 180, 0), this.gameObject.transform);

                    spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                }
                else if (j == zTiles - 1)
                {
                    Instantiate(mazePieces[1], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), transform.rotation, this.gameObject.transform);

                    spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                }
                // Fill the middle
                else
                {
                    // Spawn goal
                    if(i == goalXCoord && j == goalZCoord)
                    {
                        Instantiate(goalPrefab, new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), transform.rotation, this.gameObject.transform);
                    }
                    else
                    {
                        int random = Random.Range(1, 5);
                        Quaternion prefabRotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                        switch (random)
                        {
                            case 1:
                                Instantiate(mazePieces[1], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), prefabRotation, this.gameObject.transform);
                                spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                                break;
                            case 2:
                                Instantiate(mazePieces[2], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), prefabRotation, this.gameObject.transform);
                                spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                                break;
                            case 3:
                                Instantiate(mazePieces[3], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), prefabRotation, this.gameObject.transform);
                                spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                                break;
                            case 4:
                                Instantiate(mazePieces[4], new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)), prefabRotation, this.gameObject.transform);
                                spawnEnemies(new Vector3(transform.position.x + (i * 10), 0, transform.position.z + (j * 10)));
                                break;
                            default: break;
                        }
                    }
                }
            }
        }
    }

    public bool checkIfCoordsMatch(int xValue, int zValue, Coordinates coordinatesInList)
    {
        if(xValue == coordinatesInList.x && zValue == coordinatesInList.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool spawnChance()
    {
        float randomFloat = Random.Range(1f, spawnProbability);
        int randomInt = Mathf.RoundToInt(randomFloat);
        if(randomInt == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void spawnEnemies(Vector3 position)
    {
        if (spawnChance())
        {
            int randomNumber = Random.Range(1, enemies.Length);
            Instantiate(enemies[randomNumber], new Vector3(position.x + 2, position.y + 1, position.z + 2), transform.rotation);
            Instantiate(enemies[randomNumber], new Vector3(position.x + 2, position.y + 1, position.z - 2), transform.rotation);
            Instantiate(enemies[randomNumber], new Vector3(position.x - 2, position.y + 1, position.z + 2), transform.rotation);
            Instantiate(enemies[randomNumber], new Vector3(position.x - 2, position.y + 1, position.z - 2), transform.rotation);
        }
    }
}
