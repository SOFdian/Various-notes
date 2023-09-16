using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager shopInstance;
    public Inventory shop;
    public Text shopItemInfo;
    public GameObject Grid;
    public List<ShopSlot> shopSlots = new List<ShopSlot>(); //���ⲿ���и�ֵ �ǵ�ÿ��npcȷ��һ��Ҫ���Ķ��� ÿ��50��

    void Awake()
    {
        if (shopInstance != null)
            Destroy(this);
        shopInstance = this;
        shop.itemList.Clear();
    }

    public static void ShowInfo(string itemDescription)
    {
        shopInstance.shopItemInfo.text = itemDescription;
        for (int i = 0; i < shopInstance.shopSlots.Count; i++)
        {
            shopInstance.shopSlots[i].isClicked = false;
        }
    }

    private void Start()
    {
        
    }
}
