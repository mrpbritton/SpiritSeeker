using JetBrains.Annotations;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    [SerializeField] private float xValue;
    [SerializeField] private float zValue;
    [SerializeField] private float tileSize;
    [SerializeField] private GameObject[] powerUps;

    private float xTiles;
    private float zTiles;

    private void OnEnable()
    {
        xValue = GetSize();
        zValue = GetSize();

        xTiles = xValue / tileSize;
        zTiles = zValue / tileSize;

        for(int i = 0; i < xTiles; i++)
        {
            for(int j = 0; j < zTiles; j++)
            {
                int random = Random.Range(0, powerUps.Length);                
                Instantiate(powerUps[random], new Vector3(transform.position.x + (i * tileSize), 1, transform.position.z + (j * tileSize)), transform.rotation, this.gameObject.transform);
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
