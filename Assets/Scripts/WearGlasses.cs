using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearGlasses : MonoBehaviour
{
    public AudioSource destructionSound;

    // Function to handle collision with the player's head collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with the player's head collider
        if (other.gameObject.CompareTag("Glasses"))
        {
            // Play the destruction sound
            destructionSound.Play();

            // Destroy the glasses object
            Destroy(other.gameObject);

        }
    }
}





