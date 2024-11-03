using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBull_attack2Enrage : StateMachineBehaviour
{
    private BossBull bossBull;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossBull = animator.GetComponent<BossBull>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int randomChoice = Random.Range(0, 3);
        if (randomChoice==0)
        {
            if (bossBull.isInAttackCollider(bossBull.attack1Collider))
            {
                animator.SetBool("attack",true);
            }
        }
        if (randomChoice==1)
        {
            if (bossBull.isInAttackCollider(bossBull.attack4Collider))
            {
                animator.SetBool("attack4",true);
            }
        }
        else if (bossBull.isInAttackCollider(bossBull.attack2Collider)==false)
        {
            animator.SetBool("attack2",false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("attack2",false);
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
