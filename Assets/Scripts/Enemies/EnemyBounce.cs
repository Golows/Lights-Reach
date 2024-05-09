using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBounce : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private EnemySlime enemy;

    private Transform charcterPosition;

    float interval = 1;
    float nextTime = 0;

    int[] directionX = { -1, 1 };
    int[] directionY = { -1, 1 };
    private int offsetX, offsetY;
    float currentLocationX, currentLocationY;

    private string jumpTrigger = "Jump";

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        charcterPosition = GameController.instance.character.GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<EnemySlime>();

        offsetX = Random.Range(0, directionX.Length);
        offsetY = Random.Range(0, directionY.Length);

        currentLocationX = rb.position.x - charcterPosition.position.x;
        currentLocationY = rb.position.y - charcterPosition.position.y;
        animator.ResetTrigger(jumpTrigger);

        nextTime = Time.time + interval;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(Time.timeScale < 0.1f)
        {
            return;
        }

        enemy.Flip();

        if (currentLocationX > 0 && currentLocationY > 0)
        {
            rb.velocity = new Vector3(-1, -1, 0).normalized * enemy.moveSpeed;
        }
        else if (currentLocationX < 0 && currentLocationY < 0)
        {
            rb.velocity = new Vector3(1, 1, 0).normalized * enemy.moveSpeed;
        }
        else if (currentLocationX > 0 && currentLocationY < 0)
        {
            rb.velocity = new Vector3(-1, 1, 0).normalized * enemy.moveSpeed;
        }
        else if (currentLocationX < 0 && currentLocationY > 0)
        {
            rb.velocity = new Vector3(1, -1, 0).normalized * enemy.moveSpeed;
        }

        if (Time.time >= nextTime)
        {
            rb.velocity = Vector3.zero;
            animator.SetTrigger(jumpTrigger);

            
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(jumpTrigger);
    }
}
