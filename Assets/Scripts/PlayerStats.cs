using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerStats found");
            return;
        }
        instance = this;
    }
    #endregion

    public int currentHealth, maxHealth;
    public int strength;
    public int dexterity;

    public InteractableObject[] inventory;
    public int maxInventorySlots;
    public Button[] inventoryButtons;

    public List<string> nounsInInventory = new List<string>();

    [Space]
    public int gold;
    public Text goldDisplay;

    [HideInInspector]
    public GameController gController;

    public enum CombatState { ATTACKING, DEFENDING };
    public CombatState combatState;

    public Sprite blankInventoryImage;

    private void Start()
    {
        gController = GameController.instance;
        AddGold(0);
    }

    public void TakeDamage(int amount, GameController controller, Enemy attackingEnemy)
    {
        gController = controller;
        currentHealth -= amount;
        if (!attackingEnemy.isNamed)
            controller.LogStringWithReturn("The " + attackingEnemy.noun + " attacks you for " + amount + " damage!");
        else
            controller.LogStringWithReturn(attackingEnemy.noun + " attacks you for " + amount + " damage!");

        gController.LogStringWithReturn("--------------------------------------");
        UpdateHealthDisplay();

        if (DeathCheck())
        {
            controller.LogStringWithReturn("YOU DIED");
            controller.stateController.gameState = GameStateController.GameState.DEAD;
            controller.buttonsPanel.SetActive(true);
        }
    }

    public bool DeathCheck()
    {
        return (currentHealth <= 0) ? true : false;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthDisplay();
    }

    public void UpdateHealthDisplay()
    {
        if (currentHealth <= 0) currentHealth = 0;
        gController.playerHealthDisplay.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    public void DisplayInventory()
    {
        gController.LogStringWithReturn("You look in your backpack, inside you have: ");
        for (int i = 0; i < inventory.Length; i++)
        {
            gController.LogStringWithReturn(inventory[i].name);
        }
    }

    public void AddItemToInventory(InteractableObject objectToAdd)
    {
        inventory[GetFirstFreeInventorySlot()] = objectToAdd;
        UpdateInventoryDisplay();

        if (objectToAdd.GetType().ToString() == "Weapon")
        {
            Weapon weapon = (Weapon)objectToAdd;
            weapon.SetDurabilityToMax();
        }

        if (objectToAdd.GetType().ToString() == "Gold")
        {
            Gold goldsack = (Gold)objectToAdd;
            AddGold(goldsack.AddGold());
            RemoveItemFromInventory(goldsack);
            gController.GetComponent<TextInput>().InputComplete();
        }

        gController.aud.clip = objectToAdd.playOnPickup;
        gController.aud.Play();

        nounsInInventory.Add(objectToAdd.noun);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        goldDisplay.text = gold.ToString();
    }

    public void RemoveGold(int amount)
    {
        gold -= amount;
        goldDisplay.text = gold.ToString();
    }

    public void RemoveItemFromInventory(InteractableObject objectToRemove)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == objectToRemove)
            {
                inventory[i] = null;
                if(Tooltip.instance != null)
                Tooltip.instance.HideInfo();
                UpdateInventoryDisplay();
            }
        }
    }

    public void UpdateInventoryDisplay()
    {
        for (int i = 0; i < inventory.Length; i++) //loop over all inventory slots
        {
            if (inventory[i] == null) //if theres nothing in the slot make it blank and stuff
            {
                inventoryButtons[i].GetComponent<Image>().sprite = blankInventoryImage;
                inventoryButtons[i].GetComponent<InventoryButton>().objectInButton = null;
            }
            else           
            {
                inventoryButtons[i].GetComponent<Image>().sprite = inventory[i].itemSprite;
                inventoryButtons[i].GetComponent<InventoryButton>().objectInButton = inventory[i];
            }
        }
    }

    public int GetFirstFreeInventorySlot()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                return i;
            }
        }
        //there are no empty slots
        return -1;
    }

    public void PlayerAttack(Weapon weaponToAttackWith)
    {
        GameController controller = GameController.instance;

        if (controller.roomNavigation.currentRoom.enemies.Count > 0) // if there's an enemy in the room
        {
            Enemy enemyToAttack = controller.roomNavigation.currentRoom.enemies[0];

            if (!controller.enemies.enemiesKilled.Contains(enemyToAttack)) // if the enemy's not already dead
            {
                if (combatState == CombatState.ATTACKING) // if it's time to attack
                {
                    if (!enemyToAttack.isNamed)
                        controller.LogStringWithReturn("You attack the " + enemyToAttack.noun + " with your " + weaponToAttackWith.noun + "!");
                    else
                        controller.LogStringWithReturn("You attack " + enemyToAttack.noun + "!");
                    enemyToAttack.TakeDamage(CalculateDamage(weaponToAttackWith, enemyToAttack)); //Deal damage to the enemy
                    weaponToAttackWith.RemoveDurability();
                    combatState = CombatState.DEFENDING;
                }
                else // if it's time to defend
                {
                    int damageToTake = enemyToAttack.CalculateDamage() - (weaponToAttackWith.blockAmount - dexterity);
                    if (damageToTake <= 0)
                    {
                        controller.LogStringWithReturn("You block the attack completely with your " + weaponToAttackWith.noun);
                    }
                    else
                    {
                        currentHealth -= damageToTake;
                        controller.LogStringWithReturn("You block with your " + weaponToAttackWith.noun + " and take " + damageToTake + " damage");
                        if (DeathCheck())
                        {
                            controller.LogStringWithReturn("<color=red>YOU DIED</color>");
                            controller.stateController.gameState = GameStateController.GameState.DEAD;
                            controller.buttonsPanel.SetActive(true);                            
                            UpdateHealthDisplay();
                        }
                        else
                        {
                            weaponToAttackWith.RemoveDurability(); //Remove durability for blocking
                            controller.LogStringWithReturn("--------------------------------------");
                            controller.LogStringWithReturn("What will you do?");
                            UpdateHealthDisplay();
                            combatState = CombatState.ATTACKING;
                        }
                    }                   
                }
                //update the tooltip to match the new durability
                Tooltip.instance.HideInfo();
                Tooltip.instance.DisplayWeaponInfo(weaponToAttackWith);
            }
            else
            {
                controller.LogStringWithReturn("There are no enemies here to attack.");
            }
        }
        else
        {
            controller.LogStringWithReturn("There are no enemies here to attack.");
        }
        controller.GetComponent<TextInput>().InputComplete();
    }

    public int CalculateDamage(Weapon weaponToAttackWith, Enemy target)
    {
        return (weaponToAttackWith.GetRandomDamage() + strength) - target.block;
    }
    

}
