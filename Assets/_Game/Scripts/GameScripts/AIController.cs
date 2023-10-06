using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // Add references to your AI character's components and animations
    public Transform player;
    private Rigidbody2D rb;
    private float distanceToPlayer;

    // AI behavior parameters
    public float rMoveMinTime = 0.5f;
    public float rMoveMaxTime = 2.5f;
    private float rActionDuration; // The remaining duration for the current action
    private bool isPerformingAction; // Flag to indicate if an action is currently being performed
    public float moveSpeed = 2.0f;
    public float farDistance = 2f;
    public float midDistance = 1f;
    public float nearDistance = 0f;
    public float anyDistance = 1.75f ;

    private Animator animator;
    private string currentAnimation;
    private float animTime;

    // Animation States
    const string IDLE = "Idle";
    const string WALKFORWARD = "WalkForward";
    const string WALKBACKWARD = "WalkBackward";
    const string DASH = "Dash";
    const string PUNCHLIGHT = "PunchLight";
    const string PUNCHHEAVY = "PunchHeavy";
    const string KICKHEAVY = "KickHeavy";
    const string JUMP = "Jump";

    // Start is called before the first frame update
    void Start()
    {
        // Initialize references to components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Implement AI logic here based on your game's rules and conditions
        // For example, you can use timers, distances, or other criteria to trigger actions.

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (!isPerformingAction)
        {
            HandleInteractions(distanceToPlayer);
        }

        /*Debug.Log(rActionDuration);
        if (distanceToPlayer > farDistanceThreshold)
        {
            HandleInteractions(farDistanceThreshold);
        }
        else if (distanceToPlayer > midDistanceThreshold)
        {
            HandleInteractions(midDistanceThreshold);
        }
        else if (distanceToPlayer > nearDistanceThreshold)
        {
            HandleInteractions(nearDistanceThreshold);
        }
        else
        {
            HandleInteractions(farDistanceThreshold);
        }*/
    }

    void HandleInteractions(float distance)
    {
        // Decide whether to perform a new action based on the distance
        if (distance > farDistance)
        {
            PerformRandomMovement();
        }
        else if (distance > midDistance)
        {
            PerformRandomMovement();
        }
        else if (distance > nearDistance)
        {
            PerformRandomMovement();
        }
        else
        {
            PerformRandomMovement();
        }
    }

    void PerformRandomMovement()
    {
        int randomAction = Random.Range(0, 3); // 0: Idle, 1: Move forward, 2: Move backward

        switch (randomAction)
        {
            case 0:
                Idle();
                ChangeAnimationState(IDLE);
                break;

            case 1:
                MoveForward();
                ChangeAnimationState(WALKFORWARD);
                break;

            case 2:
                MoveBackward();
                ChangeAnimationState(WALKBACKWARD);
                break;
        }
        // Set the flag to indicate that an action is currently being performed
        isPerformingAction = true;
        // Set the action duration for this action
        rActionDuration = Random.Range(rMoveMinTime, rMoveMaxTime);
        // Start a coroutine to reset the action flag after the specified duration
        StartCoroutine(ResetActionFlag(rActionDuration));
    }

    IEnumerator ResetActionFlag(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Reset the action flag after the duration has passed
        isPerformingAction = false;
    }

    void Idle()
    {
        // Implement logic to make the AI idle
        rb.velocity = Vector2.zero;
        animator.SetBool("Move", false);
    }

    void MoveForward()
    {
        // Implement logic to move forward here
        // For example, set the velocity of the Rigidbody2D to move forward
        Vector2 forwardDirection = -transform.right; // Assuming the enemy is facing left
        rb.velocity = forwardDirection * moveSpeed;
        animator.SetBool("Move", true);
    }

    void MoveBackward()
    {
        // Implement logic to move backward here
        // For example, set the velocity of the Rigidbody2D to move backward
        Vector2 backwardDirection = transform.right; // Assuming the enemy is facing left
        rb.velocity = backwardDirection * moveSpeed;
        animator.SetBool("Move", true);
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
}
