using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windgenerator : MonoBehaviour
{
    private int a;
    private int b;
    private System.Random rdm;

    public GameObject prefab;

    public GameObject prefal;
    // Start is called before the first frame update
    void Start()
    {
        rdm = new System.Random();
        a = 1200;
        b = 5;
    }

    public void generatewind()
    {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void generateleaf()
    {
        Instantiate(prefal, new Vector3(rdm.Next(-15,128)*(float)rdm.NextDouble(), 7, rdm.Next(2,4)), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        a--;
        b--;
        if (a<0)
        {
            a = rdm.Next(5000);
            generatewind();
        }

        if (b<0)
        {
            b = 30;
            generateleaf();
        }
    }
}
