using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartingScene()
    {
        FindObjectOfType<GameSession>().SelfDestroy();
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        FindObjectOfType<GameSession>().SelfDestroy();
        SceneManager.LoadScene(1);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(GameOverScene());
    }
    
    public void LoadWinGameScene()
    {
        SceneManager.LoadScene("Win Game Scene");
    }

    private IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
