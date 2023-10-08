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

    private float animTime;
    private bool isAttacking;
    private Coroutine animationCoroutine;

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
        animTime = 0.5f;
        StartCoroutine(WaitPunchLAnim());
    }

    private IEnumerator WaitPunchLAnim()
    {
        yield return new WaitForSeconds(animTime);
        animator.SetBool("PunchLight", false);
        animationCoroutine = null; // Reset the coroutine reference
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        animator.SetBool("KickForward", true);
        animTime = 0.65f;
        StartCoroutine(WaitKickFAnim());
    }

    private IEnumerator WaitKickFAnim()
    {
        yield return new WaitForSeconds(animTime);
        animator.SetBool("KickForward", false);
        animationCoroutine = null; // Reset the coroutine reference
    }

    public void OnPunchF(InputAction.CallbackContext context)
    {
        // Check if both "D" and "T" keys are pressed simultaneously.
        if (Keyboard.current.dKey.isPressed && Keyboard.current.tKey.isPressed)
        {
            // Uppercut punch logic here
            Debug.Log("Uppercut punch performed!");
            animator.SetBool("PunchForward", true);
            animTime = 0.6666666f;
            StartCoroutine(WaitPunchFAnim());
        }
    }

    private IEnumerator WaitPunchFAnim()
    {
        yield return new WaitForSeconds(animTime);
        animator.SetBool("PunchForward", false);
        animationCoroutine = null; // Reset the coroutine reference
    }
}
