using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaf : MonoBehaviour
{
    private int velocity;

    private int alive;
    private bool wind;
    // Start is called before the first frame update
    void Start()
    {
        velocity = 0;
        alive = 10000;
        wind = false;
    }

    private void OnTriggerEnter2D(Collider2D p)
    {
        string name = p.name;
        if (name=="windc")
        {
            wind = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D p)
    {

        if (p.name == "windc")
        {
            wind = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (alive > 0)
        {
            alive--;
            if (wind)
            {
                velocity++;
            }
            else
            {
                if (velocity > 0)
                {
                    velocity--;
                }
            }

            transform.position = transform.position + new Vector3((float)0.0001 * velocity, (float)-0.003, 0);
        }
    }
}
