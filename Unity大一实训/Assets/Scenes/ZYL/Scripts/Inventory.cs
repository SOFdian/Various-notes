using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����б��洢item
//��gameobject�ı�����һ�� �����ר���ڴ洢����
//Ҳ�������ں����npc����ʱnpc�ı���

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventorys")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
