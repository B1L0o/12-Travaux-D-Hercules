using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTimer : MonoBehaviour
{
    public GameObject Timer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("p2"))
        {
            Timer.SetActive(true);
        }
        
    }
}

