using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : Enemy
{
    [SerializeField]
    private GameObject _attackCollider;
    public void AttackAnimationEvent()
    {
        AttackWithoutCoolDown(_attackCollider.GetComponent<Collider2D>());
    }

    public EnemyState GetEnemyState()
    {
        return _enemyState;
    }

    public void SetEnemyState(EnemyState enemyState)
    {
        _enemyState = enemyState;
    }

    public bool GetOrientation()
    {
        return isRightOriented;
    }
    
}
