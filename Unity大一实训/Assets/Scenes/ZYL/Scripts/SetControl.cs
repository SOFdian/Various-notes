using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//����player�ϣ����ƴ����ý���
public class SetControl : MonoBehaviour
{
    public AudioSource SetAudioSource;
    public AudioClip SetAudioClip;
    public GameObject settingUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            settingUI.SetActive(!settingUI.activeSelf);
            AudioSource.PlayClipAtPoint(SetAudioClip, transform.position);
        }
    }
}
