using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentShopScript : MonoBehaviour
{
    #region Singleton
    public static EquipmentShopScript instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentShopScript found");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject shopPanel;

    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
    }

    public void HideShopPanel()
    {
        shopPanel.SetActive(false);
    }

    public void LeaveShop(Room roomToChangeTo)
    {
        HideShopPanel();
        GameController.instance.roomNavigation.currentRoom = roomToChangeTo;
        GameController.instance.DisplayRoomText();
    }
}
