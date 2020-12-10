using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Script displays a timer
public class Timer : MonoBehaviour
{
    private float time;
    public float finalTime;
    Text timerText;
  
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameEnding.atHostage)
        {
            time += Time.deltaTime;
            string minutes = ((time % 3600) / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
        else
        {
            finalTime = time;
        }
    }
}
