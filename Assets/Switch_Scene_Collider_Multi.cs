using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch_Scene_Collider_Multi : MonoBehaviour
{
    public string sceneToGo;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")||col.CompareTag("p2"))
        {
            //VictoryManager.instance.WhenBossDeath();
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(sceneToGo);
        }
    }
}
