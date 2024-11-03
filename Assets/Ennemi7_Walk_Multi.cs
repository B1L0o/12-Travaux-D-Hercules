using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi7_Walk_Multi : StateMachineBehaviour
{
    private Enemy7 enemy7;
    private Transform[] waypoints;
    //private Transform target;
    //private int index_next_target = 0;
    private float speed;
    private Player2join _player2Join;
    private Player2 _player2;
    private EnemyState enemyState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy7 = animator.GetComponent<Enemy7>();
        _player2 = enemy7._player2;
        _player2Join = enemy7._player2Join;
        waypoints = enemy7._waypoints;
        speed = enemy7._speed;
        //target = waypoints[0];
        enemy7.SetEnemyState(new Patrol(waypoints,speed,animator.gameObject,_player2.gameObject,enemy7.GetOrientation()));
        enemyState = enemy7.GetEnemyState();
        enemyState.InitialiseState();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((_player2.transform.position.x >= waypoints[0].transform.position.x && _player2.transform.position.x <= waypoints[1].transform.position.x))
        {
            enemy7.LookAtSomething(_player2.GetComponent<Transform>());
            if (Vector2.Distance(_player2.transform.position,enemy7.transform.position)<1f)
            {
                animator.SetTrigger("explode");
            }
            else
            {
                Vector2 playerTarget = new Vector2(_player2.transform.position.x, enemy7.transform.position.y);
                enemy7.transform.position = Vector2.MoveTowards( enemy7.transform.position, playerTarget, speed * Time.deltaTime);
            }
        }
        else if ((_player2Join.transform.position.x >= waypoints[0].transform.position.x && _player2Join.transform.position.x <= waypoints[1].transform.position.x))
        {
            enemy7.LookAtSomething(_player2Join.GetComponent<Transform>());
            if (Vector2.Distance(_player2Join.transform.position,enemy7.transform.position)<1f)
            {
                animator.SetTrigger("explode");
            }
            else
            {
                Vector2 playerTarget = new Vector2(_player2Join.transform.position.x, enemy7.transform.position.y);
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
