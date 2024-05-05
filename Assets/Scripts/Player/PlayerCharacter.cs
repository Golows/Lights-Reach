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
    public bool dead = false;

    [SerializeField] private float baseDashCooldown;
    [NonSerialized] public float dashCooldown;

    [SerializeField] private float baseDashSpeed;
    [NonSerialized] public float dashSpeed;

    [NonSerialized] public float dashTime;
    [SerializeField] public float dashLenght = 3f;

    [SerializeField] public float baseHealth;
    [NonSerialized] public float health;
    [NonSerialized] public float currentHealth;

    [SerializeField] private float baseHealthRegen;
    [NonSerialized] public float healthRegen;


    public float critChance;

    public float baseCritMultiplier;
    public float critMultiplier;

    public float baseDamage;
    public float damage;

    public float baseAttackSpeed;
    public float attackSpeed;

    public int pierce;

    public float damageReduction = 0;

    public bool startingArea = false;

    private void Awake()
    {
        SetStartStats();
    }

    private void SetStartStats()
    {
        dead = false;
        moveSpeed = baseMoveSpeed;
        dashCooldown = baseDashCooldown;
        dashSpeed = baseDashSpeed;
        dashTime = dashLenght / dashSpeed;
        health = baseHealth;
        currentHealth = health;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        critMultiplier = baseCritMultiplier;
        healthRegen = baseHealthRegen;
        StartCoroutine(HealthRegeneration());
    }

    public void TakeDamage(float damage)
    {
        damage = damage * (1 - damageReduction/100);
        if(currentHealth - damage > 0)
        {
            currentHealth -= damage;
            GameController.instance.uiManager.UpdateHealth();
        }
        else
        {
            Die();
            currentHealth = 0;
            GameController.instance.uiManager.UpdateHealth();
        }
    }

    IEnumerator HealthRegeneration()
    {
        while(currentHealth != 0)
        {
            if (currentHealth < health)
            {
                if ((currentHealth + healthRegen) > health)
                {
                    currentHealth = health;
                    GameController.instance.uiManager.UpdateHealth();
                }
                else
                {
                    currentHealth += healthRegen;
                    GameController.instance.uiManager.UpdateHealth();
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        dead = true;
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
