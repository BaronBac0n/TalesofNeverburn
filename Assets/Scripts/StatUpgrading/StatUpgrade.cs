using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgrade : MonoBehaviour
{
    public int strength;
    public int dexterity;
    public int maxHealth;

    public float strPrice;
    public float dexPrice;
    public float vigPrice;
    public int currentSTR;
    public int currentDEX;
    public int currentVIG;

    public float increaseValue;
    public int gold;
    public Text goldText;

    public Text STRcost;
    public Text DEXcost;
    public Text VIGcost; //doing this as health for the moment

    private void Start()
    {
        strPrice = 60;
        dexPrice = 60;
        vigPrice = 60;
        increaseValue = 1.6f;

        currentSTR = ((int)strPrice);
        currentDEX = ((int)dexPrice);
        currentVIG = ((int)vigPrice);

        //setting the cost values
        STRcost.text = currentSTR.ToString();
        DEXcost.text = currentDEX.ToString();
        VIGcost.text = currentVIG.ToString();

        goldText.text = gold.ToString();
}

    public void StrUpgrade()
    {
        if (gold >= currentSTR)
        {
            gold -= currentSTR;
            strength++;

            strPrice = strPrice * increaseValue;
            currentSTR = ((int)strPrice);
            STRcost.text = currentSTR.ToString();
            goldText.text = gold.ToString();
        }
        else
        {
            //cannot buy
        }
        
    }

    public void DexUpgrade()
    {
        if (gold >= currentDEX)
        {
            gold -= currentDEX;
            dexterity++;

            dexPrice = dexPrice * increaseValue;
            currentDEX = ((int)dexPrice);
            DEXcost.text = currentDEX.ToString();
            goldText.text = gold.ToString();
        }
        else
        {
            //cannot buy
        }
        
    }

    public void VigUpgrade()
    {
        if (gold >= currentVIG)
        {
            gold -= currentVIG;
            maxHealth++;

            vigPrice = vigPrice * increaseValue;
            currentVIG = ((int)vigPrice);
            VIGcost.text = currentVIG.ToString();
            goldText.text = gold.ToString();
        }
        else
        {
            //cannot buy
        }
    }
}