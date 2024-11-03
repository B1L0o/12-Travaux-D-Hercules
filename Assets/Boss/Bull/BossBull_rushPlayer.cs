using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBull_rushPlayer : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private BossBull bossBull;
    private GameObject player;
    private Transform bossTransform;
    [SerializeField]
    private float speed;
    private float accelerationTime = 6f;    
    private float minSpeed ;
    private float maxSpeed = 10f;
    private float time ;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossBull = animator.GetComponent<BossBull>();
        bossBull.SetIsInvulnerable(false);
        bossTransform = animator.GetComponent<Transform>();
        player = bossBull._player.gameObject;
        rb = animator.GetComponent<Rigidbody2D>();
        minSpeed = speed;
        time = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (time>=accelerationTime || Vector2.Distance(player.transform.position,bossTransform.position)<6f)
        {
            animator.SetBool("taunt",false);
            
        }
        bossBull.LookAtPlayer();
        Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
        //bossTransform.position = Vector2.MoveTowards(bossTransform.position, target, speed * Time.fixedDeltaTime);
        speed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime );
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        time += Time.fixedDeltaTime;
        
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
