using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class Hydre_Walk : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private GameObject target;
    private Transform bossTransform;
    private Player player;
    private AudioManager AudioManager;
    private Rigidbody2D rb;
    private Hydre hydre;
    [SerializeField]
    private float speed;
   
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hydre = animator.GetComponent<Hydre>();
        AudioManager =  hydre.AudioManager;
        bossTransform = animator.GetComponent<Transform>();
        target = GameObject.FindGameObjectWithTag("target");
        player = hydre._player;
        player.GetComponent<Player>().enabled = false;
        rb = animator.GetComponent<Rigidbody2D>();
    }
    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        //Vector2 direction = new Vector3(target.transform.position.x - bossTransform.position.x,0);
        //bossTransform.Translate(direction.normalized * speed * Time.fixedDeltaTime,Space.World);
        Vector2 direction = new Vector2(target.transform.position.x, bossTransform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, direction, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (Vector2.Distance(bossTransform.position,target.transform.position)<=3)
        {
            animator.SetTrigger("isReachWaypoint");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.Play("BossFightMusic");
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
