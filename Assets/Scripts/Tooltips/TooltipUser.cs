using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipUser : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Tooltip tooltip;
    public enum tooltipType { SLOT0, SLOT1, SLOT2, SLOT3, SLOT4, SLOT5, SLOT6, SLOT7, SLOT8, CMDINFO, STATINFO, ENEMYHEALTH, ENEMYDMG, ENEMYBLOCK};
    public tooltipType type;


    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (type)
        {
            case tooltipType.STATINFO:
                tooltip.DisplayStatInfo();
                break;

            case tooltipType.CMDINFO:
                tooltip.DisplayCommandInfo();
                break;

            case tooltipType.ENEMYBLOCK:
                tooltip.DisplayEnemyBlockInfo();
                break;

            case tooltipType.ENEMYDMG:
                tooltip.DisplayEnemyDamageInfo();
                break;

            case tooltipType.ENEMYHEALTH:
                tooltip.DisplayEnemyHealthInfo();
                break;

            default:
                if (PlayerStats.instance.inventory[(int)type] != null) //if there is something in the inventory slot
                {
                    if (PlayerStats.instance.inventory[(int)type].GetType().ToString() == "Weapon") //if the item is a weapon
                    {
                        tooltip.DisplayWeaponInfo((Weapon)PlayerStats.instance.inventory[(int)type]);
                    }
                    else //if the item is not a weapon
                    {
                        tooltip.DisplayItemInfo(PlayerStats.instance.inventory[(int)type]);
                    }
                }
                break;
        }      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideInfo();
    }
}
