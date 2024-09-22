using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUiHandler : MonoBehaviour
{
    public TMP_InputField IF;
    private string text;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        Debug.Log($"Start {Manager.Instance.Name}");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void EnterName()
    {
        text=IF.text;
        Manager.Instance.Name = text;
       
    }
}

