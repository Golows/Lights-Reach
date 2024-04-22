using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRun : StateMachineBehaviour
{
    private Transform charcterPosition;
    private Rigidbody2D rb;
    private Enemy enemy;
    private EnemyData enemyData;
    private Vector2 previousLocation = new Vector2(0,0);
    private float offsetX, offsetY;
    int interval = 1;
    float nextTime = 0;
    private BoxCollider2D collisionBoxCollider;
    private string attackTrigger = "Attack";

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        charcterPosition =  GameController.instance.character.GetComponent<Transform>();
        collisionBoxCollider = animator.GetComponent<BoxCollider2D>();
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        enemyData = enemy.enemyData;
        offsetX = Random.Range(-3f, 3f);
        offsetY = Random.Range(-3f, 3f);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.Flip();

        Vector2 target = new Vector2(charcterPosition.position.x, charcterPosition.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, new Vector2(target.x+offsetX, target.y + offsetY), enemyData.speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Time.time >= nextTime)
        {
            offsetX = Random.Range(-2.5f, 2.5f);
            offsetY = Random.Range(-2.5f, 2.5f);

            if (!collisionBoxCollider.isTrigger && Vector3.Distance(charcterPosition.position, rb.position) < 1.5)
            {
                collisionBoxCollider.isTrigger = true;
            }
            else if (collisionBoxCollider.isTrigger && Vector3.Distance(charcterPosition.position, rb.position) > 1.5)
            {
                collisionBoxCollider.isTrigger = false;
            }
            nextTime += interval;
        }

        if(Vector2.Distance(charcterPosition.position, rb.position) <= enemyData.attackRange)
        {
            animator.SetTrigger(attackTrigger);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(attackTrigger);
    }

}
