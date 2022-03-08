using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    #region Singleton
    public static SceneSwapper instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SceneSwapper found");
            return;
        }
        instance = this;
    }
    #endregion

    public Room courtyard;
    public GameObject endButtons, enemyinfoPanel;

    //private void Update()
    //{
    //    if(GameObject.FindGameObjectsWithTag("Game Manager").Length > 1)
    //    {
    //        Destroy(GameObject.FindGameObjectsWithTag("Game Manager")[1]);
    //    }
    //}

    public void ReturnToVillage()
    {
        GameController.instance.roomNavigation.currentRoom = courtyard;
        GameController.instance.DisplayRoomText();
        GameController.instance.DisplayLoggedText();
        PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
        PlayerStats.instance.UpdateHealthDisplay();
        endButtons.SetActive(false);
        enemyinfoPanel.SetActive(false);
        GameController.instance.stateController.gameState = GameStateController.GameState.EXPLORING;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
