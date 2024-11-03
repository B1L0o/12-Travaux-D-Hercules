using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUpMulti : MonoBehaviour
{
    public Player2 _player2;
    public int amount;
    public AudioSource Heal;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.CompareTag("Player")||col.CompareTag("p2")) && _player2.health != _player2.maxHealth)
        {
            Heal.Play();
            _player2.HealPlayer(amount);
            Destroy(gameObject);
        }
        
    }
}
