using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn_Multi : MonoBehaviour
{
    void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        GameObject.FindGameObjectWithTag("p2").transform.position = transform.position;

    }
}
