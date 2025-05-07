using System.Collections;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public Transform cameraTransform;
    public float defaultMovementSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;
    public float groundCheckRaySize = 0.1f;
    public PowerUpController powerUpController;
    public Transform modelTransform;
    public Controls controls;
    public CharacterController playerCC;
    public bool canDoubleJump = false;
    public bool canSprint = false;
    public bool knockbackApplied = false;

    private float currentMoveSpeed;
    private Animator playerAnimator;
    private Vector3 moveDirection;
    private Vector3 downForce;
    public bool isGrounded = true;
    private bool canAttack = true;

    private void OnEnable()
    {
        // Enable the player's controls
        controls = new Controls();
        controls.Enable();

        // Get the character controller and animator
        playerCC = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // isGrounded Debug Ray
        // Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), new Vector3(0, -1, 0), Color.red, groundCheckRaySize);

        // Read the direction of the movement input
        Vector2 inputDirection = controls.Player.Move.ReadValue<Vector2>().normalized;

        Debug.Log(inputDirection.x + "," + inputDirection.y);

        // Allows for diagonal movement if player is going sideways
        Vector3 directionalModifier = new Vector3(0, 0, 0);
        if ((inputDirection.x < 0.95f && inputDirection.x > 0.05f) || (inputDirection.x > -0.95f && inputDirection.x < -0.05f))
        {
            Debug.Log("Diagonal functionality triggered");
            if (inputDirection.y < 1 && inputDirection.y > 0)
            {
                directionalModifier = transform.forward;
                Debug.Log("Diagonal forward");
            }
            else if (inputDirection.y > -1 && inputDirection.y < 0)
            {
                directionalModifier = -transform.forward;
                Debug.Log("Diagonal backward");
            }
        }


        //Sprint
        if (controls.Player.Sprint.IsPressed() && canSprint)
        {
            StartCoroutine(nameof(SprintCD));
            currentMoveSpeed = defaultMovementSpeed * 2f;
        }
        else
        {
            currentMoveSpeed = defaultMovementSpeed;
        }

        // Not walking
        if (inputDirection == Vector2.zero && canAttack)
        {
            MoveAndAnimate("Idle", Vector3.zero);
        }
        // Move forward
        else if (inputDirection.y > 0.71f && inputDirection.y <= 1f)
        {
            MoveAndAnimate("WalkForward", transform.forward);
        }
        // Move back
        else if (inputDirection.y < -0.71f && inputDirection.y >= -1f)
        {
            MoveAndAnimate("WalkBackward", -transform.forward);
        }
        // Move right
        else if (inputDirection.x > 0)
        {
            MoveAndAnimate("StrafeRight", transform.right + directionalModifier);
        }
        // Move left
        else if (inputDirection.x < 0)
        {
            MoveAndAnimate("StrafeLeft", -transform.right + directionalModifier);
        }

        // Normalize movement to avoid diagonal speed boosting
        moveDirection = moveDirection.normalized;
        
        // Limit horizontal movement if airborne
        if (!isGrounded)
        {
            moveDirection = new Vector3(moveDirection.x * 0.75f, moveDirection.y, moveDirection.z * 0.75f);
        }

        // Apply movement
        playerCC.Move(moveDirection * Time.deltaTime * currentMoveSpeed);
        
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
                powerUpController.usedDoubleJump();
            }
        }

        // Gravity application
        downForce.y += gravity * Time.deltaTime;
        playerCC.Move(downForce * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), new Vector3(0, -1, 0), out hit, groundCheckRaySize))
        {
            if(hit.collider.gameObject.layer == 6)
            {
                isGrounded = true;
            }
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

    public void doubleJumpNowActive()
    {
        canDoubleJump = true;
    }

    public void sprintNowActive()
    {
        canSprint = true;
    }

    public IEnumerator SprintCD()
    {
        while (canSprint)
        {
            yield return new WaitForSeconds(10);
            canSprint = false;
            powerUpController.usedSprint();
            StopCoroutine(nameof(SprintCD));
        }
    }
    public IEnumerator AttackCD()
    {
        while (!canAttack)
        {
            yield return new WaitForSeconds(1);
            canAttack = true;
            StopCoroutine(nameof(AttackCD));
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