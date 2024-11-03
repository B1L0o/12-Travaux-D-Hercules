using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public static Counter instance;
    public TMP_Text WallText;
    public int currentWalls;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        WallText.text = "Number of walls repared: " + currentWalls.ToString();
    }

    public void IncreaseWalls()
    {
        currentWalls++;
        WallText.text = "Number of walls repared: " + currentWalls.ToString();
        if (currentWalls==4)
        {
            VictoryManager.instance.WhenBossDeath();
        }
    }
}
