using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//背包列表，存储item
//和gameobject的背包不一样 这个类专用于存储数据
//也可以用在后面和npc交互时npc的背包

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventorys")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
