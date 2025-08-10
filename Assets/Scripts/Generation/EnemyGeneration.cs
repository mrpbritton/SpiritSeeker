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
        for(int z = 0; z < width / tileSize; z++)
        {
            for (int x = 0; x < depth / tileSize; x++)
            {
                Debug.Log("For loop entered");
                if ((int)Random.Range(1, spawnProbability) == 1)
                {
                    Debug.Log("Spawn enemies called);");
                    spawnEnemies(new Vector3(transform.position.x + (z * tileSize), 0, transform.position.z + (x * tileSize)));
                }
            }
        }
    }
    public void spawnEnemies(Vector3 position)
    {
        int randomNumber = Random.Range(1, enemies.Length);
        Instantiate(enemies[randomNumber], new Vector3(position.x + (tileSize / 5), position.y + 1, position.z + (tileSize / 5)), transform.rotation);
        Instantiate(enemies[randomNumber], new Vector3(position.x + (tileSize / 5), position.y + 1, position.z - (tileSize / 5)), transform.rotation);
        Instantiate(enemies[randomNumber], new Vector3(position.x - (tileSize / 5), position.y + 1, position.z + (tileSize / 5)), transform.rotation);
        Instantiate(enemies[randomNumber], new Vector3(position.x - (tileSize / 5), position.y + 1, position.z - (tileSize / 5)), transform.rotation);
    }
}
