using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D projRigidBody;
    public SpriteRenderer spriteRenderer;
    public Player _player;

    void Start()
    {
    
    projRigidBody = GetComponent<Rigidbody2D>();
    projRigidBody.velocity = transform.right * projectileSpeed;
    if (_player.spriteRenderer.flipX == false)
    {
        spriteRenderer.flipX = false;
        projRigidBody.velocity = transform.right * projectileSpeed;
    }
    else
    {
        projRigidBody.velocity = transform.right * -projectileSpeed ;
        spriteRenderer.flipX = true;
    }
    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("enemy") || col.CompareTag("Tilemap"))
        {
            if (col.CompareTag("enemy"))
            {
                col.GetComponent<Enemy>().TakeDamage(10);

            }
            Destroy(gameObject);
        }
       
    }
}
