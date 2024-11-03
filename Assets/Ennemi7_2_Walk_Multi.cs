using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi7_2_Walk_Multi : StateMachineBehaviour
{
    private Enemy7_2 enemy7;
    private GameObject player;
    public float timeValue = 10;
    private float speed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy7 = animator.GetComponent<Enemy7_2>();
        if (enemy7._player2 is null)
        {
            player = enemy7._player2Join.gameObject;
        }
        else
        {
            player = enemy7._player2.gameObject;
        }
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
        enemy7.LookAtSomething(player.GetComponent<Transform>());
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
