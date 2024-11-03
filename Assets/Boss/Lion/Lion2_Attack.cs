using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion2_Attack : StateMachineBehaviour
{
    private Lion2 lion;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lion = animator.GetComponent<Lion2>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (lion.isInAttackCollider()==false)
        {
            animator.SetBool("attack",false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}