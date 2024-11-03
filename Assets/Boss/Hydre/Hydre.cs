using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hydre : Enemy
{
    private Shake shake;
    private int counter;
    private bool isInvulnerable;
    private Animator animator;
    public GameObject skill1Collider;
    public GameObject skill2Collider;
    [SerializeField]
    private int DamageSkill2;
    [SerializeField]
    private Slider slider;

    private bool isDead;

    public AudioManager AudioManager;
    void Start()
    {
        isDead = false;
        shake = GameObject.FindWithTag("ScreenShake").GetComponent<Shake>();
        counter = 0;
        isInvulnerable = true;
        SetMaxHealth(_health);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(20);
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void IncrementAnimationCounter()
    {
        counter = counter + 1;
        animator.SetInteger("counter",counter);
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
                counter = 0;
                Damage = Damage * 2;
                DamageSkill2 = DamageSkill2 * 2;
                animator.SetInteger("counter",0);
                animator.SetBool("IsEnraged",true);
            }
            if (_health<=0 && isDead==false)
            {
                isDead = true;
                AudioManager.Pause("BossFightMusic");
                Die();
            }
        }
        
    }

    protected override void Die()
    {
        animator.SetTrigger("dead");
        //VictoryManager.instance.WhenBossDeath();
        //Destroy(gameObject);
    }

    public void TriggerVictory()
    {
        VictoryManager.instance.WhenBossDeath();
    }
    
    public bool isInAttackCollider(GameObject attackCollider)
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

    

    protected void AttackSkill1()
    {
        if (isInAttackCollider(skill1Collider))
        {
            StartCoroutine(_player.TakeDMG(Damage));
        }
        
        
    }
    public void AttackSkill2()
    {
        if (isInAttackCollider(skill2Collider))
        {
            StartCoroutine(_player.TakeDMG(DamageSkill2));
        }

    }

    public void PlayGroundBreakSound()
    {
        AudioManager.Play("GroundShake");
    }

    

    public void PlayHydreScream()
    {
        AudioManager.Pause("BossFightMusic");
        AudioManager.Play("HydreScream");
    }

    public void WalkShake()
    {
        shake.bossWalkCamShake();
    }
    public void ScreamShake()
    {
        shake.bossScreamCamShake();
    }

}
