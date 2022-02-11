using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSession : MonoBehaviour
{
    int coinsToFind;
    int coinsFound;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Coin coin in FindObjectsOfType<Coin>())
        {
            coinsToFind += coin.GetCoinValue();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncreaseCoinsFound(int value)
    {
        coinsFound += value;
    }

    public int GetCoinsFound()
    {
        return coinsFound;
    }
    public int GetCoinsToFind()
    {
        return coinsToFind;
    }

    public void SaveCoinsScore()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(PlayerPrefs.HasKey(sceneName));
        if(!PlayerPrefs.HasKey(sceneName) || PlayerPrefs.GetInt(sceneName) < coinsFound)
        {
            PlayerPrefs.SetInt(sceneName, coinsFound);
            Debug.Log(222);
        }
    }
}
