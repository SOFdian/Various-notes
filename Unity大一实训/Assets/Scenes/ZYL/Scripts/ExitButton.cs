using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
        SceneManager.LoadScene("FirstMenu");
    }
}
