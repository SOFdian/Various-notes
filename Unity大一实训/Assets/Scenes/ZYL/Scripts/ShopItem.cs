using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New ShopItem")]
public class ShopItem : ScriptableObject
{
    public Item item;  //对应玩家背包应该添加的物品
    public string shopItemName;
    public Sprite shopItemImage;
    public int shopItemHeld = 1;
    public int costCoin;
    [TextArea]
    public string shopItemInfo;
   // public bool euip;
}
