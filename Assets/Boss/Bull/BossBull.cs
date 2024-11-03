using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBull : Enemy
{
    public Camera playerCamera;
    public AudioManager AudioManager;
    public GameObject attack4Collider;
    public GameObject attack1Collider;
    public GameObject attack2Collider;
    [SerializeField]
    private int damageAttack2;
    [SerializeField]
    private int damageAttack4;
    private bool isInvulnerable;
    private Animator animator;
    private int counter;
    [SerializeField]
    private Slider slider;

    private bool isDead;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        counter = 0;
        animator = GetComponent<Animator>();
        SetMaxHealth(_health);
        isInvulnerable = true;
        AudioManager.Play("BossFightMusic");
    }
    
    
    public void IncrementAnimationCounter()
    {
        counter = counter + 1;
        animator.SetInteger("counter",counter);
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
            if (_health<=2000 && animator.GetBool("IsEnraged")==false)
            {
                counter = 0;
                //Damage = Damage * 2;
                //damageAttack2 = damageAttack2 * 2; 
                animator.SetInteger("counter",0);
                SetIsInvulnerable(true);
                animator.SetBool("IsEnraged",true);
            }
            if (_health<=0&&isDead==false)
            {
                isDead = true;
                //AudioManager.Pause("BossFightMusic");
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
    protected void Attack1()
    {
        if (isInAttackCollider(attack1Collider))
        {
            StartCoroutine(_player.TakeDMG(Damage));
        }
        
    }

    
    
    public void Attack2()
    {
        if (isInAttackCollider(attack2Collider))
        {
            StartCoroutine(_player.TakeDMG(damageAttack2));

        }

    }
    public void Attack4()
    {
        if (isInAttackCollider(attack4Collider))
        {
            StartCoroutine(_player.TakeDMG(damageAttack4));
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(20);
        }
    }
}
