using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//����Canvas��
//��ͼ�ϵ���Ʒ������ӵ�ʱ����slot��ʾ�ڱ���UI��

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;  //��������������
    public Inventory myBag;  //�����б�
    public GameObject Grid;  //UI����ĸ��ӣ�����Grid
    public Slot slotPrefeb;  //prefab��slot
    public Text ItemInformation;  //�����·���ʾ������
    public List<Slot> slots = new List<Slot>();

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        myBag.itemList.Clear();
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.ItemInformation.text = "";
    }

    //����slotʱ���õķ������ڱ����������½���ʾ���ߵ���Ϣ
    public static void UpdateItemInfo(string itemDescription)
    {
        instance.ItemInformation.text = itemDescription;
        for (int i = 0; i < instance.slots.Count; i++)
        {
            instance.slots[i].isClicked = false;
        }
    }

    
    //���item����Ϣ�������slot
    public static void CreateNewItem(Item item)
    {
        //����һ��slotPrefeb�����ӣ���ͨ��Bag��Gridȷ��λ��
        Slot newItem = Instantiate(instance.slotPrefeb, instance.Grid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.Grid.transform);
        //��Ϣ����    
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
        
        instance.slots.Add(newItem);
    }
    
    public static void RefreshItem()
    {
        for (int i = 0; i < instance.Grid.transform.childCount; i++)
        {
            if (instance.Grid.transform.childCount == 0)
                break;
            Destroy(instance.Grid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }
    }
}
