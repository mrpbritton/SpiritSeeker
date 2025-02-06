using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementSpeed = 10f;
    public float rotationSpeed = 10f;
    public float gravity = 9.81f;
    public float jumpHeight = 10f;

    private Controls controls;
    private CharacterController playerCC;
    private Animator playerAnimator;
    private Vector3 moveDirection;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        playerCC = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();

        controls.Player.Jump.performed += ctx => Jump();
    }

    private void Update()
    {
        Vector2 inputDirection = controls.Player.Move.ReadValue<Vector2>();

        // Move forward
        if(inputDirection == new Vector2(0, 1))
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkForward"))
            {
                playerAnimator.Play("WalkForward");
            }

            moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;
            playerCC.Move(moveDirection * Time.deltaTime * movementSpeed);
        }

        // Move backwards
        if(inputDirection == new Vector2(0, -1))
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkBackward"))
            {
                playerAnimator.Play("WalkBackward");
            }

            moveDirection = -transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;
            playerCC.Move(moveDirection * Time.deltaTime * movementSpeed);
        }

        // Move right forward
        if(inputDirection.x > 0)
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkForwardRight"))
            {
                playerAnimator.Play("WalkForwardRight");
            }

            moveDirection = transform.right;
            moveDirection.y -= gravity * Time.deltaTime;
            playerCC.Move(moveDirection * Time.deltaTime * movementSpeed);
        }

        // Move left forward
        if(inputDirection.x < 0)
        {

            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkForwardLeft"))
            {
                playerAnimator.Play("WalkForwardLeft");
            }

            moveDirection = -transform.right;
            moveDirection.y -= gravity * Time.deltaTime;
            playerCC.Move(moveDirection * Time.deltaTime * movementSpeed);
        }

        // Not walking
        if(inputDirection == Vector2.zero)
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerAnimator.Play("Idle");
            }

            moveDirection = Vector3.zero;
            if()
            moveDirection.y -= gravity * Time.deltaTime;
            playerCC.Move(moveDirection * Time.deltaTime * movementSpeed);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0), Time.deltaTime * rotationSpeed);
    }

    void Jump()
    {
        if (playerCC.isGrounded)
        {
            Debug.Log("Called");
            moveDirection.y = jumpHeight;
        }
    }
}
