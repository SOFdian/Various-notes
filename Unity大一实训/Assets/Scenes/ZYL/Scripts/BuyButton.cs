using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public Inventory playerBag;
    public Player player;

    public void BuyButtonOnClicked()
    {
        for (int i = 0; i < ShopManager.shopInstance.shopSlots.Count; i++)
        {
            Debug.Log(ShopManager.shopInstance.shopSlots[i].isClicked);
            if (ShopManager.shopInstance.shopSlots[i].isClicked == true)
            {
                if (ShopManager.shopInstance.shopSlots[i].shopSlotItem.shopItemHeld >= 1 && player.CoinNumber >= ShopManager.shopInstance.shopSlots[i].shopSlotItem.costCoin)
                {
                    ShopManager.shopInstance.shopSlots[i].shopSlotItem.shopItemHeld -= 1;
                    ShopManager.shopInstance.shopSlots[i].slotNum.text = ShopManager.shopInstance.shopSlots[i].shopSlotItem.shopItemHeld.ToString();
                    player.GetComponent<Player>().CoinNumber -= ShopManager.shopInstance.shopSlots[i].shopSlotItem.costCoin;
                    if (!playerBag.itemList.Contains(ShopManager.shopInstance.shopSlots[i].shopSlotItem.item))
                    {
                        ShopManager.shopInstance.shopSlots[i].shopSlotItem.item.itemHeld = 1;
                        playerBag.itemList.Add(ShopManager.shopInstance.shopSlots[i].shopSlotItem.item);
                        InventoryManager.CreateNewItem(ShopManager.shopInstance.shopSlots[i].shopSlotItem.item);
                        InventoryManager.RefreshItem();
                    }
                    else
                    {
                        ShopManager.shopInstance.shopSlots[i].shopSlotItem.item.itemHeld += 1;
                        InventoryManager.RefreshItem();
                    }
                }
                else
                {
                    return;
                }
                ShopManager.shopInstance.shopSlots[i].isClicked = false;
            }
        }
    }
}
