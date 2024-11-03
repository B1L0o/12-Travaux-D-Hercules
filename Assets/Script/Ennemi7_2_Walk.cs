using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi7_2_Walk : StateMachineBehaviour
{
    private Enemy7_2 enemy7;
    private Player player;
    public float timeValue = 10;
    private float speed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy7 = animator.GetComponent<Enemy7_2>();
        player = enemy7._player;
        speed = enemy7._speed;
    }
    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeValue>0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("explode");
        }
        enemy7.LookAtPlayer();
        if (Vector2.Distance(player.transform.position,enemy7.transform.position)<1f)
        {
            animator.SetTrigger("explode");
        }
        else
        {
            Vector2 playerTarget = new Vector2(player.transform.position.x, enemy7.transform.position.y);
            enemy7.transform.position = Vector2.MoveTowards( enemy7.transform.position, playerTarget, speed * Time.deltaTime);
        }
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
