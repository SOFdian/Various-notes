using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public int stepToStart;//与初始房间的距离
    public GameObject doorLeft, doorRight, doorUp, doorDown;//四周的门
    public bool roomLeft, roomRight, roomUp, roomDown;//四周是否有房间

    public int doorNumber;//每个房间开放的门的数量


    // Start is called before the first frame update
    void Start()
    {
        //根据四周是否有房间决定门是否开放
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void UpdateRoom()
    {
        //计算房间与basic room的距离
        //距离有正有负，所以用绝对值，18和9根据实际房间大小来设计
        stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 9));

        //计算房间开放的门的数量
        if (roomDown)
            doorNumber++;
        if (roomUp)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }


}
