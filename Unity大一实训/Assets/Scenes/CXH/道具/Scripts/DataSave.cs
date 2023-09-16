using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave
{
    public int PlayerHp;
    public int PlayerCoinNum;
    public int PlayerCD;
    public int PlayerJumpTime;
    public List<Item> PlayerItemList;

    public static DataSave Instance;
    
    //private GameObject player;
    // Start is called before the first frame update
    public DataSave()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(Instance);
        //Instance = this;
        Instance = this;
            //}else if (Instance != null)
            //{
            //    Destroy(gameObject);
            //}

        }
    // Update is called once per frame
    public void Update(GameObject player)
    {
        Instance.PlayerHp = player.GetComponent<Player>().healthValue;
        Instance.PlayerCoinNum = player.GetComponent<Player>().CoinNumber;
        Instance.PlayerCD = player.GetComponent<Player>().ChangeDashCd;
        Instance.PlayerJumpTime = player.GetComponent<Player>().jumpNum;
        Instance.PlayerItemList = InventoryManager.instance.myBag.itemList;
    }
    
}
