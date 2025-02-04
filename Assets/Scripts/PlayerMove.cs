using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementSpeed = 10f;

    private Controls controls;
    private Rigidbody playerRB;
    private CharacterController playerCC;
    private Animator playerAnimator;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        playerRB = GetComponent<Rigidbody>();
        playerCC = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        playerRB.MoveRotation(Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0));

        Vector2 moveDirection = controls.Player.Move.ReadValue<Vector2>();

        // Move forward
        if(moveDirection == new Vector2(0, 1))
        {
            playerAnimator.SetTrigger("StartWalking");

            playerCC.Move(transform.forward * Time.deltaTime * movementSpeed);
        }

        // Move backwards
        if(moveDirection == new Vector2(0, -1))
        {
            playerCC.Move(-transform.forward * Time.deltaTime * movementSpeed);
        }
    }
}
