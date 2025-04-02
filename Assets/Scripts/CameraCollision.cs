using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Camera collision occurred");
    }
}
