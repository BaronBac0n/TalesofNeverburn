using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    #region Singleton
    public static Tooltip instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Tooltip found");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] RectTransform popupObject;
    [SerializeField] Text infoText;
    [SerializeField] Vector3 offset;
    [SerializeField] float padding;
    [SerializeField] Canvas canvas;

    private void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        if (!popupObject.gameObject.activeSelf) { return; }

        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;

        //make sure it doesnt go off screen
        float rightEdgeToScreenDistance = Screen.width - (newPos.x + popupObject.rect.width * canvas.scaleFactor / 2) - padding;
        if (rightEdgeToScreenDistance < 0)
        {
            newPos.x += rightEdgeToScreenDistance;
        }

        float leftEdgeToScreenDistance = 0 - (newPos.x - popupObject.rect.width * canvas.scaleFactor / 2) + padding;
        if (leftEdgeToScreenDistance > 0)
        {
            newPos.x += leftEdgeToScreenDistance;
        }

        float topEdgeToScreenDistance = Screen.height - (newPos.y + popupObject.rect.height * canvas.scaleFactor) - padding;
        if (topEdgeToScreenDistance < 0)
        {
            newPos.y += topEdgeToScreenDistance;
        }
        popupObject.transform.position = newPos;
    }

    public void DisplayItemInfo(InteractableObject item)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=20>").Append(item.name).Append("</size>").AppendLine();
        builder.Append(item.GetInfoText());

        infoText.text = builder.ToString();
        popupObject.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    public void DisplayWeaponInfo(Weapon weapon)
    {

        StringBuilder builder = new StringBuilder();
        builder.Append("<size=20>").Append(weapon.name).Append("</size>").AppendLine();

        if (weapon.maxDurability > 0) //if the weapon uses durability, display it
        {
            float durabilityPercent = weapon.currentDurability / weapon.maxDurability;

            if (durabilityPercent > 0 && durabilityPercent < 0.35f) // 0 - .35
                builder.Append("<color=#FF0000>" + "Durability:" + weapon.currentDurability + "/" + weapon.maxDurability + "</color>").AppendLine();

            if (durabilityPercent > 0.36f && durabilityPercent < 0.70) // .36 - .70
                builder.Append("<color=#e2e61e>" + "Durability:" + weapon.currentDurability + "/" + weapon.maxDurability + "</color>").AppendLine();

            if (durabilityPercent > 0.71 && durabilityPercent <= 1 ) // .71 - 1
                builder.Append("<color=" +
                    "#00ff1e>" + "Durability:" + weapon.currentDurability + "/" + weapon.maxDurability + "</color>").AppendLine();
        }

        if (weapon.damageRange.x == weapon.damageRange.y) //if the damage of a weapon is not random, there is no need to display the min and max damage numbers      
            builder.Append("Deals " + weapon.damageRange.x.ToString() + " damage + STR").AppendLine();
        else
            builder.Append("Deals " + weapon.damageRange.x.ToString() + "-" + weapon.damageRange.y.ToString() + " damage + STR").AppendLine();

        builder.Append("Blocks " + weapon.blockAmount.ToString() + " damage + DEX");

        infoText.text = builder.ToString();
        popupObject.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);

    }

    public void DisplayStatInfo()
    {
        PlayerStats playerStats = PlayerStats.instance;
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=20>").Append("Your stats").Append("</size>").AppendLine();
        builder.Append("STR: " + playerStats.strength).AppendLine();
        builder.Append("DEX: " + playerStats.dexterity).AppendLine();

        infoText.text = builder.ToString();
        popupObject.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    public void DisplayCommandInfo()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=20>").Append("Commands").Append("</size>").AppendLine();
        builder.Append("Use 'go <direction>' to move that way. EX: 'go north'. \n \n" +
            "Use 'examine <item>' to examine an item in a room. EX: 'examine skull'. \n \n" +
            "Use 'take <item>' to take an item from a room. EX: 'take skull'. \n \n" +
            "Use 'use <item>' to use an item. EX: 'use skull'. \n \n" +
            "Click on a weapon in your inventory to attack or defend with it. \n" +
            "Use 'examine room' to see the room's description again.");

        infoText.text = builder.ToString();
        popupObject.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    public void DisplayEnemyHealthInfo()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=20>").Append("Enemy's Health").Append("</size>").AppendLine();
        builder.Append("Reduce this to 0 to kill them").AppendLine();

        infoText.text = builder.ToString();
        popupObject.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    public void DisplayEnemyDamageInfo()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=20>").Append("Enemy's Damage").Append("</size>").AppendLine();
        builder.Append("The range of damage the enemy will deal to you").AppendLine();

        infoText.text = builder.ToString();
        popupObject.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    public void DisplayEnemyBlockInfo()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=20>").Append("Enemy's Block").Append("</size>").AppendLine();
        builder.Append("The damage the enemy will negate from your attacks").AppendLine();

        infoText.text = builder.ToString();
        popupObject.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    public void HideInfo()
    {
        popupObject.gameObject.SetActive(false);
    }


}
