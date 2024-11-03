using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class deathZone : MonoBehaviour
{
    public GameObject playerSpawn;
    public Animator FadeSystem;

    public Player player;
    public int damage;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(player.TakeDMG(damage));
            StartCoroutine(fade());

            col.transform.position = playerSpawn.transform.position;

        }
    }
    
    public IEnumerator fade()
    {
       
        FadeSystem.SetTrigger("FadeIn");
        yield return  new WaitForSeconds(0.1f);
    }

}
