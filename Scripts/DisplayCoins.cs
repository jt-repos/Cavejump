using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCoins : MonoBehaviour
{
    TextMeshProUGUI coinsText;
    int coinsToFind;

    // Start is called before the first frame update
    void Start()
    {
        coinsToFind = FindObjectOfType<GameSession>().GetCoinsToFind();
        coinsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var coinsFound = FindObjectOfType<GameSession>().GetCoinsFound();
        coinsText.text = coinsFound + "/" + coinsToFind;
    }
}
