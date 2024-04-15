using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasses : MonoBehaviour
{
    public GameObject glasses;
    public AudioSource destructionSound;

    // Function to handle collision with the player's head collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player's head collider
        if (collision.gameObject.CompareTag("Glasses"))
        {
            // Destroy the glasses object
            Destroy(gameObject);
        }
    }
}





