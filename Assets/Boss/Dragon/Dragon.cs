using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : Enemy
{
   
    [SerializeField] 
    private Transform firePosition;
    [SerializeField] 
    private Transform fireFlyPosition;
    [SerializeField]
    private GameObject fireBallPrefab;
    public AudioManager audioManager;
    public GameObject attackGroundCollider;
    public Transform landingPoint;
    private bool isInvulnerable;
    private Animator animator;
    private int counter;
    [SerializeField]
    private Slider slider;
    public float timeBeforeSwitchingGround;
    public float actualTimeBeforeSwitchingGround;
    private bool isDead;


    private Transform fireBallTransform;
    public Transform transitionPoint;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        animator = GetComponent<Animator>();
        SetMaxHealth(_health);
        isInvulnerable = true;
        audioManager.Play("BossFightMusic");
        fireBallTransform = GetComponent<Transform>();
        actualTimeBeforeSwitchingGround = timeBeforeSwitchingGround;
        isDead = false;
    }

    
    public void ShootFireBall()
    {
        GameObject fireBall =  Instantiate(fireBallPrefab,firePosition.position,firePosition.rotation);
        Fireball fireballObject = fireBall.GetComponent<Fireball>();
        float fireBallScale = Random.Range(0.5f, 0.8f);
        fireballObject.orientedToPlayer = true;
        if (fireBallTransform.localScale.x>0)
        {
            fireballObject.direction = new Vector3(1f,0f,0f);

        }
        else if(fireBallTransform.localScale.x<0)
        {
            fireballObject.direction = new Vector3(-1f,0f,0f);

        }
        fireballObject.GetComponent<Transform>().localScale = new Vector3(fireBallScale, fireBallScale, fireBallScale);
        fireballObject._player = _player;
        fireballObject.bulletSpeed = Random.Range(5f,15f);
        fireballObject.Damage = Random.Range(3,7);
    }

    public void ShootFireBallFly()
    {
        GameObject fireBall =  Instantiate(fireBallPrefab,fireFlyPosition.position,fireFlyPosition.rotation);
        Fireball fireballObject = fireBall.GetComponent<Fireball>();
        float fireBallScale = Random.Range(0.5f, 0.8f);
        fireballObject.orientedToPlayer = true;
        Vector2 target = (_player.transform.position - fireballObject.transform.position).normalized;
        fireballObject.direction = target;
        fireballObject.GetComponent<Transform>().localScale = new Vector3(fireBallScale, fireBallScale, fireBallScale);
        fireballObject._player = _player;
        fireballObject.bulletSpeed = Random.Range(5f,15f);
        fireballObject.Damage = Random.Range(3,7);
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
                animator.SetInteger("counter",0);
                SetIsInvulnerable(true);
                animator.SetBool("IsEnraged",true);
            }
            if (_health<=0&&isDead==false)
            {
                isDead = true;
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
    protected void AttackGround()
    {
        if (isInAttackCollider(attackGroundCollider))
        {
            StartCoroutine(_player.TakeDMG(Damage));
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
