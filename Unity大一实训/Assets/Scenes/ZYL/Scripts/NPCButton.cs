using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCButton : MonoBehaviour
{
    public GameObject talkUI;
    public GameObject Bag;
    public int index = 1;

    public void OnClicked()
    {
        if (index == 1)
        {
            talkUI.SetActive(true);
            index++;
        }
        else
        {
            Bag.SetActive(true);
        }
    }
}
