using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserPosition;
    public float laserDistance;
    public Vector2 direction;
    public Player player;
    public int damage;
    void Update()
    {
        Debug.DrawRay(laserPosition.position,direction*laserDistance,Color.yellow);
        
    }

    public void Attack()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(laserPosition.position, direction,laserDistance);
        if (hit.Length != 0)
        {
            foreach (var raycastHit2D in hit)
            {
                if (raycastHit2D.transform.CompareTag("Player"))
                {
                    StartCoroutine(player.TakeDMG(damage));
                }
            }
        }
    }
    
    public void DestroyLaser()
    {
        Destroy(gameObject);
    }
}
