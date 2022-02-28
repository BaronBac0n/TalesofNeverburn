using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/InputActions/Help")]
public class Help : InputAction
{
    public override void RespondToInput(GameController controller, string[] seperatedInputWords)
    {
        controller.LogStringWithReturn(
            "Use 'go <direction>' to move that way. EX: 'go north'. \n" +
            "Use 'examine <item>' to examine an item in a room. EX: 'examine skull'. \n" +
            "Use 'take <item>' to take an item from a room. EX: 'take skull'. \n" +
            "Use 'use <item>' to use an item. EX: 'use skull'. \n" +
            "Click on a weapon in your inventory to attack or defend with it. \n" +
            "Use 'examine room' to see the room's description again.");

        controller.LogStringWithReturn("--------------------------------------");
    }
}
