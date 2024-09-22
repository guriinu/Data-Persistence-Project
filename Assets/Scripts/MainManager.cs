using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int highScore;
    private bool m_GameOver = false;
    private string m_name;

    // Start is called before the first frame update
    void Start()
    {
        UpdateName();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);

                

            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            Load();
            UpdateScore();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {

            if(m_Points > highScore)
            {
                highScore= m_Points;
                Save();
                UpdateScore();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void UpdateName() 
    { 
        m_name=Manager.Instance.Name;
    }
   void UpdateScore()
    {
        BestScoreText.text = $"BsetScore:{m_name}:{highScore}";
    }
    [System.Serializable]
    class SaveData
    {
        public string m_name;
        public int highScore;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.m_name = m_name;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json",json);
        Debug.Log("Score Saved");

    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore= data.highScore;
            m_name = data.m_name;
            Debug.Log("Load Data");
        }
        

    }

    public void Delete()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        File.Delete(path);
        Debug.Log("Delete Data");
    }
}
