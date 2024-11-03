using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion2_Walk : StateMachineBehaviour
{
    private Lion2 lion;
    private Player player;
    private Transform bossTransform;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lion = animator.GetComponent<Lion2>();
        lion.SetIsInvulnerable(false);
        bossTransform = animator.GetComponent<Transform>();
        player = lion._player;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.transform.position,bossTransform.position)<2f)
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
            lion.LookAtPlayer();
            //Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
            //bossTransform.position = Vector2.MoveTowards(bossTransform.position, target, speed * Time.fixedDeltaTime);
            Vector2 target = new Vector2(player.transform.position.x, bossTransform.position.y);
            //bossTransform.position = Vector2.MoveTowards(bossTransform.position, target, speed * Time.fixedDeltaTime);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (lion.isInAttackCollider())
            {
                animator.SetBool("attack",true);
            }
        }
        
    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}
}