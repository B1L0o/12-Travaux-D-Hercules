using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Walk : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private Rigidbody2D rb;
    private Dragon dragon;
    private GameObject player;
    private Transform bossTransform;
    [SerializeField]
    private float speed;
    

    private float actualTimeIntervalBetweenFireBall;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        dragon = animator.GetComponent<Dragon>();
        bossTransform = animator.GetComponent<Transform>();
        player = dragon._player.gameObject;
        dragon.SetIsInvulnerable(false);
        actualTimeIntervalBetweenFireBall = Random.Range(3f,6f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (actualTimeIntervalBetweenFireBall>0)
        {
            actualTimeIntervalBetweenFireBall -= Time.deltaTime;
        }
        if (Vector2.Distance(player.transform.position,bossTransform.position)<6f)
        {
            if (player.transform.position.x>bossTransform.position.x)
            {
                bossTransform.Translate(Vector2.left* speed * Time.fixedDeltaTime,Space.World);

            }
            else
            {
                bossTransform.Translate(Vector2.right* speed * Time.fixedDeltaTime,Space.World);
            }

            //rb.MovePosition(-bossTransform.forward * speed * Time.fixedDeltaTime);
        }
        else
        {
            dragon.LookAtPlayer();
            Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (dragon.isInAttackCollider(dragon.attackGroundCollider))
            {
                animator.SetBool("attack1",true);
            }
            else if(actualTimeIntervalBetweenFireBall<=0)
            {
                actualTimeIntervalBetweenFireBall = Random.Range(3f,6f);;
                animator.SetTrigger("attack2");
            }
        }


    }

    
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack2");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
