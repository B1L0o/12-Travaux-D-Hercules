using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2 : Enemy
{
    private bool isabletopatrol;
    [SerializeField] 
    private bool _DebugMode;
    [SerializeField] 
    private int _width;
    [SerializeField]
    private int _height;

    [SerializeField] 
    private int _groundy;
    protected void Start()
    {
        isabletopatrol = true;
        SetState(new Patrol(_waypoints,_speed,gameObject,_player.gameObject,isRightOriented));
        _enemyState.InitialiseState();
    }
    protected void Update()
    {
        AttackWithOwnCollider();
        _enemyState.ApplyState();
        if ((_enemyState is Patrol||isabletopatrol==false) && _player.transform.position.x >= _waypoints[0].transform.position.x && _player.transform.position.x <= _waypoints[1].transform.position.x)
        {
            isabletopatrol = true;
            SetState(new FollowPlayer(_width,_height,_speed,_DebugMode,_player.gameObject,gameObject,_waypoints[0],_groundy,isRightOriented));
            _enemyState.InitialiseState();

        }
        if (_enemyState is FollowPlayer &&(isabletopatrol==false || (_player.transform.position.x < _waypoints[0].transform.position.x||_player.transform.position.x > _waypoints[1].transform.position.x)))
        {
            if (isabletopatrol)
            {
                isabletopatrol = false;
                SetState(new FollowPlayer(_width,_height,_speed,_DebugMode,_waypoints[0].gameObject,gameObject,_waypoints[0],_groundy,isRightOriented));
                _enemyState.InitialiseState();   
            }
            float distance = Vector3.Distance(transform.position,_waypoints[0].transform.position);
            if(distance<=1.5f)
            {
                SetState(new Patrol(_waypoints,_speed,gameObject,_player.gameObject,isRightOriented));
                _enemyState.InitialiseState();
            }
        }
        
    }
    
    
}
