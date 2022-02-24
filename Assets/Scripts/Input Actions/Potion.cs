using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/ActionResponses/Potion")]
public class Potion : ActionResponse
{

    public int restoreAmount;
    public override bool DoActionResponse(GameController controller)
    {
        controller.playerStats.Heal(restoreAmount);
        controller.LogStringWithReturn("You drink the potion and restore " + restoreAmount + " health.");
        return true;
    }
}