using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SwitchSceneCollider : MonoBehaviour
{
    public string sceneToGo;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")||col.CompareTag("p2"))
        {
            //VictoryManager.instance.WhenBossDeath();
            SceneManager.LoadScene(sceneToGo);
        }
    }
}


