using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1); //just change number to the village scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
