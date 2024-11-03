using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6Multi : Enemy
{
    private bool isDead;
    private Animator _animator;
    [SerializeField]
    private float range;
    [SerializeField]
    private GameObject poisonBall;
    [SerializeField]
    private Transform poisonBallPosition;
    [SerializeField]
    private GameObject ennemySpawnAfterDeath;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(IsLookingAtSomething(_player.transform));
        if (Vector2.Distance(_player2.transform.position,transform.position)<range&&Mathf.Abs(_player2.transform.position.y-transform.position.y)<1 && IsLookingAtSomething(_player2.transform))
        {
            if (Time.time > lastAttackedTime + cooldown)
            {
                _animator.SetTrigger("attack");
                lastAttackedTime = Time.time;
            }
        }
        else if (Vector2.Distance(_player2Join.transform.position,transform.position)<range&&Mathf.Abs(_player2Join.transform.position.y-transform.position.y)<1 && IsLookingAtSomething(_player2Join.transform))
        {
            if (Time.time > lastAttackedTime + cooldown)
            {
                _animator.SetTrigger("attack");
                lastAttackedTime = Time.time;
            }
        }
    }
    
    public void Shoot()
    {
        GameObject poisonFireBall =  Instantiate(poisonBall,poisonBallPosition.position,poisonBallPosition.rotation);
        PoisonBallMulti poisonBallObject = poisonFireBall.GetComponent<PoisonBallMulti>();
        poisonBallObject.direction = -transform.right;
        poisonBallObject._player2Join = _player2Join;
        poisonBallObject._player2 = _player2;
        poisonBallObject.speed = _speed;
        poisonBallObject.damage = Damage;

    }
    
    public override void TakeDamage(int damage)
    {
        Flash();
        _health = _health - damage;
        if (_health<=0 &&isDead==false)
        {
            isDead = true;
            _animator.SetTrigger("death");
        }
        
    }
    
    protected override void Die()
    {
        for (int i = 0; i < Random.Range(1,5); i++)
        {
            GameObject enemy7 = Instantiate(ennemySpawnAfterDeath,poisonBallPosition.position,Quaternion.Euler(new Vector3(0, 0, 0)));
            Enemy7_2 enemy7Object = enemy7.GetComponent<Enemy7_2>();
            if (Vector2.Distance(_player2Join.transform.position,transform.position)<Vector2.Distance(_player2.transform.position,transform.position))
            {
                enemy7Object._player2Join = _player2Join;
            }
            else
            {
                enemy7Object._player2 = _player2;
            }
            enemy7Object.Damage = Random.Range(5, 41);
            enemy7Object._speed = Random.Range(1f, 6f);
            
        }
        Destroy(transform.parent.gameObject);
    }
}
