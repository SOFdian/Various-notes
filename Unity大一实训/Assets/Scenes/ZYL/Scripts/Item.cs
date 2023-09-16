using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ڴ洢�ڱ����б��͵�ͼ�ϵĸ�����Ʒһһ��Ӧ

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
