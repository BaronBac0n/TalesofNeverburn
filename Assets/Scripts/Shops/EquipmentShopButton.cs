using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentShopButton : MonoBehaviour
{
    public InteractableObject itemInButton;

    Button button;

    Text text;

    void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        button.image.sprite = itemInButton.itemSprite;

        if (itemInButton != null)
        {
            if (itemInButton.GetType().ToString() == "Weapon")
            {
                Weapon weaponInButton = (Weapon)itemInButton;
                text.text = weaponInButton.name + "\n" +
                    "Cost: " + weaponInButton.costToBuy + "\n" +
                    "Damage: " + weaponInButton.damageRange.x + "-" + weaponInButton.damageRange.y + " + STR \n" +
                    "Block: " + weaponInButton.blockAmount + " + DEX";
            }

            if (itemInButton.GetType().ToString() == "Consumable")
            {
                Consumable consumableInButton = (Consumable)itemInButton;
                text.text = consumableInButton.name + "\n" +
                    "Cost: " + consumableInButton.costToBuy + "\n" +
                    "Effect: " + consumableInButton.inventoryDescription;
            }
        }
        else
        {
            button.interactable = false;
        }
    }

    public void BuyItem()
    {
        PlayerStats playerStats = PlayerStats.instance;
        if(playerStats.gold >= itemInButton.costToBuy)
        {
            playerStats.RemoveGold(itemInButton.costToBuy);
            button.interactable = false;
            text.text = "SOLD OUT";
            playerStats.AddItemToInventory(itemInButton);
        }
    }
}
