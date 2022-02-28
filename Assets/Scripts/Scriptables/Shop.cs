using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/Shop")]
public class Shop : Room
{
    public enum ShopType { STATS, EQUIPMENT};
    public ShopType shopType;
}
