using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] bool isTutorialExit;
    bool isLevelFinished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isLevelFinished)
        {
            isLevelFinished = true;
            var level = FindObjectOfType<Level>();
            var levelsCompleted = PlayerPrefs.GetInt("levelsCompleted");
            if (isTutorialExit)
            {
                PlayerPrefs.SetInt("tutorialCompleted", 1);
            }
            else if (levelsCompleted <= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("levelsCompleted", levelsCompleted + 1);
            }
            StartCoroutine(level.WaitAndLoadHub());
            FindObjectOfType<GameSession>().SaveCoinsScore();
        }     
    }
}
