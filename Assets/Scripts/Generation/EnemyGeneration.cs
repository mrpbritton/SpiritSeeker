using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [Tooltip("Z-Axis")][SerializeField] private int width;
    [Tooltip("X-Axis")][SerializeField] private int depth;
    [SerializeField] private int tileSize;
    [Tooltip("The chance of an enemy squad spawning on a given tile will be 1 in (this number)")] public float spawnProbability = 11;

    private void Start()
    {
        depth = GetSize();
        width = GetSize();
        spawnProbability = GetEnemyQuantity();

        for (int z = 0; z < width / tileSize; z++)
        {
            for (int x = 0; x < depth / tileSize; x++)
            {
                if ((int)Random.Range(1, spawnProbability) == 1)
                {
                    spawnEnemies(new Vector3(transform.position.x + (x * tileSize), 0, transform.position.z + (z * tileSize)));
                }
            }
        }
    }
    public void spawnEnemies(Vector3 position)
    {
        int randomNumber = Random.Range(1, enemies.Length);
        GameObject enemy1 = Instantiate(enemies[randomNumber], new Vector3(position.x + (tileSize / 5), position.y + 1, position.z + (tileSize / 5)), transform.rotation, transform);
        // enemy1.SetActive(false);
        GameObject enemy2 = Instantiate(enemies[randomNumber], new Vector3(position.x + (tileSize / 5), position.y + 1, position.z - (tileSize / 5)), transform.rotation, transform);
        // enemy2.SetActive(false);
        GameObject enemy3 = Instantiate(enemies[randomNumber], new Vector3(position.x - (tileSize / 5), position.y + 1, position.z + (tileSize / 5)), transform.rotation, transform);
        // enemy3.SetActive(false);
        GameObject enemy4 = Instantiate(enemies[randomNumber], new Vector3(position.x - (tileSize / 5), position.y + 1, position.z - (tileSize / 5)), transform.rotation, transform);
        // enemy4.SetActive(false);
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

    /*private int GetEnemyQuantity()
    {
        if (LevelSettingsData.instance == null)
        {
            return 11;
        }
        switch (LevelSettingsData.instance.EnemyQuantity)
        {
            case 1:
                return 31;
            case 2:
                return 21;
            case 3:
                return 11;
            default:
                return 31;
        }
    }*/

    private int GetEnemyQuantity()
    {
        switch (DataManager.enemySpawnRate)
        {
            case 1:
                return 31;
            case 2:
                return 21;
            case 3:
                return 11;
            default:
                return 31;
        }
    }
}
