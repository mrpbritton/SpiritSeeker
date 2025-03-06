using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;
    public float groundCheckRaySize = 0.1f;

    private Controls controls;
    private CharacterController playerCC;
    private Animator playerAnimator;
    private Vector3 moveDirection;
    private Vector3 downForce;
    private bool isGrounded = true;
    public bool canDoubleJump = false;
    public bool canSprint = false;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        playerCC = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), new Vector3(0, -1, 0), Color.red, groundCheckRaySize);

        // Read the direction of the movement input
        Vector2 inputDirection = controls.Player.Move.ReadValue<Vector2>();

        if (controls.Player.Sprint.IsPressed() && canSprint)
        {
            StartCoroutine(nameof(SprintCD));
            movementSpeed = 10f;
        }
        else
        {
            movementSpeed = 5f;
        }

        // Move forward
        if (inputDirection.y == 1)
        {
            MoveAndAnimate("WalkForward", transform.forward);
        }

        // Move backwards
        if(inputDirection.y == -1)
        {
            MoveAndAnimate("WalkBackward", -transform.forward);
        }

        // Move right
        if(inputDirection.x > 0)
        {
            MoveAndAnimate("StrafeRight", transform.right);
        }

        // Move left forward
        if(inputDirection.x < 0)
        {
            MoveAndAnimate("StrafeLeft", -transform.right);
        }

        // Not walking
        if(inputDirection == Vector2.zero)
        {
            MoveAndAnimate("Idle", Vector3.zero);
        }

        // Apply movement
        playerCC.Move(moveDirection * Time.deltaTime * movementSpeed);

        // Keep downForce at 0 because otherwise you're accumulating a downForce.y that the jumpHeight can't overcome
        if(isGrounded && downForce.y < 0)
        {
            downForce.y = 0;
        }

        // Jump
        if ((controls.Player.Jump.triggered && isGrounded) || (controls.Player.Jump.triggered && canDoubleJump))
        {
            downForce.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            if(isGrounded == false && canDoubleJump == true)
            {
                canDoubleJump = false;
            }
        }

        downForce.y += gravity * Time.deltaTime;
        playerCC.Move(downForce * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), new Vector3(0, -1, 0), out hit, groundCheckRaySize))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Rotate with the camera
        if(moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0), Time.deltaTime * rotationSpeed);
        }
    }

    public void MoveAndAnimate(string animation, Vector3 direction)
    {
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animation) && isGrounded)
        {
            playerAnimator.Play(animation);
        }
        else if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("FallingLoop") && !isGrounded)
        {
            playerAnimator.Play("FallingLoop");
        }
        moveDirection = direction;
    }

    public IEnumerator SprintCD()
    {
        while (canSprint)
        {
            yield return new WaitForSeconds(10);
            canSprint = false;
            Debug.Log("Sprint false");
            StopCoroutine(nameof(SprintCD));
        }
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);   
    }
}