using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCharacter : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] private Animator animator;

    //Stats
    public float baseMoveSpeed;
    public float moveSpeed;

    [SerializeField] private float baseDashCooldown;
    [NonSerialized] public float dashCooldown;

    [SerializeField] private float baseDashSpeed;
    [NonSerialized] public float dashSpeed;

    [NonSerialized] public float dashTime;
    [SerializeField] public float dashLenght = 3f;

    [SerializeField] private float baseHealth;
    [NonSerialized] public float health;
    [NonSerialized] public float currentHealth;

    

    public float critChance;

    public float baseCritMultiplier;
    public float critMultiplier;

    public float baseDamage;
    public float damage;

    public float baseAttackSpeed;
    public float attackSpeed;

    public int pierce;

    public float damageReduction = 0;

    private void Awake()
    {
        SetStartStats();
    }

    private void SetStartStats()
    {
        moveSpeed = baseMoveSpeed;
        dashCooldown = baseDashCooldown;
        dashSpeed = baseDashSpeed;
        dashTime = dashLenght / dashSpeed;
        health = baseHealth;
        currentHealth = health;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        critMultiplier = baseCritMultiplier;
        GameController.instance.uiManager.UpdateHealth(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        damage = damage * (1 - damageReduction/100);
        if(currentHealth - damage > 0)
        {
            currentHealth -= damage;
            GameController.instance.uiManager.UpdateHealth(currentHealth);
        }
        else
        {
            Die();
            currentHealth = 0;
            GameController.instance.uiManager.UpdateHealth(0);
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1.667f);
        Time.timeScale = 0f;
    }

    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        StartCoroutine(Death());
    }
}
