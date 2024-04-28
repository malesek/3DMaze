using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int gameType = 0;
    public void Start()
    {
        //Reset all variables when game ends
        switch(gameType)
        {
            case 0:
                SmallMode(); break;
            case 1:
                MediumMode(); break;
            case 2:
                BigMode(); break;
            default: 
                SmallMode(); break;
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //GameMode changes
    public void SmallMode() 
    {
        MazeGenerator.mazeHeight = 5;
        MazeGenerator.mazeWidth = 5;
        TimerText.time = 30;
        gameType = 0;
    }
    public void MediumMode() 
    {
        MazeGenerator.mazeHeight = 10;
        MazeGenerator.mazeWidth = 10;
        TimerText.time = 90;
        gameType = 1;
    }
    public void BigMode() 
    {
        MazeGenerator.mazeHeight = 15;
        MazeGenerator.mazeWidth = 15;
        TimerText.time = 180;
        gameType = 2;
    }
}