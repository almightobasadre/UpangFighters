using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TestController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInput;
    public float moveSpeed = 5f;
    private bool wasMovingRight;

    public float jumpForce = 10f;
    private float jumpStartTime = 0.25f;
    private float jumpTime;
    private bool isJumping;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    private float dashCooldown = 1f;
    private bool isDashing = false;

    private bool isAttacking = false;

    private Animator animator;
    private string currentAnimation;
    private float animTime = 0.0f;
    private Coroutine animationCoroutine;

    // Animation States
    const string IDLE = "Idle";
    const string WALKFORWARD = "WalkForward";
    const string WALKBACKWARD = "WalkBackward";
    const string DASH = "Dash";
    const string PUNCHLIGHT = "PunchLight";
    const string PUNCHHEAVY = "PunchHeavy";
    const string KICKHEAVY = "KickHeavy";
    const string JUMP = "Jump";

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

            if (wasMovingRight == true)
            {
                ChangeAnimationState(DASH);
            }
            else
            {
                ChangeAnimationState(WALKBACKWARD);
            }
        }
        // Animates Movements
        else if (isGrounded && !isAttacking)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            if (moveInput > 0)
            {
                ChangeAnimationState(WALKFORWARD);
            }
            else if (moveInput < 0)
            {
                ChangeAnimationState(WALKBACKWARD);
            }
            else
            {
                ChangeAnimationState(IDLE);
            }
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        // Play the animation
        animator.Play(newAnimation);

        // Stop the same animation from interrupting itself
        if (currentAnimation == newAnimation) return;

        // Reassign the current animation
        currentAnimation = newAnimation;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            wasMovingRight = moveInput > 0;
            moveInput = context.ReadValue<float>();
            animator.SetFloat("Move", moveInput);
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
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        /*if (context.started)
        {
            if (context.interaction is HoldInteraction)
            {
                Debug.Log(context.interaction);
            }
            else if (context.interaction is TapInteraction)
            {
                Debug.Log(context.interaction);
            }
        }

        switch (context.interaction)
        {
            case PressInteraction _:
                ChangeAnimationState(PUNCHHEAVY);
                break;
            case TapInteraction _:
                ChangeAnimationState(PUNCHLIGHT);
                break;
        }*/
        if (!isAttacking)
        {
            ChangeAnimationState(PUNCHLIGHT);
            isAttacking = true;
            animTime = animator.GetCurrentAnimatorStateInfo(0).length;

            // Stop any existing animation coroutine before starting a new one
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            StartCoroutine(WaitForAnimationComplete());
        }
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        if (isAttacking)
        {
            return; // Prevent starting a new attack while already attacking
        }

        if (context.started)
        {
            ChangeAnimationState(KICKHEAVY);
        }
        isAttacking = true;
        animTime = animator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(WaitForAnimationComplete());
    }

    private IEnumerator WaitForAnimationComplete()
    {
        yield return new WaitForSeconds(animTime);
        isAttacking = false;
        animationCoroutine = null; // Reset the coroutine reference
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        ChangeAnimationState(JUMP);
        animTime = animator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(WaitForAnimationComplete());
    }
}
