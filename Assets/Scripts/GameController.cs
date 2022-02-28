using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text displayText;

    [HideInInspector]
    public RoomNavigation roomNavigation;

    List<string> actionLog = new List<string>();
    [HideInInspector]
    public List<string> interactionDescriptionsInRoom = new List<string>();

    [HideInInspector]
    public List<string> enemiesAliveInRoom = new List<string>();

    public InputAction[] inputActions;

    [HideInInspector]
    public InteractableItems interactableItems;

    [HideInInspector]
    public Enemies enemies;

    [HideInInspector]
    public GameStateController stateController;

    [HideInInspector]
    public AudioSource aud;


    public Image roomDisplay;


    public PlayerStats playerStats;

    public Text playerHealthDisplay;

    public GameObject buttonsPanel;
    
    [Space]
    [Header("Audio")]
    public AudioClip itemPickup;


    void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameController found");
            return;
        }
        instance = this;
        #endregion

        roomNavigation = GetComponent<RoomNavigation>();
        interactableItems = GetComponent<InteractableItems>();
        stateController = GetComponent<GameStateController>();
        aud = GetComponent<AudioSource>();
        enemies = GetComponent<Enemies>();
        playerStats.currentHealth = playerStats.maxHealth;
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayRoomText()
    {

        Room currentRoom = roomNavigation.currentRoom;

        ClearCollectionsForNewRoom();
        UnpackRoom();

        string joinedInteractionDesriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string joinedEnemyDesriptions = string.Join("\n", enemiesAliveInRoom.ToArray());

        string combinedText = currentRoom.description + "\n" + joinedEnemyDesriptions + "\n" + joinedInteractionDesriptions;


        //change the room display to the new room
        if (currentRoom.roomSprite != null)
        {
            roomDisplay.enabled = true;
            roomDisplay.sprite = currentRoom.roomSprite;
        }
        else
        {
            roomDisplay.enabled = false;
        }

        //if the room is a shop
        if (currentRoom.GetType().ToString() == "Shop")
        {
            Shop shop = (Shop)currentRoom;
            //if it's an equipment shop
            if (shop.shopType == Shop.ShopType.EQUIPMENT)
            {
                //display the UI
                EquipmentShopScript.instance.ShowShopPanel();
            }

            if (shop.shopType == Shop.ShopType.STATS)
            {
                StatUpgrade.instance.ShowTutorUI();
            }
        }

        LogStringWithReturn(combinedText);
        LogStringWithReturn("--------------------------------------");
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    void UnpackRoom()
    {
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
        PrepareEnemiesInRoom(roomNavigation.currentRoom);

        roomNavigation.UnpackExitsInRoom();
    }

    void PrepareEnemiesInRoom(Room currentRoom)
    {
        if (currentRoom.enemies.Count > 0)
        {
            for (int i = 0; i < currentRoom.enemies.Count; i++)
            {
                Enemy currentEnemy = currentRoom.enemies[i];
                if (currentEnemy.currentHealth > 0)
                {
                    stateController.gameState = GameStateController.GameState.COMBAT;
                    playerStats.combatState = PlayerStats.CombatState.ATTACKING;
                    //start the combat encounter
                    CombatEncounter(enemies.GetEnemiesNotDead(currentRoom, i));

                }
            }
        }
    }

    void CombatEncounter(Enemy enemy)
    {
        EnemyInfoDisplay.instance.ShowEnemyInfoDisplay(enemy);
        enemiesAliveInRoom.Add(enemy.description);
    }

    void PrepareObjectsToTakeOrExamine(Room currentRoom)
    {
        for (int i = 0; i < currentRoom.interactableObjectsInRoom.Count; i++)
        {
            string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
            if (descriptionNotInInventory != null && currentRoom.interactableObjectsInRoom[i].showsDescriptionInRoom == true)
            {
                interactionDescriptionsInRoom.Add(descriptionNotInInventory);
            }

            InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];
            for (int j = 0; j < interactableInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableInRoom.interactions[j];
                if (interaction.inputAction.keyWord == "examine")
                {
                    interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                }

                if (interaction.inputAction.keyWord == "take")
                {
                    interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                }
            }
        }
    }

    public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
    {
        if (verbDictionary.ContainsKey(noun))
        {
            return verbDictionary[noun];
        }
        else
        {
            return "You can't " + verb + " " + noun;
        }
    }

    void ClearCollectionsForNewRoom()
    {
        interactableItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
        enemies.ClearEnemies();
        enemiesAliveInRoom.Clear();
    }
}

