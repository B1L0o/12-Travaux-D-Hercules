using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class collider : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D p)
    {
        string name = p.name;
        if (name=="windc")
        {
            lunch();
        }
    }
    
    private void OnTriggerExit2D(Collider2D p)
    {

        if (p.name == "windc")
        {
            stop();
        }
    }
    
    public void lunch()
    {
        animator.SetBool("wind", true);
    }

    public void stop()
    {
        animator.SetBool("wind", false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
