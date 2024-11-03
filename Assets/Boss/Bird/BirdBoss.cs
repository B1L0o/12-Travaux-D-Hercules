using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirdBoss : MonoBehaviour
{
    public Transform explosion;
    public SpawerBird SpawerBird;
    public Camera playerCamera;
    private float speed;
    private Vector2 direction;
    public float cooldown = 1f;
    private float lastAttackedTime = -9999f;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        Destroy(gameObject);
        GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
        Destroy(exploder, 2.0f);
        SpawerBird.IncreaseNumberOfBirdsKilled();
        
    }

    
    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = playerCamera.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (!onScreen)
        {
            SpawerBird.IncreaseNumberOfBirdsLeft();
           
            Destroy(gameObject);
        }
        if (Time.time > lastAttackedTime + cooldown)
        {
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
            if (direction.x<0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;

            }
            lastAttackedTime = Time.time;
            speed = Random.Range(1,4);
        }
        transform.Translate(direction*speed * Time.deltaTime,Space.World);
    }
}
