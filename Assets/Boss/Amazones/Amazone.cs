using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amazone : Enemy,IPooledObject
{
    private bool isDead;
    private Animator animator;
    [SerializeField] 
    private Collider2D attackCollider1;
    [SerializeField] 
    private Collider2D attackCollider2;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        isDead = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tag = "enemy";
        animator.SetBool("restart",true);
        enabled = true;
    }

    public void SetHealth(int health)
    {
        _health = health;
    }
    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        if (CanAttack(attackCollider2))
        {
            animator.SetBool("attack",true);
        }
        else
        {
            animator.SetBool("attack",false);
            Vector2 target = new Vector2(_player.transform.position.x, transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, _speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }
    public void AttackAnimationEvent1()
    {
        AttackWithoutCoolDown(attackCollider1);
    }
    public void AttackAnimationEvent2()
    {
        AttackWithoutCoolDown(attackCollider2);
    }

    protected override void Die()
    {
        animator.SetTrigger("death");
        enabled = false;
        tag = "Untagged";
        animator.SetBool("restart",false);

    }
    
    public override void TakeDamage(int damage)
    {
        Flash();
        _health = _health - damage;
        if (_health<=0 && isDead==false)
        {
            isDead = true;
            Die();
        }
    }

    
}
