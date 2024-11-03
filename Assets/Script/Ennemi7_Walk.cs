using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi7_Walk : StateMachineBehaviour
{
    private Enemy7 enemy7;
    private Player player;
    private Transform[] waypoints;
    //private Transform target;
    //private int index_next_target = 0;
    private float speed;

    private EnemyState enemyState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy7 = animator.GetComponent<Enemy7>();
        player = enemy7._player;
        waypoints = enemy7._waypoints;
        speed = enemy7._speed;
        //target = waypoints[0];
        enemy7.SetEnemyState(new Patrol(waypoints,speed,animator.gameObject,player.gameObject,enemy7.GetOrientation()));
        enemyState = enemy7.GetEnemyState();
        enemyState.InitialiseState();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((player.transform.position.x >= waypoints[0].transform.position.x && player.transform.position.x <= waypoints[1].transform.position.x))
        {
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
        else
        {
            enemyState.ApplyState();
            //enemy7.LookAtSomething(target);
            //Vector3 direction = target.position - enemy7.transform.position;
            //enemy7.transform.Translate(direction.normalized * speed * Time.deltaTime,Space.World);
            //if (Vector3.Distance(enemy7.transform.position,target.position)<0.3f)
            //{
                //index_next_target = (index_next_target + 1) % waypoints.Length;
                //target = waypoints[index_next_target];
            //}
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
