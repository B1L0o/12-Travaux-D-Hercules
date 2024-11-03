using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    public GameObject image;
    public GameObject startpos;
    public GameObject currentpos;
    public int speed;

    public float height;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentpos.transform.position.x - startpos.transform.position.x >= 2560)
        {
            currentpos.transform.position = new Vector3(startpos.transform.position.x,0,0);
        }

        image.transform.position = new Vector3(currentpos.transform.position.x,height,0);
        currentpos.transform.position = new Vector3(currentpos.transform.position.x + speed/100f,0,0);



    }
}
