using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    public float rockSpeed;
    public int damage;
    public Player player;
    private Rigidbody2D rb;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction*rockSpeed;
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Tilemap"))
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(player.TakeDMG(damage));

            }
            Destroy(gameObject);
        }
       
    }
}
