using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public ParticleSystem dust;
    public bool invicible;
    private bool ApplyPoison;
    private Shake _shake;
    public AudioSource Heal;
    public AudioSource jumpsound;
    public AudioSource run;
    public AudioSource attack;
    public AudioSource airAttack;
    public AudioSource bow;
    public AudioSource hit;

    public float timeInBetween_MeleeG;
    private float timeInBetween_Bow;
    public float startTimeAttackMeleeG;
    public float startTimeAttackBow;


    
    // Jump Variables //
    public bool isJumping;
    public int maxJumps;
    private float jumpForce = 20;
    private float maxButtonHoldTime = .4f;
    private float holdForce = 100;
    private float maxJumpSpeed = 6;
    private float maxFallSpeed = -20;
    private float fallSpeed = 50;
    private float gravityMultipler = 7;
    private bool jumpPressed;
    private bool jumpHeld;
    private float buttonHoldTime;
    private float originalGravity;
    private int numberOfJumpsLeft;
    // Collision Handlers //
    public Transform groundCheck;
    public Transform GroundattackPoint;
    public Transform AirattackPoint;
    public Transform firePosition;
    public GameObject projectile;
    public float groundAttackRange = 0.5f;
    public float airAttackRange = 0.5f;
    public LayerMask ennemyLayers;
    public float groundCheckRadius;
    public LayerMask collisionLayers;
    public bool isGrounded;
    // Jump Variables //
    
    
   
    
    
    // Animation Variables //
    private float attackDelay = 1;
    private float bowDelay = 100f;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    // Animation Variables //
    
    
    
    // Movement Variables //
    public float moveSpeed;
    public Vector3 velocity = Vector3.zero;
    public float horizontalMovement = 0;
    // Movement Variables //

    
    
    // Health Variables //
    public int health;
    public int maxHealth;
    public HealthBar healthBar;
    // Health Variables //
    
    [SerializeField] 
    private Material _flashMaterial;
    [SerializeField] 
    private float _flashDuration;
    private bool _isflashing=false;
    
    public void FixedUpdate()
    {
        //MovePlayer(Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime);
        MovePlayer((horizontalMovement) * moveSpeed * Time.fixedDeltaTime);
        IsJumping();
    }
    
    
    void Start()
    { 
        health = maxHealth;
        invicible = false;
        healthBar.SetMaxHealth(maxHealth);
        buttonHoldTime = maxButtonHoldTime;
        originalGravity = rb.gravityScale;
        numberOfJumpsLeft = maxJumps;
        moveSpeed = 600;
        _shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }
    

    void Update ()
    {
        if (timeInBetween_MeleeG > 0)
        {
            invicible = true;
        }
        else
        {
            invicible = false;
        }
        
        
        if (isGrounded && (rb.velocity.x > 1 || rb.velocity.x < -1))
        {
            if (!run.isPlaying)
            {
                run.Play();

            }
            
            
        }
        else if (!run.isPlaying)
        {
            run.Stop();
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            horizontalMovement = 1;
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            horizontalMovement = 0;
        } 
        
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            horizontalMovement = -1;
        }

        // Jump Part //
        if (Input.GetButtonDown("Jump"))
        {
            if (numberOfJumpsLeft > 0)
            {
                CreateDust();
                jumpsound.Play();
            }
            
            jumpPressed = true;
            animator.SetBool("Jump",true);
            animator.Play("Player_JUmp");

        }
        else
            jumpPressed = false;
        if (Input.GetButton("Jump"))
        {
            jumpHeld = true;
        }
        else
            jumpHeld = false;
        CheckForJump();
        GroundCheck();
        // Jump Part //
        

        

        // Sprite Display //
        Flip(rb.velocity.x);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("Jump", !isGrounded);
        // Sprite Display //

        

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            rb.position = new Vector2(114.93f, -24.01f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            rb.position = new Vector2(
                456.2f, -43.9f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            rb.position = new Vector2(
                310f, 11.68f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            rb.position = new Vector2(
                353.3f, 16f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            rb.position = new Vector2(
                507.3f, 10.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            rb.position = new Vector2(
                546.95f, 12.37f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            rb.position = new Vector2(
                142.2f, 8.1f);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            rb.position = new Vector2(
                735.4f, 9.08f);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HealPlayer(25);
        }
        
        
        
        // Attack and Damage //
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(TakeDMG(10));
        }

        if (Input.GetButtonDown("Attack") && timeInBetween_MeleeG < 0)
        {
            StartCoroutine(Melee());
            timeInBetween_MeleeG = startTimeAttackMeleeG;
        }
        else
        {
            timeInBetween_MeleeG -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Bow") && timeInBetween_Bow < 0)
        {
            StartCoroutine(Distance());
            timeInBetween_Bow = startTimeAttackBow;
        }
        else
        {
            timeInBetween_Bow -= Time.deltaTime;
        }

        // Attack and Damage //

        


    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        
    }

     void Flip(float _velocity) // The side Hercules is facing
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
            GroundattackPoint.localPosition = new Vector2(0.357f, 0);
        }
        else if (_velocity < -0.1f)
        {
            GroundattackPoint.localPosition = new Vector2(-0.357f, 0);
            spriteRenderer.flipX = true;


        }
    }

     public void HealPlayer(int amount)
     {
         if (health < maxHealth)
         {
             Heal.Play();
             health += amount;
             healthBar.SetHealth(health);
         }
     }

    

    private void OnDrawGizmos() // Displays Circle for hitbox collisions with the grou,d //
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        if (GroundattackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(GroundattackPoint.position, groundAttackRange);
        Gizmos.DrawWireSphere(AirattackPoint.position, airAttackRange);

    }

    
    
    IEnumerator Melee() // Melee Animation
    {
        if (!isGrounded)
        {
            animator.Play("AIR SLASH");
            airAttack.Play();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AirattackPoint.position,airAttackRange);
            foreach (var enemy in hitEnemies)
            {
                if (enemy.tag == "enemy")
                {
                    _shake.camShake();
                    enemy.GetComponent<Enemy>().TakeDamage(20);
                }
            }

        }
        else
        {
            animator.Play("PlayerSwordSlash_");
            attack.Play();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(GroundattackPoint.position, groundAttackRange);
            foreach (var enemy in hitEnemies)
            {
                if (enemy.tag == "enemy")
                {
                    _shake.camShake();
                    enemy.GetComponent<Enemy>().TakeDamage(20);

                }
            }

        }
        yield return new WaitForSeconds(attackDelay);

    }
    IEnumerator Distance() // Bow Animation
    {
        animator.Play("Player Bow boom");
        bow.Play();
        Instantiate(projectile, firePosition.position, firePosition.rotation);
        yield return new WaitForSeconds(bowDelay);
    }
    
    public IEnumerator TakeDMG(int _damage) //Simple method used to inflict damage to Hercules
    {
        if (!invicible)
        {
            health -= _damage;
            Flash();
            _shake.camShake();
            healthBar.SetHealth(health);
            if (health <= 0)
            {
                GameOverManager.instance.WhenPlayerDeath();
                gameObject.SetActive(false);
                //Time.timeScale = 0.0f;

            }
            yield return new WaitForSeconds(attackDelay);
        }
       
    }

    public void StartPoisonEffectSpeed(float speed, float time)
    {
        StartCoroutine(PoisonEffectSpeed(speed, time));
    }
    IEnumerator PoisonEffectHealth(int damage, float delay) {
        var waitForSeconds = new WaitForSeconds(delay);
        while (ApplyPoison)
        {
            if (!invicible)
            {
                health -= damage;
                Flash();
                _shake.camShake();
                healthBar.SetHealth(health);
                if (health <= 0)
                {
                    GameOverManager.instance.WhenPlayerDeath();
                    gameObject.SetActive(false);
                }
            }
            yield return waitForSeconds;
        }
    }

    IEnumerator PoisonEffectHealthDelay(int damage,float time,float delay)
    {
        ApplyPoison = true;
        StartCoroutine(PoisonEffectHealth(damage,delay));
        yield return new WaitForSeconds(time);
        ApplyPoison = false;
    }

    public void StartPoisonEffectHealth(int damage,float time,float delay)
    {
        StartCoroutine(PoisonEffectHealthDelay(damage, time,delay));
    }

    public IEnumerator PoisonEffectSpeed(float speed,float time)
    {
        if (!invicible)
        {
            float initialMoveSpeed = moveSpeed;
            moveSpeed = speed;
            yield return new WaitForSeconds(time);
            moveSpeed = initialMoveSpeed;
        }
    }
    
    

    private void CheckForJump() // Determines whether or not Hercules has remaining jumps 
    {
        if (jumpPressed)
        {
            if (!isGrounded && numberOfJumpsLeft == maxJumps)
            {
                isJumping = false;
                return;
            }
            numberOfJumpsLeft--;
            if (numberOfJumpsLeft >= 0)
            {
                rb.gravityScale = originalGravity;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                buttonHoldTime = maxButtonHoldTime;
               isJumping = true;
            }
        }
    }

    private void IsJumping() //Applies forces upward to make Hercules go boom boom in the air
    {
        if(isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce);

            AdditionalAir();
        }
        if (rb.velocity.y > maxJumpSpeed)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
        }
        Falling();
    }

    private void AdditionalAir() // Handles Additional Jumps
    {
        if (jumpHeld)
        {
            buttonHoldTime -= Time.deltaTime;
            if (buttonHoldTime <= 0)
            {
                buttonHoldTime = 0;
                isJumping = false;
            }
            else
                rb.AddForce(Vector2.up * holdForce);
        }
        else
        {
            isJumping = false;
        }
    }

    private void Falling() // Makes Hercules go into the ground, Handles gravity in a way
    {
        if(!isJumping && rb.velocity.y < fallSpeed)
        {
            rb.gravityScale = gravityMultipler;
        }
        if(rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
        
    }
    
    private void GroundCheck() // Determines whether or not Hercules is grounded ,by using the hitbox under his feets
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers) && !isJumping)
        {
            isGrounded = true;
            numberOfJumpsLeft = maxJumps;
            rb.gravityScale = originalGravity;
        }
        else
        {
            isGrounded = false;
        }
    }

    void CreateDust()
    {
        dust.Play();
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
    
    
    
}
