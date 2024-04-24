using UnityEngine;
using UnityEngine.SceneManagement;

public class Point : MonoBehaviour
{
    // Static variable to keep track of the score
    public static int points;

    private void Start()
    {
        points = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Increment the score
            points++;

            // Destroy the point object
            Destroy(gameObject);

            if(points == MazeRenderer.maxPoints)
            {
                BestScoreText.points = points;
                BestScoreText.time = TimerText.time;
                SceneManager.LoadSceneAsync(0);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    private void Update()
    {
        if(TimerText.time <= 0)
        {
            BestScoreText.points = points;
            BestScoreText.time = 0;
            SceneManager.LoadSceneAsync(0);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
