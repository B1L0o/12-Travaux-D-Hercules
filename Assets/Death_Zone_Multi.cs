using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Zone_Multi : MonoBehaviour
{
    public GameObject playerSpawn;
    public Animator FadeSystem;

    public Player2 player2;
    public int damage;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")||col.CompareTag("p2"))
        {
            StartCoroutine(player2.TakeDMG(damage));
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
