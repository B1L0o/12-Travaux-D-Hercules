using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    public float heigth;
    
   void Start()
   {
       startpos = transform.position.x;
       length = GetComponent<SpriteRenderer>().bounds.size.x;
   }

    // Update is called once per frame
    void FixedUpdate() 
    {
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, heigth, transform.position.z);
    }
}
