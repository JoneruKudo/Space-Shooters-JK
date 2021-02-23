using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    int score = 0;

    private void Awake()
    {
        int countGameSessions = FindObjectsOfType<GameSession>().Length;
        if(countGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateScore()
    {
        score += 10;
    }

    public int GetScore()
    {
        return score;
    }

    public void SelfDestroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
