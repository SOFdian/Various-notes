using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//挂在player上，控制打开设置界面
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
