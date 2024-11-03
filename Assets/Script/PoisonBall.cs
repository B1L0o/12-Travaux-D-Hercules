using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBall : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private Rigidbody2D rb;
    public Player player;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction*speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Tilemap"))
        {
            if (col.CompareTag("Player"))
            {
                int chooseEffect = Random.Range(0, 2);
                if (chooseEffect==0)
                {
                    player.StartPoisonEffectHealth(damage,15,2);
                }
                else
                {
                    player.StartPoisonEffectSpeed(200,10);
                }
                

            }
            Destroy(gameObject);
        }
       
    }
}
