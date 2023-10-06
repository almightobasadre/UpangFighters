using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public Transform target; // The other character's Transform

    private Vector3 initialScale;

    public GameObject hitbox; // Reference to the Hitbox GameObject

    // Start is called before the first frame update
    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the direction from this character to the target character
            Vector3 direction = target.position - transform.position;

            // Check if the target is on the left or right of this character
            if (direction.x < 0)
            {
                // Flip the character to face left
                transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            }
            else
            {
                // Flip the character to face right
                transform.localScale = initialScale;
            }
        }

    }

    public void ActivateHitbox()
    {
        // Enable the Hitbox GameObject to activate it
        hitbox.SetActive(true);
    }

    public void DeactivateHitbox()
    {
        // Disable the Hitbox GameObject to deactivate it
        hitbox.SetActive(false);
    }
}
