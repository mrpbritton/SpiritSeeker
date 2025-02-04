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

    private void Update()
    {
        Vector2 moveDirection = controls.Player.Move.ReadValue<Vector2>();

        // Move forward
        if(moveDirection == new Vector2(0, 1))
        {
            playerAnimator.Play("WalkForward");

            playerCC.Move(transform.forward * Time.deltaTime * movementSpeed);
        }

        // Move backwards
        if(moveDirection == new Vector2(0, -1))
        {
            playerAnimator.Play("WalkBackward");

            playerCC.Move(-transform.forward * Time.deltaTime * movementSpeed);
        }

        // Move right forward
        if(moveDirection.x > 0)
        {
            playerAnimator.Play("WalkForwardRight");

            playerCC.Move(transform.right * Time.deltaTime * movementSpeed);
        }

        // Move left forward
        if(moveDirection.x < 0)
        {
            playerAnimator.Play("WalkForwardLeft");

            playerCC.Move(-transform.right * Time.deltaTime * movementSpeed);
        }

        // Not walking
        if(moveDirection == Vector2.zero)
        {
            playerAnimator.Play("Idle");
        }
    }

    private void FixedUpdate()
    {
        playerRB.MoveRotation(Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0));
    }
}
