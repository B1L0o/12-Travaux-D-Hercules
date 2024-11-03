using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diomede_Run : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private Rigidbody2D rb;
    private Diomede diomede;
    private GameObject player;
    private Transform bossTransform;
    [SerializeField]
    private float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        diomede = animator.GetComponent<Diomede>();
        bossTransform = animator.GetComponent<Transform>();
        player = diomede._player.gameObject;
        diomede.SetIsInvulnerable(false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        diomede.LookAtPlayer();
       Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
       Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
       rb.MovePosition(newPos);
        if (Random.Range(0,3)==2 &&   diomede.isInAttackCollider(diomede.skill2Collider))
        {
            animator.SetTrigger("skill2");
        }
        else if (diomede.isInAttackCollider(diomede.skill1Collider))
        {
            animator.SetBool("skill1",true);
        }
        
        
    }

    
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("skill2");
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
