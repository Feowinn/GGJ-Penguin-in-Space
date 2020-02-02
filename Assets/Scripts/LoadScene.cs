using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //public string scene="main";
    public void LoadMainScene()
    {
        SceneManager.LoadScene("main");
        Debug.Log("load");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("credits");
    }

    public void LoadGameOverScreen()
    {
        SceneManager.LoadScene("gameOver");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("start");
    }


}
