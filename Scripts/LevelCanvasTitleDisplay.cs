using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCanvasTitleDisplay : MonoBehaviour
{
    [SerializeField] int bigFontSize = 120;
    [SerializeField] int mediumFontSize = 100;
    [SerializeField] int smallFontSize = 80;
    string levelsCompletedKeyName = "levelsCompleted";


    public void ChangeText(int index)
    {
        var tmPro = GetComponent<TextMeshProUGUI>();
        if (index > 0 && index <= 5)
        {
            tmPro.text = "Play Level " + index.ToString() + "?";
            tmPro.alignment = TextAlignmentOptions.Top;
            tmPro.fontSize = mediumFontSize;
        }
        else if (index < 0) //quit game entrance
        {
            tmPro.text = "Quit Game?";
            tmPro.alignment = TextAlignmentOptions.Center;
            tmPro.fontSize = bigFontSize;
        }
        else if (index > 5) //end game note
        {

            tmPro.text = "Congratulations! You Completed the game!";
            tmPro.alignment = TextAlignmentOptions.Center;
            tmPro.fontSize = smallFontSize;
            var parentCanvas = transform.parent.transform.GetComponent<Canvas>();
            var coroutine = FindObjectOfType<HubEntrace>().DisplayCanvas(parentCanvas, 1f, true);
            StartCoroutine(coroutine);
        }
    }
}
