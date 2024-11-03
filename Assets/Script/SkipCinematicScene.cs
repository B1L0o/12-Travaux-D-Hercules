using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCinematicScene : MonoBehaviour
{
    public GameObject objectThatSkipScene;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            objectThatSkipScene.SetActive(true);
        } 
    }
}
