using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_flyToLand : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Transform bossTransform;
    [SerializeField] 
    private float speed;
    private Dragon dragon;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        bossTransform = animator.GetComponent<Transform>();
        dragon = animator.GetComponent<Dragon>();
        dragon.SetIsInvulnerable(true);
        dragon.actualTimeBeforeSwitchingGround = dragon.timeBeforeSwitchingGround;
    }

    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rb.MovePosition(bossTransform.position + Vector3.up * Time.fixedDeltaTime *speed );
        Vector2 target = new Vector2(bossTransform.position.x, dragon.landingPoint.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (Math.Abs(bossTransform.position.y - dragon.landingPoint.position.y) < 0.05d)
        {
            animator.SetTrigger("hitTransitionPoint");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("hitTransitionPoint");
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
