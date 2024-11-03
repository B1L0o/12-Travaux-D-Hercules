using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBull_MoveEnrage : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private BossBull bossBull;
    private GameObject player;
    private Transform bossTransform;
    private Camera playerCamera;
    private Vector3 screenPoint;
    [SerializeField]
    private float speed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossBull = animator.GetComponent<BossBull>();
        bossBull.SetIsInvulnerable(false);
        bossTransform = animator.GetComponent<Transform>();
        player = bossBull._player.gameObject;
        rb = animator.GetComponent<Rigidbody2D>();
        playerCamera = bossBull.playerCamera;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.transform.position,bossTransform.position)<3f)
        {
            if (player.transform.position.x>bossTransform.position.x)
            {
                bossTransform.Translate(Vector2.left* speed * Time.fixedDeltaTime,Space.World);

            }
            else
            {
                bossTransform.Translate(Vector2.right* speed * Time.fixedDeltaTime,Space.World);
            }

            //rb.MovePosition(-bossTransform.forward * speed * Time.fixedDeltaTime);
        }
        else
        {
            bossBull.LookAtPlayer();
            screenPoint = playerCamera.WorldToViewportPoint(bossTransform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (!onScreen)
            {
                animator.SetBool("taunt",true);
            }
            if (bossBull.isInAttackCollider(bossBull.attack2Collider))
            {
                animator.SetBool("attack2",true);
            }
            else if (bossBull.isInAttackCollider(bossBull.attack1Collider) && bossBull.isInAttackCollider(bossBull.attack4Collider)==false)
            {
                animator.SetBool("attack",true);
            }
            else if (bossBull.isInAttackCollider(bossBull.attack4Collider))
            {
                animator.SetBool("attack4",true);
            }
            Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
            //bossTransform.position = Vector2.MoveTowards(bossTransform.position, target, speed * Time.fixedDeltaTime);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
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
