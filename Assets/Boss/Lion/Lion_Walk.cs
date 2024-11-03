using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion_Walk : StateMachineBehaviour
{
    private EnemyState _enemyState;
    private Vector3 _playerPosition;
    private Vector3 lionPosition;
    private List<Vector3> path;
    private Func<bool> _CanAttack;

    private float AttackRange;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyState = animator.GetComponent<Lion>().GetEnemyState();
        path = ((FollowPlayer)_enemyState).path;
        lionPosition = animator.GetComponent<Transform>().position;
        _playerPosition =  animator.GetComponent<Lion>().GetPlayerPosition();
        AttackRange = animator.GetComponent<Lion>().GetAttackRange();
         bool CanAttack()
        {
            Collider2D coll = animator.GetComponent<Lion>().GetComponent<Collider2D>();
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

         _CanAttack = CanAttack;

    }
    
    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        path = ((FollowPlayer)_enemyState).path;
        //Vector3.Distance(lionPosition,_playerPosition)<=AttackRange
        //Mathf.Abs(lionPosition.x-_playerPosition.x)<=AttackRange && Mathf.Abs(lionPosition.y-_playerPosition.y)<=0.5
        if (_CanAttack())
        {
            animator.SetTrigger("Attack");
        }
        
        if (path != null && path.Count>=2 && Mathf.Abs(path[1].y - path[0].y) >=1)
        {
            animator.SetTrigger("Jump");
        }
        _enemyState.ApplyState();   
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Attack");
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
