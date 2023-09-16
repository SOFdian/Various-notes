using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ShopItem shopSlotItem;
    public Image slotImage;
    public Text slotNum;
    public bool isClicked;

    public void ItemOnClicked()
    {
        ShopManager.ShowInfo(shopSlotItem.shopItemInfo);
        isClicked = true;
    }

    private void Start()
    {
        if (shopSlotItem.name == "JumpMedicine" || shopSlotItem.name=="CDMedicine")
        {
            shopSlotItem.shopItemHeld = 1;
        }
        else
        {
            shopSlotItem.shopItemHeld = 50;
        }    
    }
}
