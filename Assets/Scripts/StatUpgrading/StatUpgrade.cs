using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgrade : MonoBehaviour
{
    public int strength;
    public int dexterity;
    public int maxHealth;

    public int gold;
    public Text goldText;

    public Text STRcost;
    public Text DEXcost;
    public Text VIGcost; //doing this as health for the moment

    private void Start()
    {
        //setting the cost values
        STRcost.text = strength.ToString();
        DEXcost.text = dexterity.ToString();
        VIGcost.text = maxHealth.ToString();

        goldText.text = gold.ToString();
}

    public void StrUpgrade()
    {
        if (gold >= strength)
        {
            gold -= strength;
            strength++;
            STRcost.text = strength.ToString();
            goldText.text = gold.ToString();
        }
        else
        {
            //cannot buy
        }
        
    }

    public void DexUpgrade()
    {
        if (gold >= dexterity)
        {
            gold -= dexterity;
            dexterity++;
            DEXcost.text = dexterity.ToString();
            goldText.text = gold.ToString();
        }
        else
        {
            //cannot buy
        }
        
    }

    public void VigUpgrade()
    {
        if (gold >= maxHealth)
        {
            gold -= maxHealth;
            maxHealth++;
            VIGcost.text = maxHealth.ToString();
            goldText.text = gold.ToString();
        }
        else
        {
            //cannot buy
        }
    }
}