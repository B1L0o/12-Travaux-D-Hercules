using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMulti : MonoBehaviour
{
    public bool orientedToPlayer;
    public float bulletSpeed;
    public int Damage;
    public Player2 _player2;
    public Player2join _player2Join;
    private Rigidbody2D rb;
    public GameObject explosionEffect;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Vector2 target = (_player.transform.position - transform.position).normalized;
        rb.velocity = direction*bulletSpeed;
        //rb.velocity = target*bulletSpeed;
        if (orientedToPlayer)
        {
            Vector3 relativePos;
            if (_player2 is null)
            {
                relativePos = _player2Join.transform.position - transform.position;
            }
            else
            {
                relativePos = _player2.transform.position - transform.position;

            }
            float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        explosionEffect.GetComponent<Transform>().localScale = GetComponent<Transform>().localScale;
    }
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("p2")||col.CompareTag("Tilemap"))
        {
            if (col.CompareTag("Player") || col.CompareTag("p2"))
            {
                if (_player2 is null)
                {
                    StartCoroutine(_player2Join.TakeDMG(Damage));
                }
                else
                {
                    StartCoroutine(_player2.TakeDMG(Damage));

                }

            }
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       
    }
}

