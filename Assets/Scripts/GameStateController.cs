using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    #region Singleton
    public static GameStateController instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameStateController found");
            return;
        }
        instance = this;
        #endregion
    }

    public enum GameState { COMBAT, EXPLORING, DEAD };
    public GameState gameState;
}
