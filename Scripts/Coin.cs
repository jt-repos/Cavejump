using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    bool collected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collected)
        {
            Destroy(gameObject);
            FindObjectOfType<GameSession>().IncreaseCoinsFound(coinValue);
            collected = true;
        }
    }

    public int GetCoinValue()
    {
        return coinValue;
    }    
}
