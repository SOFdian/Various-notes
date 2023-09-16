using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMControl : MonoBehaviour
{
    public AudioSource bgm;
    public Slider bgmSlider;
    private void Update()
    {
        bgm.volume = bgmSlider.value;
        Debug.Log("bgmValue: "+bgm.volume);
        Debug.Log("Slider: " + bgmSlider.value);
    }
}
