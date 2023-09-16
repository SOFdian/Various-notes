using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnMap : MonoBehaviour
{
    //private SpriteRenderer spriteRenderer;
    public AudioSource getAudio;
    public AudioClip getClip;
    public Inventory playerInventory;
    public Item thisItem;

    // Start is called before the first frame update
    void Awake()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        getAudio = GetComponent<AudioSource>();

    }

    //�ж���ײ���ͣ�������Ӧ
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider== )
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"
            && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")//����tag�޸ģ�����
        {
            //getAudio.Play();
            AudioSource.PlayClipAtPoint(getClip, transform.position);
            AddNewItem();
            //Invoke("Death", 0.35f);
            Destroy(gameObject);

            //gameObject.SetActive(false);
        }
        else
        {
            //�����Ƿ����������ƶ�
            return;
        }
    }

    private void Death()
    {
        Destroy(gameObject);

    }

    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            thisItem.itemHeld = 1;
            playerInventory.itemList.Add(thisItem);
            InventoryManager.CreateNewItem(thisItem);
        }
        else
        {
            thisItem.itemHeld += 1;
            //DataSave.Instance.PlayerItemList[i].itemHeld += 1;
        }
        InventoryManager.RefreshItem();               
    }
}