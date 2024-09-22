using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
public class Manager : MonoBehaviour
{

    public static Manager Instance;
    public string Name; 

    private void Awake()
    {
        // start of new code
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    [System.Serializable]
    class SaveData
    {
        public string Name;
    }
}
