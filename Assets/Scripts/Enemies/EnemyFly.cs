using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : StateMachineBehaviour
{
    private UnityEngine.Transform charcterPosition;
    private Rigidbody2D rb;
    private EnemyFlying enemy;
    private Vector2 previousLocation = new Vector2(0, 0);
    private float offsetX, offsetY;
    float interval = 2.5f;
    float nextTime = 0;
    Vector2 targetDestination;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nextTime = Time.time + 1f;
        charcterPosition = GameController.instance.character.GetComponent<UnityEngine.Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<EnemyFlying>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.timeScale < 0.1f)
        {
            return;
        }

        if (Time.time >= nextTime)
        {
            enemy.Flip();
            float currentLocationX = rb.position.x - charcterPosition.position.x;
            float currentLocationY = rb.position.y - charcterPosition.position.y;

            if (currentLocationX > 0  && currentLocationY > 0)
            {
                targetDestination = new Vector2(charcterPosition.position.x - Random.Range(6f, 9f), charcterPosition.position.y - Random.Range(4f, 6f));
            }
            else if(currentLocationX < 0 && currentLocationY < 0)
            {
                targetDestination = new Vector2(charcterPosition.position.x + Random.Range(6f, 9f), charcterPosition.position.y + Random.Range(4f, 6f));
            }
            else if (currentLocationX > 0 && currentLocationY < 0)
            {
                targetDestination = new Vector2(charcterPosition.position.x - Random.Range(6f, 9f), charcterPosition.position.y + Random.Range(4f, 6f));
            }
            else if (currentLocationX < 0 && currentLocationY > 0)
            {
                targetDestination = new Vector2(charcterPosition.position.x + Random.Range(6f, 9f), charcterPosition.position.y - Random.Range(4f, 6f));
            }

            nextTime += interval;
        }

        Vector2 newPos = Vector2.MoveTowards(rb.position, targetDestination, enemy.moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
