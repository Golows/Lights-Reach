using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    private Transform charcterPosition;
    private Rigidbody2D rb;
    private Enemy enemy;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        charcterPosition = GameController.instance.character.GetComponent<Transform>();

        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.timeScale < 0.1f)
        {
            return;
        }
        enemy.Flip();

        Vector2 target = new Vector2(charcterPosition.position.x, charcterPosition.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, enemy.moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(rb != null)
            rb.velocity = Vector2.zero;
    }
}
