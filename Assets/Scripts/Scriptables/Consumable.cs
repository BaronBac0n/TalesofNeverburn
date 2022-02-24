using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/Consumable")]
public class Consumable : InteractableObject
{
    [Space]
    public int healAmount;
    public int raiseSTRAmount;
    public int raiseDEXAmount;

    [Space]
    [TextArea]
    public string consumedText = "What shows when the item is consumed";

    public void UseItem()
    {
        PlayerStats playerStats = PlayerStats.instance;
        if (healAmount > 0)
            playerStats.Heal(healAmount);

        if (raiseSTRAmount > 0)
            playerStats.strength += raiseSTRAmount;

        if (raiseDEXAmount > 0)
            playerStats.dexterity += raiseDEXAmount;

        playerStats.RemoveItemFromInventory(this);
        playerStats.gController.LogStringWithReturn(consumedText);
        GameController.instance.DisplayLoggedText();
    }
}
