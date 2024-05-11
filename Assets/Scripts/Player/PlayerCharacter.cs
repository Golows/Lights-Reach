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
    [SerializeField] private CircleCollider2D circleCollider;

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

    public float basePickupRanage;

    public float critChance;

    public float baseCritMultiplier;
    public float critMultiplier;

    public float baseDamage;
    public float damage;

    public float baseAttackSpeed;
    public float attackSpeed;

    public int pierce;

    public float damageReduction = 0;


    public void Start()
    {
        GameController.instance.progressManager.UpdateStatsFromProgress();
    }

    public void SetStartStats(PlayerProgress playerProgress)
    {
        dead = false;

        baseHealth = baseHealth + GameController.instance.progressManager.healthIncrease * playerProgress.healthUpgrade;
        baseDamage = baseDamage + GameController.instance.progressManager.damageIncrease * playerProgress.damageUpgrade;
        baseAttackSpeed = baseAttackSpeed + GameController.instance.progressManager.attackSpeedIncrease * playerProgress.attackSpeedUpgrade;
        basePickupRanage = basePickupRanage + GameController.instance.progressManager.pickupRangeIncrease * playerProgress.pickupRangeUpgrade;
        baseMoveSpeed = baseMoveSpeed + GameController.instance.progressManager.moveSpeedIncrease * playerProgress.moveSpeedUpgrade;
        critChance = critChance + GameController.instance.progressManager.critChangeIncrease * playerProgress.critChangeUpgrade;
        damageReduction = damageReduction + GameController.instance.progressManager.damageReductionIncrease * playerProgress.damageReductionUpgrade;
        healthRegen = healthRegen + GameController.instance.progressManager.healthRegenIncrease * playerProgress.healthRegenUpgrade;
        baseDashCooldown = baseDashCooldown - GameController.instance.progressManager.dashCooldownDecrease * playerProgress.dashCooldownUpgrade;

        dashCooldown = baseDashCooldown;
        health = baseHealth;
        currentHealth = baseHealth;
        attackSpeed = baseAttackSpeed;
        damage = baseDamage;
        moveSpeed = baseMoveSpeed;
        dashSpeed = baseDashSpeed;
        dashTime = dashLenght / dashSpeed;
        critMultiplier = baseCritMultiplier;
        circleCollider.radius = basePickupRanage;
        StartCoroutine(HealthRegeneration());
        if(GameController.instance.abilityManager != null)
            GameController.instance.abilityManager.StartFireBall();
        GameController.instance.uiManager.playerCharacter = GameController.instance.playerCharacter;
        GameController.instance.uiManager.UpdateOnStart();
    }

    public void TakeDamage(float damage)
    {
        damage = damage * (1 - damageReduction/100);
        if(currentHealth - damage > 0)
        {
            currentHealth -= damage;
            GameController.instance.uiManager.UpdateHealth();
            GameController.instance.postProcessingManager.TakeDamageStartEffect();
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
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(1.667f);
        Time.timeScale = 0f;
        GameController.instance.uiManager.OnDeathScreen();
        GameController.instance.progressManager.SaveCoinProgress();
    }

    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        StartCoroutine(Death());
    }
}
