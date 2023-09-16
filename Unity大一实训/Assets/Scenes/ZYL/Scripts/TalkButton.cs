using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject talkUI;
    // Start is called before the first frame update
    void Start()
    {
        talkUI.SetActive(false);
    }

    public void ButtonOnClicked()
    {       
         talkUI.SetActive(!talkUI.activeSelf);
    }
}
