using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float timeValue = 90;
    public Text timeText;
    // Update is called once per frame
    void Update()
    {
        if (timeValue>0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            //Appeler le gameOver
        }
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay<0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay>0)
        {
            timeToDisplay += 1;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (minutes==0 && seconds<=10)
        {
            timeText.color = Color.red;
        }

        if (seconds==0 && minutes==0)
        {
            GameOverManager.instance.WhenPlayerDeath();
        }
    }
    
    
}
