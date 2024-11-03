using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Enemy
{
    private bool IsInvulnerable;
    [SerializeField] 
    private bool _DebugMode;
    [SerializeField] 
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField] 
    private float AttackRange;

    [SerializeField] 
    private int _groundy;
    // Start is called before the first frame update
    void Start()
    {
        IsInvulnerable = true;
        SetState(new FollowPlayer(_width,_height,_speed,_DebugMode,_player.gameObject,gameObject,_waypoints[0],_groundy,isRightOriented));
        _enemyState.InitialiseState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(20);
        }
    }

    public void Jump()
    {
        transform.position +=  (2.7f*Vector3.up)  * _speed * Time.deltaTime;
    }
    public EnemyState GetEnemyState()
    {
        return _enemyState;
    }

    public void SetEnemyState(float speed)
    {
        SetState(new FollowPlayer(_width,_height,speed,_DebugMode,_player.gameObject,gameObject,_waypoints[0],_groundy,isRightOriented));
        _enemyState.InitialiseState();
    }

    public void SetAttackRange(int attackrange)
    {
        AttackRange = attackrange;
    }

    public void SetIsInvulnerable(bool isInvulnerable)
    {
        IsInvulnerable = isInvulnerable;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public float GetAttackRange()
    {
        return AttackRange;
    }
    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }

    public override void TakeDamage(int damage)
    {
        if (IsInvulnerable==false)
        {
            _health = _health - damage;
            Flash();
            if (_health<=250)
            {
                GetComponent<Animator>().SetBool("IsEnraged",true);
            }
            if (_health<=0)
            {
                Die();
            }
        }
        
    }

    protected override void Die()
    {
        VictoryManager.instance.WhenBossDeath();
        Destroy(gameObject);
    }


    protected  void Attack()
    {
        Collider2D collider2DInfo = Physics2D.OverlapCircle(transform.position, AttackRange);
        if (collider2DInfo != null)
        {
            StartCoroutine(_player.TakeDMG(Damage));
        }
    }
    
}
