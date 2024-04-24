using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    public static float time;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        time = 20;
        timeText.text = time.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0) time = 0;
        timeText.text = time.ToString("F2");
    }
}
