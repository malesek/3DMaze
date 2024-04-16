using UnityEngine;

public class Point : MonoBehaviour
{
    // Static variable to keep track of the score
    public static int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Increment the score
            score++;

            // Destroy the point object
            Destroy(gameObject);
        }
    }
}
