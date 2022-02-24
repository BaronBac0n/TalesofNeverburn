using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoDisplay : MonoBehaviour
{
    #region Singleton
    public static EnemyInfoDisplay instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EnemyInfoDisplay found");
            return;
        }
        instance = this;

    }
    #endregion

    public Text healthDisplay;
    public Text damageDisplay;
    public Text blockDisplay;

    private void Start()
    {
        HideEnemyInfoDisplay();
    }

    public void ShowEnemyInfoDisplay(Enemy enemy)
    {
        this.gameObject.SetActive(true);
        UpdateEnemyInfoDisplay(enemy);
    }

    public void HideEnemyInfoDisplay()
    {
        gameObject.SetActive(false);
    }

    public void UpdateEnemyInfoDisplay(Enemy enemy)
    {
        healthDisplay.text = enemy.currentHealth.ToString() + "/" + enemy.maxHealth.ToString();
        damageDisplay.text = enemy.damage.x.ToString() + "-" + enemy.damage.y.ToString();
        blockDisplay.text = enemy.block.ToString();
    }
}
