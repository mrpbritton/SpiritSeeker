using JetBrains.Annotations;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    public float xValue;
    public float zValue;
    public GameObject[] powerUps;

    private float xTiles;
    private float zTiles;

    private void OnEnable()
    {
        xTiles = xValue / 10;
        zTiles = zValue / 10;

        for(int i = 0; i < xTiles; i++)
        {
            for(int j = 0; j < zTiles; j++)
            {
                int random = Random.Range(0, powerUps.Length);
                /* switch (random){
                    case 1:
                        Instantiate(powerUps[0], new Vector3(transform.position.x + (i * 10), 1, transform.position.z + (j * 10)), transform.rotation, this.gameObject.transform);
                        break;
                    case 2:
                        Instantiate(powerUps[1], new Vector3(transform.position.x + (i * 10), 1, transform.position.z + (j * 10)), transform.rotation, this.gameObject.transform);
                        break;
                    case 3:
                        Instantiate(powerUps[2], new Vector3(transform.position.x + (i * 10), 1, transform.position.z + (j * 10)), transform.rotation, this.gameObject.transform);
                        break;
                    case 4:
                        Instantiate(powerUps[3], new Vector3(transform.position.x + (i * 10), 1, transform.position.z + (j * 10)), transform.rotation, this.gameObject.transform);
                        break;
                    default: break;
                } */
                Instantiate(powerUps[random], new Vector3(transform.position.x + (i * 10), 1, transform.position.z + (j * 10)), transform.rotation, this.gameObject.transform);
            }
        }
    }
}
