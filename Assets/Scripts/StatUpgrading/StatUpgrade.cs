using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgrade : MonoBehaviour
{
    #region Singleton
    public static StatUpgrade instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of StatUpgrade found");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject shopDisplay;

    public PlayerStats playerStats;
    
    public float strPrice;
    public float dexPrice;
    public float vigPrice;
    public int currentSTR;
    public int currentDEX;
    public int currentVIG;

    public float increaseValue = 1.6f;

    public Text STRcost;
    public Text DEXcost;
    public Text VIGcost; //doing this as health for the moment

    private void Start()
    {
        playerStats = PlayerStats.instance;

        strPrice = 60;
        dexPrice = 60;
        vigPrice = 60;

        currentSTR = ((int)strPrice);
        currentDEX = ((int)dexPrice);
        currentVIG = ((int)vigPrice);

        //setting the cost values
        STRcost.text = currentSTR.ToString();
        DEXcost.text = currentDEX.ToString();
        VIGcost.text = currentVIG.ToString();
}

    public void StrUpgrade()
    {
        if (playerStats.gold >= currentSTR)
        {
            playerStats.RemoveGold(currentSTR);
            playerStats.strength++;

            strPrice = strPrice * increaseValue;
            currentSTR = ((int)strPrice);
            STRcost.text = currentSTR.ToString();
        }
        else
        {
            //cannot buy
            print("NOT ENOUGH GOLD");
            //maybe flash the gold text and make a sound
        }
        
    }

    public void DexUpgrade()
    {
        if (playerStats.gold >= currentDEX)
        {
            playerStats.RemoveGold(currentDEX);
            playerStats.dexterity++;

            dexPrice = dexPrice * increaseValue;
            currentDEX = ((int)dexPrice);
            DEXcost.text = currentDEX.ToString();
        }
        else
        {
            //cannot buy
            print("NOT ENOUGH GOLD");
            //maybe flash the gold text and make a sound
        }
    }

    public void VigUpgrade()
    {
        if (playerStats.gold >= currentVIG)
        {
            playerStats.RemoveGold(currentVIG);
            playerStats.maxHealth+= 5;
            playerStats.currentHealth+=5;
            playerStats.UpdateHealthDisplay();
            playerStats.UpdateHealthDisplay();

            vigPrice = vigPrice * increaseValue;
            currentVIG = ((int)vigPrice);
            VIGcost.text = currentVIG.ToString();
        }
        else
        {
            //cannot buy
            print("NOT ENOUGH GOLD");
            //maybe flash the gold text and make a sound
        }
    }

    public void ShowTutorUI()
    {
        shopDisplay.SetActive(true);
    }

    public void HideTutorUI()
    {
        shopDisplay.SetActive(false);
    }

    public void LeaveShop(Room roomToChangeTo)
    {
        HideTutorUI();
        GameController.instance.roomNavigation.currentRoom = roomToChangeTo;
        GameController.instance.DisplayRoomText();
    }
}