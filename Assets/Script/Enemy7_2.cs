using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7_2 : Enemy
{
    private Animator _animator;
    [SerializeField]
    private GameObject _attackCollider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void AttackAnimationEvent()
    {
        AttackWithoutCoolDown(_attackCollider.GetComponent<Collider2D>());
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
    public override void TakeDamage(int damage)
    {
        _animator.SetTrigger("explode");
    }
    
    
}
