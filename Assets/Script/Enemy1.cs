using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    
    protected void Start()
    {
        if (_player2 is not null)
        {
            SetState(new Patrol(_waypoints,_speed,gameObject,_player2.gameObject,isRightOriented));
        }
        else
        {
            SetState(new Patrol(_waypoints,_speed,gameObject,_player.gameObject,isRightOriented));
        }
        _enemyState.InitialiseState();
    }

    protected void Update()
    {
        _enemyState.ApplyState();
        AttackWithOwnCollider();
    }
    

    

   
}


