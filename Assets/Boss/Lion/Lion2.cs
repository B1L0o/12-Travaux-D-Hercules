using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lion2 : Enemy
{
    private bool isInvulnerable;
    [SerializeField] 
    private GameObject attackCollider;
    [SerializeField]
    private Slider slider;
    public AudioManager AudioManager;
    private Animator animator;
    private bool isDead;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(20);
        }
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    private void Start()
    {
        isDead = false;
        animator = GetComponent<Animator>();
        isInvulnerable = false;
        SetMaxHealth(_health);
        AudioManager.Play("BossFightMusic");
    }
    
    public void SetIsInvulnerable(bool _isInvulnerable)
    {
        isInvulnerable = _isInvulnerable;
    }

    
    
    public override void TakeDamage(int damage)
    {
        if (isInvulnerable==false)
        {
            _health = _health - damage;
            SetHealth(_health);
            if (_health<=2000 &&animator.GetBool("IsEnraged")==false)
            {
                Damage = Damage * 2;
                SetIsInvulnerable(true);
                animator.SetBool("IsEnraged",true);
            }
            if (_health<=0&&isDead==false)
            {
                isDead = true;
                AudioManager.Pause("BossFightMusic");
                Die();
            }
        }
        
    }
    protected override void Die()
    {
        transform.position = new Vector3(transform.position.x, -24.5f, transform.position.z);
        animator.SetTrigger("dead");
        //VictoryManager.instance.WhenBossDeath();
        //Destroy(gameObject);
    }

    protected void PlayLionRoar()
    {
        AudioManager.Play("lionRoar");
    }

    
    public void TriggerVictory()
    {
        VictoryManager.instance.WhenBossDeath();
    }
    public bool isInAttackCollider()
    {
        Collider2D coll =  attackCollider.GetComponent<Collider2D>();
        List<Collider2D> result = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        int nb_collider = Physics2D.OverlapCollider(coll,filter,result);
        foreach (var collider in result)
        {
            if (collider.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }
    protected void AttackSkill()
    {
        if (isInAttackCollider())
        {
            StartCoroutine(_player.TakeDMG(Damage));
        }
        
    }
}