using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementSpeed = 10f;

    private Controls controls;
    private Rigidbody playerRB;
    private CharacterController playerCC;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        playerRB = GetComponent<Rigidbody>();
        playerCC = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        playerRB.MoveRotation(Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0));

        Vector2 moveDirection = controls.Player.Move.ReadValue<Vector2>();
        playerCC.Move(new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * movementSpeed);
    }
}
