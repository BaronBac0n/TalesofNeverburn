using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/InputActions/Help")]
public class Help : InputAction
{
    public override void RespondToInput(GameController controller, string[] seperatedInputWords)
    {
        controller.LogStringWithReturn(
            "Use '<color=green>go <DIRECTION></color>' to move that way. EX: 'go north'. \n" +
            "Use '<color=blue>examine <ITEM></color>' to examine an item in a room. EX: 'examine skull'. \n" +
            "Use '<color=orange>take <ITEM></color>' to take an item from a room. EX: 'take skull'. \n" +
            "Click on a <color=purple>WEAPON</color> in your inventory to attack or defend with it. \n" +
            "Use 'examine room' to see the room's description again.");

        controller.LogStringWithReturn("--------------------------------------");
    }
}
