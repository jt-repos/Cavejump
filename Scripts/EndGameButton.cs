﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DismissMessage()
    {
        FindObjectOfType<HubEntrace>().HideCanvas();
        Destroy(gameObject, 0.5f);
    }

    public void DestroyInstantly()
    {
        Destroy(gameObject);
    }
}
