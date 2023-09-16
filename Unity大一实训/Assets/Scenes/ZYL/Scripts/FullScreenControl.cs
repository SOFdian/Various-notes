using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenControl : MonoBehaviour
{
    public void FullScreenOnClicked()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
