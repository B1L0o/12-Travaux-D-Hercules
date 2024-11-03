using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3_Multi : Enemy
{
    private bool canShoot;
    public Transform firePos;
    public GameObject bullet;
    private Animator _animator;
    [SerializeField]
    private float range;
    private Transform fireBallTransform;
    private bool isPlayer2;
    

    protected void Update()
    {
        if (Vector2.Distance(_player2.transform.position,transform.position)<range&&Mathf.Abs(_player2.transform.position.y-transform.position.y)<1 && IsLookingAtSomething(_player2.transform)&&canShoot)
        {
            isPlayer2 = true;
            _animator.Play("ennemi3_shoot", -1, 0f); 
            AttackWithOwnCollider();
        }
        else if (Vector2.Distance(_player2Join.transform.position,transform.position)<range&&Mathf.Abs(_player2Join.transform.position.y-transform.position.y)<1 && IsLookingAtSomething(_player2Join.transform)&&canShoot)
        {
            isPlayer2 = false;
            _animator.Play("ennemi3_shoot", -1, 0f); 
            AttackWithOwnCollider();
        }
        
    }
    
    public void Shoot()
    {
        GameObject fireBall =  Instantiate(bullet,firePos.position,firePos.rotation);
        FireballMulti fireballObject = fireBall.GetComponent<FireballMulti>();
        fireballObject.orientedToPlayer = false;
        if (fireBallTransform.lossyScale.x>0)
        {
            fireballObject.direction = new Vector3(1f,0f,0f);
            fireballObject.GetComponent<Transform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);


        }
        else if(fireBallTransform.lossyScale.x<0)
        {
            fireballObject.direction = new Vector3(-1f,0f,0f);
            fireballObject.GetComponent<Transform>().localScale = new Vector3(-0.3f, 0.3f, 0.3f);


        }

        if (isPlayer2)
        {
            fireballObject._player2 = _player2;
        }
        else
        {
            fireballObject._player2Join = _player2Join;

        }
        
        fireballObject.bulletSpeed = _speed;
        fireballObject.Damage = Damage;
    }
    
    protected void Start()
    {
        canShoot = true;
        _animator = GetComponent<Animator>();
        fireBallTransform = GetComponent<Transform>();
        
    }
    
    protected override void Attack(Collider2D coll)
    {
       
        StartCoroutine(Attack_Coroutine());
        IEnumerator Attack_Coroutine()
        {
            canShoot = false;
            yield return new WaitForSeconds(3);
            canShoot = true;
        }
    }
}
