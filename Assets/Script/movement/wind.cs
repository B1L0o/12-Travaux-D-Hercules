using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    private int alive;
    // Start is called before the first frame update
    void Start()
    {
        alive = 100000;
    }
    // Update is called once per frame
    void Update()
    {
        if (alive != 0)
        {
            alive--;
            transform.position = transform.position + new Vector3((float)0.01,0,0);
        }
    }
}
