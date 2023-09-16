using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseButton : MonoBehaviour
{
    public Player player;
    public void UseButtonOnClicked()
    {
        for (int i = 0; i < InventoryManager.instance.slots.Count; i++)
        {
            if (InventoryManager.instance.slots[i].isClicked == true)
            {                
                if (InventoryManager.instance.slots[i].slotItem.itemHeld >= 1)
                {
                    player.healthValue += InventoryManager.instance.slots[i].slotItem.changeHealthPoint;
                    player.jumpNum += InventoryManager.instance.slots[i].slotItem.changeJumpNum;
                    player.ChangeDashCd = InventoryManager.instance.slots[i].slotItem.changeDashCD;
                    InventoryManager.instance.slots[i].slotItem.itemHeld -= 1;
                    InventoryManager.instance.slots[i].slotNum.text = InventoryManager.instance.slots[i].slotItem.itemHeld.ToString();
                }
                else if(InventoryManager.instance.slots[i].slotItem.itemHeld == 0)
                {
                    return;
                }
                InventoryManager.instance.slots[i].isClicked = false;
            }
        }
    }
}
