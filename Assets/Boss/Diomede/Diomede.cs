using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Diomede : Enemy
{
    [SerializeField] 
    private Volume postProcessing;
    private KnockBack knockBack;
    private bool isDead;
    private Animator animator;
    private int counter;
    public GameObject skill1Collider;
    public GameObject skill2Collider;
    [SerializeField]
    private int DamageSkill2;
    [SerializeField]
    private Slider slider;
    private bool isInvulnerable;

    public AudioManager AudioManager;
    void Start()
    {
        isInvulnerable = true;
        knockBack = _player.GetComponent<KnockBack>();
        isDead = false;
        animator = GetComponent<Animator>();
        counter = 0;
        SetMaxHealth(_health);
        AudioManager.Play("BossFightMusic");
    }
    public void SetIsInvulnerable(bool _isInvulnerable)
    {
        isInvulnerable = _isInvulnerable;
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
    
    

    
    public override void TakeDamage(int damage)
    {
        if (isInvulnerable == false)
        {
            _health = _health - damage;
            SetHealth(_health);
            if (_health<=2000 && animator.GetBool("IsEnraged")==false)
            {
                counter = 0;
                //Damage = Damage * 2;
                DamageSkill2 = DamageSkill2 * 4;
                animator.SetInteger("counter",0);
                SetIsInvulnerable(true);
                knockBack.strength = knockBack.strength * 4;
                postProcessing.enabled = true;
                animator.SetBool("IsEnraged",true);
                AudioManager.Pause("BossFightMusic");
                AudioManager.Play("BossBattle2");
               

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
        animator.SetTrigger("death");
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
    

    public void EnablePlayer()
    {
        _player.GetComponent<Player>().enabled = true;
    }
    public void DisablePlayer()
    {
        _player.GetComponent<Player>().enabled = false;
    }
    public void DoKnockBackPlayer()
    { 
       knockBack.PlayFeedback(this.gameObject);
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
            DoKnockBackPlayer();
            StartCoroutine(_player.TakeDMG(DamageSkill2));
        }

    }
}
