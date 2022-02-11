using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayBeforeLoad;
    [SerializeField] float timeSlow;
    [SerializeField] bool resetSaveOnStart;
    [SerializeField] int levelsCompletedDebug;
    string levelsCompletedKeyName = "levelsCompleted";
    string tutorialCompletedKeyName = "tutorialCompleted";
    int levelsCompleted;
    

    // Start is called before the first frame update
    void Start()
    {
        if(resetSaveOnStart)
        {
            PlayerPrefs.DeleteAll(); //debug
            PlayerPrefs.SetInt(levelsCompletedKeyName, levelsCompletedDebug); //debug
        }
        levelsCompleted = SetPlayerPrefsInt(levelsCompletedKeyName);
    }

    private int SetPlayerPrefsInt(string keyName)
    {
        if (!PlayerPrefs.HasKey(name))
        {
            PlayerPrefs.SetInt(name, 0);
        }
        var keyVariable = PlayerPrefs.GetInt(keyName);
        return keyVariable;
    }

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if(FindObjectsOfType<Level>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator WaitAndLoadHub()
    {
        Time.timeScale /= timeSlow;
        yield return new WaitForSeconds(delayBeforeLoad / timeSlow);
        LoadHub();
        Time.timeScale *= timeSlow;
    }

    public void LoadHub()
    {
        if (!PlayerPrefs.HasKey(tutorialCompletedKeyName))
        {
            PlayerPrefs.SetInt(tutorialCompletedKeyName, 0);
        }
        if (PlayerPrefs.GetInt(tutorialCompletedKeyName) == 0) //0 false, 1 true
        {
            LoadTutorial();
        }
        else
        {
            SceneManager.LoadScene("Level 0");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevelFromHub()
    {
        SceneManager.LoadScene(FindObjectOfType<DisplayCoinsHub>().GetCurrentEntranceIndex());
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Level h2p");
    }
}
