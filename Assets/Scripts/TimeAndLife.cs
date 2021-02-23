using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAndLife : MonoBehaviour
{
    [SerializeField] Text lifeText;
    [SerializeField] Text timeText;
    [SerializeField] int winTimeInMins = 5;

    float winTimeInSec = 60;
    string strWinTimeInSec;
    string strWinTimeInMins;
    string strTimer;

    Player player;


    // Start is called before the first frame update
    void Start()
    {
        TimeTextHandler();

        timeText.text = strTimer;
        player = FindObjectOfType<Player>();

        if (player)
        {
            lifeText.text = player.GetPlayerHealth().ToString();
        }
    }
    private void TimeTextHandler()
    {
        winTimeInMins--;
        if (winTimeInMins < 10)
        {
            strWinTimeInMins = "0" + winTimeInMins.ToString();
        }
        strTimer = strWinTimeInMins + ":00";

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            lifeText.text = player.GetPlayerHealth().ToString();
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        winTimeInSec -= Time.deltaTime;

        if (winTimeInMins <= 0 && winTimeInSec <= 0)
        {
            strWinTimeInSec = "0" + winTimeInSec.ToString();
            FindObjectOfType<Level>().LoadWinGameScene();
        }
        if (winTimeInSec < 10)
        {
            strWinTimeInSec = "0" + winTimeInSec.ToString();
        }
        else
        {
            strWinTimeInSec = winTimeInSec.ToString();
        }
        if (winTimeInMins < 10)
        {
            strWinTimeInMins = "0" + winTimeInMins.ToString();
        }
        else
        {
            strWinTimeInMins = winTimeInMins.ToString();
        }
        strTimer = strWinTimeInMins + ":" + strWinTimeInSec;
        timeText.text = strTimer;

        if (winTimeInSec <= 0)
        {
            winTimeInSec = 59f;
            winTimeInMins--;
        }
    }

}
