using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TextGame/Gold")]
public class Gold : InteractableObject
{
    public Vector2Int randomRange;

    public int AddGold()
    {
        int rand = Random.Range(randomRange.x, randomRange.y);
        GameController.instance.LogStringWithReturn("It's worth " + rand + " coins");
        return rand;
    }
}

