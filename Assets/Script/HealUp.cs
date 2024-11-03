
using UnityEngine;

public class HealUp : MonoBehaviour
{
    public Player _player;
    public int amount;
    public AudioSource Heal;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && _player.health != _player.maxHealth)
        {
            Heal.Play();
            _player.HealPlayer(amount);
            Destroy(gameObject);
        }
        
    }
}
