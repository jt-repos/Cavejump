using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayCoinsHub : MonoBehaviour
{
    TextMeshProUGUI coinText;
    int currentEntranceIndex;
    Color colorStandard;
    [SerializeField] Color colorWhenMaxCoins;

    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
        colorStandard = coinText.color;
    }

    public void ChangeText(int index, int coinsInLevel)
    {
        currentEntranceIndex = index;
        if(index > 0 && index <= 5)
        {
            var levelName = "Level " + currentEntranceIndex;
            var scoreToDisplay = PlayerPrefs.GetInt(levelName);
            coinText.text = scoreToDisplay.ToString() + "/" + coinsInLevel;
            if (scoreToDisplay >= coinsInLevel)
            {
                coinText.color = colorWhenMaxCoins;
            }
            else
            {
                coinText.color = colorStandard;
            }
            foreach (Transform child in transform)
            {
                var FullAlpha = Color.white;
                FullAlpha.a = 0.88f;
                child.GetComponent<Image>().color = FullAlpha;
            }
        }
        else
        {
            var zeroAlpha = coinText.color;
            zeroAlpha.a = 0;
            coinText.color = zeroAlpha;
            foreach (Transform child in transform)
            {
                child.GetComponent<Image>().color = Color.clear;
            }
        }
        FindObjectOfType<LevelCanvasTitleDisplay>().ChangeText(index);
    }

    public int GetCurrentEntranceIndex()
    {
        return currentEntranceIndex;
    }
}
