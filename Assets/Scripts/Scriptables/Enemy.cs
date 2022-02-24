using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/Enemy/Enemy")]
public class Enemy : ScriptableObject
{
    public string noun = "name";
    [TextArea]
    public string description = "Description in room";

    public Interaction[] interactions;

    public int maxHealth;
    public int currentHealth;

    public int block;

    public Vector2Int damage;

    public bool isAboveHalfHealth = true;
    public bool isNamed = false;

    public bool changesRoomOnDeath;
    public Room roomToChangeTo;

    public void TakeDamage(int amount)
    {
        GameController controller = GameController.instance;
        if (amount >= 0)
            currentHealth -= amount;

        EnemyInfoDisplay.instance.UpdateEnemyInfoDisplay(this);
        if (!isNamed)
            controller.LogStringWithReturn("The " + noun + " takes " + amount + " damage!");
        else
            controller.LogStringWithReturn(noun + " takes " + amount + " damage!");

        if (currentHealth <= (maxHealth / 2) && isAboveHalfHealth && currentHealth > 0)
        {
            isAboveHalfHealth = false;
            if (!isNamed)
                controller.LogStringWithReturn("The " + noun + " is looking rough!");
            else
                controller.LogStringWithReturn(noun + " is looking rough!");
        }

        if (DeathCheck())
        {
            EnemyInfoDisplay.instance.HideEnemyInfoDisplay();
            if (!isNamed)
                controller.LogStringWithReturn("The " + noun + " falls to the ground, dead.");
            else
                controller.LogStringWithReturn(noun + " falls to the ground, dead.");

            if (changesRoomOnDeath)
            {
                controller.roomNavigation.currentRoom = roomToChangeTo;
                controller.DisplayRoomText();
            }
            controller.enemies.enemiesKilled.Add(this);
            PlayerStats.instance.combatState = PlayerStats.CombatState.ATTACKING;
            controller.stateController.gameState = GameStateController.GameState.EXPLORING;
        }
        else
        {
            Attack();
        }
    }

    public bool DeathCheck()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        else return false;
    }

    public void Attack()
    {
        GameController controller = GameController.instance;
        if (!isNamed)
            controller.LogStringWithReturn("The " + noun + " prepares to attack! What will you block with?");
        else
            controller.LogStringWithReturn(noun + " prepares to attack! What will you block with?");
        //PlayerStats.instance.PlayerBlock(this);
        //controller.playerStats.TakeDamage(CalculateDamage(), controller, this);
    }

    public int CalculateDamage()
    {
        return Random.Range(damage.x, damage.y);
    }
}
