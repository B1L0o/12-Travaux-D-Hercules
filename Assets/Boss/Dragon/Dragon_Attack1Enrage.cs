using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Attack1Enrage : StateMachineBehaviour
{
    private Dragon dragon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon = animator.GetComponent<Dragon>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (dragon.actualTimeBeforeSwitchingGround>0)
        {
            dragon.actualTimeBeforeSwitchingGround -= Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("air");
        }
        dragon.LookAtPlayer();
        if (dragon.isInAttackCollider(dragon.attackGroundCollider)==false)
        {
            animator.SetBool("attack1",false);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("air");
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