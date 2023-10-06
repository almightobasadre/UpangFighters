using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;
    public float moveSpeed = 5f;
    private bool wasMovingRight;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    private float dashCooldown = 1f;
    private bool isDashing = false;

    private float animDuration;

    public int isMoving
    {
        get; private set;
    }

    private void Awake()
    {
        // Set the target frame rate to 60 FPS
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (isDashing)
        {
            // Handle dashing behavior here
            rb.velocity = new Vector2((wasMovingRight ? 1 : -1) * dashSpeed, 0f);
        }
        else if (isGrounded)// && !isAttacking)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            wasMovingRight = moveInput > 0;
            moveInput = context.ReadValue<float>();
            isMoving = (int)moveInput;
            animator.SetInteger("Moving", isMoving);
        }
        else if (context.performed)
        {
            if (context.interaction is MultiTapInteraction && isGrounded)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        if (wasMovingRight)
        {
            animator.SetBool("Dashing", true);
        }
        else if (!wasMovingRight)
        {
            animator.SetInteger("Moving", -1);
        }
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        animator.SetInteger("Moving", isMoving);
        animator.SetBool("Dashing", false);
        yield return new WaitForSeconds(dashCooldown);
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        animator.SetBool("PunchLight", true);
    }

}
