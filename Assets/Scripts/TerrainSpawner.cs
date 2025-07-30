using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject terrainPiece;

    void Start()
    {
        Instantiate(terrainPiece, new Vector3(0,0,0), transform.rotation);
    }
}
