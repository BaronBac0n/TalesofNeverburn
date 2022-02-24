using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    public List<Enemy> enemiesKilled = new List<Enemy>();
    public List<Enemy> enemiesInRoom = new List<Enemy>();
    public List<Enemy> allEnemies = new List<Enemy>();

    private void Awake()
    {
        foreach (Enemy enemy in allEnemies)
        {
            enemy.currentHealth = enemy.maxHealth;
            enemy.isAboveHalfHealth = true;
        }
    }

    public Enemy GetEnemiesNotDead(Room currentRoom, int i)
    {

        Enemy currentEnemy = currentRoom.enemies[i];

        if (!enemiesKilled.Contains(currentEnemy))
        {
            enemiesInRoom.Add(currentEnemy);
            return currentEnemy;

        }
        else
        {
            return null;
        }
    }

    public void ClearEnemies()
    {
        enemiesInRoom.Clear();
    }
}