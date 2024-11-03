using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geryon_Walk : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private Rigidbody2D rb;
    private Geryon geryon;
    private GameObject player;
    private Transform bossTransform;
    [SerializeField]
    private float speed;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        geryon = animator.GetComponent<Geryon>();
        bossTransform = animator.GetComponent<Transform>();
        player = geryon._player.gameObject;
        geryon.SetIsInvulnerable(false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        geryon.LookAtPlayer();
        Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (geryon.isInAttackCollider(geryon.attack1Collider))
        {
            animator.SetBool("attack1",true);
        }
        else if (Vector2.Distance(bossTransform.position,player.transform.position)<7f)
        {
            animator.SetTrigger("attack2");
        }
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack2");
    }

    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
