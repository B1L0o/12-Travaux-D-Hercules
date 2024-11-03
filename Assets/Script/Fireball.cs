using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public bool orientedToPlayer;
    public float bulletSpeed;
    public int Damage;
    public Player _player;
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
            Vector3 relativePos = _player.transform.position - transform.position;
            float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        explosionEffect.GetComponent<Transform>().localScale = GetComponent<Transform>().localScale;
    }
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Tilemap"))
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(_player.TakeDMG(Damage));

            }
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       
    }
}
