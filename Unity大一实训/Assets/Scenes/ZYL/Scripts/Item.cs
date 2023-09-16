using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于存储在背包列表，和地图上的各种物品一一对应

[CreateAssetMenu(fileName="New Item",menuName="Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld = 1;
    [TextArea]
    public string itemInfo;
    public bool euip;
    public int changeHealthPoint;
    public int changeJumpNum;
    public int changeDashCD;
}
