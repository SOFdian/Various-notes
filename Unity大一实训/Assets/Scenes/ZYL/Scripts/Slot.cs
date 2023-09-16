using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//该脚本挂在prefab Slot上，是背包UI界面的物品

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public bool isClicked;

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
        isClicked = true;
    }
    private void Start()
    {
        
    }
}
