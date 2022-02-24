using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/Room")]
public class Room : ScriptableObject
{
    [TextArea]
    public string description;
    public string roomName;

    public Exit[] exits;
    public Sprite roomSprite;
    public List<InteractableObject> interactableObjectsInRoom;
    public List<Enemy> enemies;

    public bool checkIfItemsNeeded;
    public string objectNeeded;
}
