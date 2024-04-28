using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BestScoreText : MonoBehaviour
{
    public TMP_Text bestScoreText;
    public static int points;
    public static float time;
    private double score;
    private double previousScore;
    // Start is called before the first frame update
    void Start()
    {
        previousScore = score;

        score = Math.Floor(CalculateScoreCoefficient(points, time));

        if(score > previousScore)
        {
            bestScoreText.text = "Best Score: " + score;
        }
    }

    double CalculateScoreCoefficient(int points, float timeRemaining)
    {
        if(points == 0 && timeRemaining <= 0) return 0;
        // Adjust time remaining to avoid division by zero
        if (timeRemaining <= 0)
        {
            timeRemaining = 1;
        }

        // Calculate the coefficient
        double scoreCoefficient = 1000 * (timeRemaining * points + points * (1.0 / timeRemaining));

        return scoreCoefficient;
    }
}
