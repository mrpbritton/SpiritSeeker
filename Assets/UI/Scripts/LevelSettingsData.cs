using UnityEngine;

public class LevelSettingsData : MonoBehaviour
{
    private int mazeSize;
    public int MazeSize{ get => mazeSize; set => mazeSize = value; }

    private int enemyQuantity;
    public int EnemyQuantity{ get => enemyQuantity; set => enemyQuantity = value; }

    public static LevelSettingsData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
