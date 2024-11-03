using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Geryon : Enemy
{
    private bool isDead;
    private Animator animator;
    private int counter;
    public GameObject attack1Collider;
    [SerializeField]
    private Slider slider;
    private bool isInvulnerable;
    public AudioManager AudioManager;
    [SerializeField] 
    private Transform rockPosition;
    [SerializeField] 
    private float rockSpeed;
    [SerializeField] 
    private GameObject laserPrefab;
    [SerializeField] 
    private Transform laserPosition;
    [SerializeField] 
    private float laserDistance;
    [SerializeField] 
    private int laserDamage;
    [SerializeField] 
    private GameObject rockPrefab;
    private Transform bossTransform;
    void Start()
    {
        isInvulnerable = true;
        isDead = false;
        animator = GetComponent<Animator>();
        counter = 0;
        SetMaxHealth(_health);
        AudioManager.Play("BossFightMusic");
        bossTransform = GetComponent<Transform>();
    }
    
    public void ShootRock()
    {
        GameObject rock =  Instantiate(rockPrefab,rockPosition.position,rockPosition.rotation);
        rock rockObject = rock.GetComponent<rock>();
        if (bossTransform.localScale.x>0)
        {
            rockObject.direction = new Vector3(1f,0f,0f);

        }
        else if(bossTransform.localScale.x<0)
        {
            rockObject.direction = new Vector3(-1f,0f,0f);

        }
        rockObject.player = _player;
        rockObject.rockSpeed =rockSpeed;
        rockObject.damage= Random.Range(5,11);
    }

    public void ShootLaser()
    {
        GameObject laser =  Instantiate(laserPrefab,laserPosition.position,laserPosition.rotation);
        laser.transform.position = new Vector3(laser.transform.position.x, laser.transform.position.y + 0.56f,
            laser.transform.position.z);
        Laser laserObject = laser.GetComponent<Laser>();
        if (bossTransform.localScale.x>0)
        {
            laserObject.direction = new Vector3(1f,0f,0f);
            laserObject.transform.localScale = new Vector3(65, 10, 1);

        }
        else if(bossTransform.localScale.x<0)
        {
            laserObject.direction = new Vector3(-1f,0f,0f);
            laserObject.transform.localScale = new Vector3(-65, 10, 1);

        }

        laserObject.laserDistance = laserDistance;
        laserObject.laserPosition = laserPosition;
        laserObject.player = _player;
        laserObject.damage = laserDamage;
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
                animator.SetInteger("counter",0);
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
    
}
