using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected bool isRightOriented;
    [SerializeField] 
    protected float cooldown = 1f;
    protected float lastAttackedTime = -9999f;
    public int Damage;
    public float _speed;
    [SerializeField]
    protected int _health;
    public Player _player;
    public Player2 _player2;
    public Player2join _player2Join;
    protected EnemyState _enemyState;
    [SerializeField]
    public Transform[] _waypoints;
    [SerializeField] 
    private Material _flashMaterial;
    [SerializeField] 
    private float _flashDuration;

    private bool _isflashing=false;

    
    
    public virtual void TakeDamage(int damage)
    {
        Flash();
        _health = _health - damage;
        if (_health<=0)
            {
                Die();
            }
    }

    protected virtual void Die()
    {
        Destroy(transform.parent.gameObject);
    }
    
    protected void SetState(EnemyState enemyState)
    {
        _enemyState = enemyState;
    }

    
    protected virtual void Attack(Collider2D coll)
    {
        if (CanAttack(coll))
        {
            if (Time.time > lastAttackedTime + cooldown)
            {
                if (_player2 is not null)
                {
                    StartCoroutine(_player2.TakeDMG(Damage));
                }
                else if (_player2Join is not null)
                {
                    StartCoroutine(_player2Join.TakeDMG(Damage));

                }
                else
                {
                    StartCoroutine(_player.TakeDMG(Damage));
                }
                lastAttackedTime = Time.time;
            }
        }
        
    }

    protected void AttackWithOwnCollider()
    {
        Attack(GetComponent<Collider2D>());
    }
    protected void AttackWithOwnColliderAndWithoutCoolDown()
    {
        AttackWithoutCoolDown(GetComponent<Collider2D>());
    }
    protected virtual void AttackWithoutCoolDown(Collider2D coll)
    {
        if (CanAttack(coll))
        {
            if (_player2 is not null)
            {
                StartCoroutine(_player2.TakeDMG(Damage));
            }
            else if (_player2Join is not null)
            {
                StartCoroutine(_player2Join.TakeDMG(Damage));

            }
            else
            {
                StartCoroutine(_player.TakeDMG(Damage));
            }
        }
    }
    

    protected virtual bool CanAttack(Collider2D coll)
    {
        //Collider2D coll = GetComponent<Collider2D>();
        List<Collider2D> result = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        int nb_collider = Physics2D.OverlapCollider(coll,filter,result);
        foreach (var collider in result)
        {
            if (collider.tag == "Player" || collider.tag == "p2")
            {
                return true;
            }
        }

        return false;
    }
    protected void Flash()
    {
        if (_isflashing == false)
        {
            StartCoroutine(FlashRoutine());
        }

    }
    protected IEnumerator FlashRoutine()
    {
        _isflashing = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Material originalMaterial = spriteRenderer.material;
        spriteRenderer.material = _flashMaterial;
        yield return new WaitForSeconds(_flashDuration);
        spriteRenderer.material = originalMaterial;
        _isflashing = false;
    }
    public void LookAtPlayer()
    {
        Vector3 scale = transform.localScale;
        if (_player.transform.position.x>transform.position.x)
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

        transform.localScale = scale;

    }

    public bool IsLookingAtSomething(Transform transformToLookAt)
    {
        if (transformToLookAt.position.x > transform.position.x)
        {
            if (isRightOriented)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (isRightOriented)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    public void LookAtSomething(Transform transformToLookAt)
    {
        Vector3 scale = transform.localScale;
        if (transformToLookAt.position.x>transform.position.x)
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

        transform.localScale = scale;

    }

}
