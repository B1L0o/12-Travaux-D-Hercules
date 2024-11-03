using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hydre_RunToPlayer : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private Player player;
    private Hydre hydre;
    private Rigidbody2D rb;
    private Transform bossTransform;
    [SerializeField]
    private float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hydre = animator.GetComponent<Hydre>();
        hydre.SetIsInvulnerable(false);
        bossTransform = animator.GetComponent<Transform>();
        player = hydre._player;
        rb = animator.GetComponent<Rigidbody2D>();
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
            hydre.LookAtPlayer();
            //Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
            //bossTransform.position = Vector2.MoveTowards(bossTransform.position, target, speed * Time.fixedDeltaTime);
            Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (hydre.isInAttackCollider(hydre.skill1Collider))
            {
                animator.SetBool("skill1",true);
            }
        
            else if (hydre.isInAttackCollider(hydre.skill2Collider))
            {
                animator.SetTrigger("skill2");
            }
            else if (Random.Range(0,200)==0)
            {
                animator.SetTrigger("skill3");
            }
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
