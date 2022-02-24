using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;

    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    GameController controller;

    GameStateController stateController;


    public Room endRoom;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        stateController = GetComponent<GameStateController>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        if (stateController.gameState == GameStateController.GameState.EXPLORING)
        {

            if (exitDictionary.ContainsKey(directionNoun))
            {

                currentRoom = exitDictionary[directionNoun];
                controller.LogStringWithReturn("You head off to the " + directionNoun);

                if (currentRoom.checkIfItemsNeeded && PlayerStats.instance.nounsInInventory.Contains(currentRoom.objectNeeded))
                {
                    currentRoom = endRoom;
                    controller.buttonsPanel.SetActive(true);
                    controller.DisplayRoomText();
                    controller.interactableItems.DisplayInventoryAfterGame();
                }
                else
                {
                    controller.DisplayRoomText();
                }
            }
            else
            {
                controller.LogStringWithReturn("There is no path to the " + directionNoun);
            }
        }
        else
        {
            controller.LogStringWithReturn("You cannot run while in combat!");
        }
    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }
}

