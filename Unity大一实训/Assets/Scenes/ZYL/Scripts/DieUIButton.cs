using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieUIButton : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Lose");
        SceneManager.LoadScene("FirstMenu");
    }
}
