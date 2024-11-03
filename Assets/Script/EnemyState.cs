using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    
    public abstract void InitialiseState();
    public abstract void ApplyState();
    
    
    
}

public class Patrol : EnemyState
{
    protected Transform[] _waypoints;
    protected int _index_next_target=0;
    protected Transform _target;
    protected GameObject _player;
    protected GameObject _ennemy;
    protected float _speed;
    protected bool _isRightOriented;
    
    

    public Patrol(Transform[] waypoint,float speed, GameObject ennemy, GameObject player,bool isRightOriented)
    {
        _waypoints = waypoint;
        _speed = speed;
        _ennemy = ennemy;
        _player = player;
        _isRightOriented = isRightOriented;

    }
    public override void InitialiseState()
    {
        _target = _waypoints[0];
    }
    public void LookAtSomething(Transform transformToLookAt,bool isRightOriented)
    {
        Vector3 scale = _ennemy.transform.localScale;
        if (transformToLookAt.position.x>_ennemy.transform.position.x)
        {
            if (isRightOriented)
            {
                scale.x = Math.Abs(scale.x);
            }
            else
            {
                scale.x = Math.Abs(scale.x)*-1;
            }
            
        }
        else
        {
            if (isRightOriented)
            {
                scale.x = Math.Abs(scale.x)*-1;
            }
            else
            {
                scale.x = Math.Abs(scale.x);
            }
        }

        _ennemy.transform.localScale = scale;

    }

    
    public override void ApplyState()
    {
        LookAtSomething(_target.transform,_isRightOriented);
        Vector3 direction = _target.position - _ennemy.transform.position;
        _ennemy.transform.Translate(direction.normalized * _speed * Time.deltaTime,Space.World);
        if (Vector3.Distance(_ennemy.transform.position,_target.position)<0.3f)
        {
            _index_next_target = (_index_next_target + 1) % _waypoints.Length;
            _target = _waypoints[_index_next_target];
            //_ennemy.GetComponent<SpriteRenderer>().flipX = !(_ennemy.GetComponent<SpriteRenderer>().flipX);
        }
    }

   
}

public class FollowPlayer : EnemyState
{
    private Pathfinding pathfinding;
    public List<Vector3> path
    {
        get;
        set;

    } 
    private int _width;
    private int _height;
    private float _speed;
    private bool _DebugMode;
    private GameObject _player;
    private GameObject _ennemy;
    private Transform _waypoint0;
    private int _groundy;
    private bool _isRightOriented;
    private int j;
    public FollowPlayer(int width, int height,float speed,bool debugMode,GameObject player, GameObject ennemy,Transform waypoint0,int groundy,bool isRightOriented)
    {
         
        _width = width;
        _height = height;
        _speed = speed;
        _DebugMode = debugMode;
        _player = player;
        _ennemy = ennemy;
        _waypoint0 = waypoint0;
        _groundy = groundy;
        _isRightOriented = isRightOriented;

    }
    public void LookAtSomething(Transform transformToLookAt,bool isRightOriented)
    {
        Vector3 scale = _ennemy.transform.localScale;
        if (transformToLookAt.position.x>_ennemy.transform.position.x)
        {
            if (isRightOriented)
            {
                scale.x = Math.Abs(scale.x);
            }
            else
            {
                scale.x = Math.Abs(scale.x)*-1;
            }
            
        }
        else
        {
            if (isRightOriented)
            {
                scale.x = Math.Abs(scale.x)*-1;
            }
            else
            {
                scale.x = Math.Abs(scale.x);
            }
        }

        _ennemy.transform.localScale = scale;

    }

    public override void InitialiseState()
    {
        //_ennemy.GetComponent<SpriteRenderer>().flipX = true;
        pathfinding = new Pathfinding(_width, _height,new Vector3((int)_waypoint0.transform.position.x-2,_groundy),_DebugMode);
    }
    
    
    public override void ApplyState()
    {
        //_ennemy.GetComponent<SpriteRenderer>().flipX = _ennemy.transform.position.x > _player.transform.position.x;
        j = 0;
        path = pathfinding.FindPath(_ennemy.transform.position, _player.transform.position);
        LookAtSomething(_player.transform,_isRightOriented);
        if (path != null)
        {
            if (_DebugMode)
            {
                for (int i=0;i+1<path.Count;i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x,path[i].y),new Vector3(path[i+1].x,path[i+1].y),Color.green,0.5f,false);
                }
            }
            
            float distance = Vector2.Distance(_ennemy.transform.position,path[j]);
            while (distance>=3f && j<path.Count)
                {
                    
                    //_ennemy.GetComponent<SpriteRenderer>().flipX = _ennemy.transform.position.x > _player.transform.position.x;
                    Vector3 moveDirection = (path[j] +new Vector3(0.5f,0.5f) - _ennemy.transform.position);
                    _ennemy.transform.position += moveDirection.normalized * _speed * Time.deltaTime;
                    distance = Vector2.Distance(_ennemy.transform.position,path[j]);
                }
            if(j+1<path.Count)
            {
                //_ennemy.GetComponent<SpriteRenderer>().flipX = _ennemy.transform.position.x > _player.transform.position.x;
                Vector3 moveDirection = (path[j+1] +new Vector3(0.5f,0.5f) - _ennemy.transform.position);
                _ennemy.transform.position += moveDirection.normalized * _speed * Time.deltaTime;
               
            }
            
        }
    }
}
