using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraTransform = transform;
    }
}
