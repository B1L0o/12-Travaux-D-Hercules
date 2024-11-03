using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBallMulti : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private Rigidbody2D rb;
    public Player2 _player2;
    public Player2join _player2Join;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction*speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("p2")||col.CompareTag("Tilemap"))
        {
            if (col.CompareTag("Player")|| col.CompareTag("p2"))
            {
                int chooseEffect = Random.Range(0, 2);
                if (chooseEffect==0)
                {
                    _player2.StartPoisonEffectHealth(damage,15,2);
                }
                else
                {
                    _player2.StartPoisonEffectSpeed(200,10);
                    _player2Join.StartPoisonEffectSpeed(200,10);

                }
                

            }
            Destroy(gameObject);
        }
       
    }
}
