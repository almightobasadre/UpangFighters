using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public GameObject hitbox; // Reference to the Hitbox GameObject

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
