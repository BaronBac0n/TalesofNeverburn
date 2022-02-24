using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TextGame/Weapon")]
public class Weapon : InteractableObject
{
    [Space]
    public Vector2Int damageRange;

    [Space]
    public int blockAmount;

    [Space]
    public float maxDurability;
    public float currentDurability;

    public int GetRandomDamage()
    {
        return Random.Range(damageRange.x, damageRange.y);
    }

    public void SetDurabilityToMax()
    {
        currentDurability = maxDurability;
    }

    public void RemoveDurability()
    {
        if(maxDurability > 0)
        {
            currentDurability--;

            if (currentDurability <= 0)
            {
                GameController.instance.LogStringWithReturn("Your " + noun + " broke!");
                PlayerStats.instance.RemoveItemFromInventory(this);
            }
        }       
    }
}

