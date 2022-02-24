using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public InteractableObject objectInButton;

    Button button;

    private void Start()
    {
        button = this.GetComponent<Button>();
    }

    public void UseItem()
    {
        if (GameStateController.instance.gameState != GameStateController.GameState.DEAD)
        {
            if (objectInButton.GetType().ToString() == "Weapon")
            {
                PlayerStats.instance.PlayerAttack((Weapon)objectInButton);
            }
            else if (objectInButton.GetType().ToString() == "Consumable")
            {
                Consumable consumableInButton = (Consumable)objectInButton;
                consumableInButton.UseItem();
            }
        }
    }

    public void Update() //This is the only update function in the game and i'd like to try get rid of it eventually
    {
        button.interactable = !objectInButton ? false : true; //sets the button to interactive only if there is something in it       


    }
}
