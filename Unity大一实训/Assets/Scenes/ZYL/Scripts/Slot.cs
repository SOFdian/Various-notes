using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ýű�����prefab Slot�ϣ��Ǳ���UI�������Ʒ

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
