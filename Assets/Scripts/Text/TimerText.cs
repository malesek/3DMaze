using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    public static float time = 30;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
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
