using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] Text scoreText;


    GameSession myGameSession;

    // Start is called before the first frame update
    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
        scoreText.text = myGameSession.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = myGameSession.GetScore().ToString();
    }

    
}
