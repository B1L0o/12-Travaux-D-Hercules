using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    [SerializeField] 
    private GameObject attackCollider;
    [SerializeField] 
    private LayerMask whatIsGround;
    private bool isGrounded = false;
    Vector3 direction;
    private float jumpForceX = 1f;
    private float jumpForceY = 4f;
    private Transform _target;
    private int _index_next_target=0;
    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        _target = _waypoints[0];
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((_player.transform.position.x >= _waypoints[0].transform.position.x && _player.transform.position.x <= _waypoints[1].transform.position.x))
        {
            LookAtPlayer();
            //Vector2.Distance(_player.transform.position,transform.position)<2.1f
            if (CanAttack(attackCollider.GetComponent<Collider2D>()))
            {
                animator.SetBool("attack",true);
            }
            else
            {
                animator.SetBool("attack",false);
                direction = (_player.transform.position - transform.position).normalized;
            }
        }
        else
        {
            animator.SetBool("attack",false);
            LookAtSomething(_target);
            direction = (_target.position - transform.position).normalized;
            if (Vector3.Distance(transform.position,_target.position)<0.6f)
            {
                _index_next_target = (_index_next_target + 1) % _waypoints.Length;
                _target = _waypoints[_index_next_target];
            }
        }
        
        
    }

    public void Jump()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f),whatIsGround);
        if (isGrounded)
        {
            rb.velocity = new Vector2(jumpForceX * direction.x, jumpForceY);
        }
       
    }

    public void StopJump()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f),whatIsGround);
        if (isGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void AttackAnimationEvent()
    {
        AttackWithoutCoolDown(attackCollider.GetComponent<Collider2D>());
    }
    
    
}
