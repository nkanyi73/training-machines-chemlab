using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearLabCoat : MonoBehaviour
{
    public AudioSource destructionSound;

    // Function to handle collision with the player's body collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with the player's body collider
        if (other.gameObject.CompareTag("Coat"))
        {
            // Play the destruction sound
            destructionSound.Play();

            // Destroy the Coat object
            Destroy(other.gameObject);

        }
    }
}
