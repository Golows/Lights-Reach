using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCharacter : MonoBehaviour
{
    public WeaponData weaponData;
    public Camera mainCamera;
    [SerializeField] private Animator animator;

    //Stats
    [SerializeField] private float baseMoveSpeed = 3f;
    public float moveSpeed = 0;

    [SerializeField] private float baseDashCooldown = 0.2f;
    public float dashCooldown = 0;

    [SerializeField] private float baseDashSpeed = 18f;
    public float dashSpeed = 0f;

    public float dashTime = 0.2f;

    [SerializeField] private float baseHealth = 20f;
    public float health = 0;
    public float currentHealth = 0;

    public float dashLenght = 3f;

    public float critChance = 10f;
    public float critMultiplier = 2f;
    public float damage = 100f;

    private void Start()
    {
        weaponData.SetStartStats();
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
        GameController.instance.uiManager.UpdateHealth(currentHealth);
    }

    public void TakeDamage(float damage)
    {
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

    private void Die()
    {
        animator.SetTrigger("Death");
    }
}
