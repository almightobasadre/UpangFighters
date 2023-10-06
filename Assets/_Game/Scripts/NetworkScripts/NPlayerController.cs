using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class NPlayerController : NetworkBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public float moveSpeed = 5f;

    private Vector2 gravityForce;
    public float gravity = 9.81f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float jumpForce = 10f;
    private bool isGrounded;

    // Call the object once in scene
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once every frame
    private void FixedUpdate()
    {
        // Moves Player
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.deltaTime);
        // Animate Sprite
        Debug.Log(moveInput);

        // Check if the character is grounded using Raycast
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0f, groundLayer);
        // Debug log to check the isGrounded state
        Debug.Log("Is Grounded: " + isGrounded);
        // Moves Player only if grounded or moving upward
        
        // Manually simulates gravity for kinematic RigidBody2D if not grounded
        if (!isGrounded)
        {
            gravityForce = new Vector2(0f, -gravity * Time.fixedDeltaTime);
            rb.position += gravityForce;
        }
    }

    public void OnMove(InputAction.CallbackContext moveContext)
    {
        // Called when there is movement input.
        // The value parameter contains the Vector2 input data
        // Use float when using Axis or Button instead of Vector2
        moveInput = moveContext.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext jumpContext)
    {
        Debug.Log(jumpContext.phase);
    }
}
/*{

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public float moveSpeed = 5f;

    public float collisionOffset = 0.2f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public float gravity = 9.81f;
    private float jumpForce = 10f;

    // Call the object once in scene
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once every frame
    private void FixedUpdate()
    {
        // Manually simulates gravity for kinematic RigidBody2D if not grounded
        TryMove(new Vector2(moveInput.x, -gravity * Time.fixedDeltaTime));
        
        // Attempt to move in the given direction
        if (moveInput != Vector2.zero)
        {
            bool displace = TryMove(moveInput);

            // If there are collisions while moving in the given direction, try to displace partially in x-direction
            if (!displace)
            {
                displace = TryMove(new Vector2(moveInput.x, 0));
            }
            // If there are still collisions while displacing in x-direction, try to displace partially in y-direction
            if (!displace)
            {
                displace = TryMove(new Vector2(0, moveInput.y));
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        // Checks for potential collisions
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

        if (count == 0) // No collisions
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else // Disables movement
        {
            return false;
        }
    }

    public void OnMove(InputAction.CallbackContext moveContext)
    {
        // Called when there is movement input.
        // The value parameter contains the Vector2 input data
        // Use float when using Axis or Button instead of Vector2
        moveInput = moveContext.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext jumpContext)
    {
        Debug.Log(jumpContext.phase);
    }
}*/
