using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerMove : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementSpeed = 10f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;

    private Controls controls;
    private CharacterController playerCC;
    private Animator playerAnimator;
    private Vector3 moveDirection;
    private Vector3 downForce;
    private bool isGrounded = true;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        playerCC = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Read the direction of the movement input
        Vector2 inputDirection = controls.Player.Move.ReadValue<Vector2>();

        // Move forward
        if(inputDirection.y == 1)
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkForward"))
            {
                playerAnimator.Play("WalkForward");
            }
            moveDirection = transform.forward;
        }

        // Move backwards
        if(inputDirection.y == -1)
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkBackward"))
            {
                playerAnimator.Play("WalkBackward");
            }
            moveDirection = -transform.forward;
        }

        // Move right forward
        if(inputDirection.x > 0)
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkForwardRight"))
            {
                playerAnimator.Play("WalkForwardRight");
            }
            moveDirection = transform.right;
        }

        // Move left forward
        if(inputDirection.x < 0)
        {

            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkForwardLeft"))
            {
                playerAnimator.Play("WalkForwardLeft");
            }
            moveDirection = -transform.right;
        }

        // Not walking
        if(inputDirection == Vector2.zero)
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerAnimator.Play("Idle");
            }
            moveDirection = Vector3.zero;
        }

        // Apply movement
        playerCC.Move(moveDirection * Time.deltaTime * movementSpeed);

        // Keep downForce at 0 because otherwise you're accumulating a downForce.y that the jumpHeight can't overcome
        if(isGrounded && downForce.y < 0)
        {
            downForce.y = 0;
        }

        // Jump
        if (controls.Player.Jump.triggered && isGrounded)
        {
            downForce.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        downForce.y += gravity * Time.deltaTime;
        playerCC.Move(downForce * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), new Vector3(0, -1, 0), out hit, 0.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Rotate with the camera
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0), Time.deltaTime * rotationSpeed);
    }
}
