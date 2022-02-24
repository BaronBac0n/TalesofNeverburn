using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TextGame/InteractableObject")]
public class InteractableObject : ScriptableObject
{
    public string noun = "name";
    [TextArea]
    public string description = "Description in room";
    public bool showsDescriptionInRoom;

    [Space]
    [TextArea]
    public string inventoryDescription = "Description for inventory tooltips";

    [Space]
    public Sprite itemSprite;
    public bool canPickup;
    public AudioClip playOnPickup;

    public Interaction[] interactions;

    public string GetInfoText()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(inventoryDescription);
        return builder.ToString();
    }
}

