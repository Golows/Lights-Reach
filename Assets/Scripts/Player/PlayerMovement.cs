using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private float moveX, moveY;
    private Vector2 moveDirection;
    private bool canDash = true;
    
    public bool isDashing;
    public bool facingRight = true;
    public bool movingUp = false;
    public bool dashOnCd = false;
    private string walkingBool = "Walking";


    [SerializeField] private Rigidbody2D characterRB;
    [SerializeField] public PlayerCharacter character;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Transform shadowTransform;
    [SerializeField] private Animator animator;

    public PlayerInput playerInput;

    private void Start()
    {
        characterRB = GetComponent<Rigidbody2D>();
    }

    public void PerformDash()
    {
        if(canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public void PerformMovement(InputAction.CallbackContext context)
    {
        if (!isDashing)
        {
            moveDirection = context.ReadValue<Vector2>();
            moveX = moveDirection.x;
            moveY = moveDirection.y;
            if (moveX > 0 && !facingRight)
            {
                Flip();
            }
            if (moveX < 0 && facingRight)
            {
                Flip();
            }
            ProcessInputs();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        else
            Move();

    }

    private void ProcessInputs()
    {
        if(moveY > 0)
        {
            movingUp = true;
        }
        else if(moveY < 0)
        {
            movingUp = false;
        }

        if((moveX > 0 || moveX < 0) && !animator.GetBool(walkingBool) || (moveY > 0 || moveY < 0) && !animator.GetBool(walkingBool))
        {
            animator.SetBool(walkingBool, true);
        }
        else if(moveX == 0 && moveY == 0 && animator.GetBool(walkingBool))
        {
            animator.SetBool(walkingBool, false);
        }
    }

    private void Move()
    {
        characterRB.velocity = new Vector2(moveDirection.x * character.moveSpeed, moveDirection.y * character.moveSpeed);
    }

    private void Flip()
    {
        Vector3 currentScale = characterTransform.localScale;
        Vector3 currentShadowScale = shadowTransform.localScale;
        currentScale.x *= -1;
        currentShadowScale.x *= -1;
        characterTransform.localScale = currentScale;
        shadowTransform.localScale = currentShadowScale;

        facingRight = !facingRight;
    }

    private IEnumerator Dash()
    {
        //Debug.Log(character.dashCooldown);
        canDash = false;
        isDashing = true;

        if (moveDirection.x == 0 && moveDirection.y == 0 && facingRight)
        {
            characterRB.velocity = new Vector2(1 * character.dashSpeed, 0);
        }
        else if(moveDirection.x == 0 && moveDirection.y == 0 && !facingRight)
        {
            characterRB.velocity = new Vector2(-1 * character.dashSpeed, 0);
        }
        else
        {
            characterRB.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * character.dashSpeed;
        }
        if(character.transform.position  == character.transform.position)
        
        yield return new WaitForSeconds(character.dashTime);
        isDashing = false;
        dashOnCd = true;
        yield return new WaitForSeconds(character.dashCooldown);
        dashOnCd = false;
        canDash = true;
    }
}
