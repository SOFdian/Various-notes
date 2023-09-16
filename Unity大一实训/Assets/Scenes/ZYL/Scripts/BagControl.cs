using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��������player�ϣ����ڽű�����
public class BagControl : MonoBehaviour
{
    public GameObject myBag;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenMyBag();
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            myBag.SetActive(!myBag.activeSelf);
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }
}
