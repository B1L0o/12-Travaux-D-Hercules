using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int value;
    public Animator FadeSystem;

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FadeSystem.SetTrigger("FadeIn");
            Destroy(gameObject);
            Counter.instance.IncreaseWalls();
        }
        
    }
}
