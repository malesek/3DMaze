using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MazeSize : MonoBehaviour
{
    public TMP_Text mazeSizeText;
    // Start is called before the first frame update
    void Start()
    {
        mazeSizeText.text = "Maze size: " + MazeGenerator.mazeHeight + "x" + MazeGenerator.mazeWidth;
    }

    // Update is called once per frame
    void Update()
    {
        mazeSizeText.text = "Maze size: " + MazeGenerator.mazeHeight + "x" + MazeGenerator.mazeWidth;
    }
}
