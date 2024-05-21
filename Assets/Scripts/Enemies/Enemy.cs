using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private PlayerMovement playerMovement;
    public EnemyData enemyData;
    public bool isFlipped = false;
    public LayerMask attackMask;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform xpDropPosition;
    [SerializeField] private GameObject scroll;
    [SerializeField] private Image healthBar;
    public GameObject healthBarPrefab;
    public float waitDeath;
    private string deathTrigger = "Died";
    private Rigidbody2D rb;
    public bool beenStruck = false;
    private bool keepTakingDamage;
    public float currentHealth;
    public float maxHealth;
    public float moveSpeed;
    public float attackRange;
    public float damage;
    private PlayerCharacter playerCharacter;
    public bool elite = false;
    public bool boss = false;

    public GameObject collectAllGem;

    [SerializeField] private AudioClip[] deathAudio;
    
    public enum DamageType
    {
        fireball,
        lightning,
        tornado
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {
        player = GameController.instance.character.GetComponent<Transform>();
        playerCharacter = GameController.instance.playerCharacter;
        playerMovement = GameController.instance.character.GetComponent<PlayerMovement>();
        if(!elite && !boss)
        {
            maxHealth = enemyData.health * GameController.instance.timeManager.healthMultiplier;
            currentHealth = enemyData.health * GameController.instance.timeManager.healthMultiplier;
            moveSpeed = enemyData.speed;
            attackRange = enemyData.attackRange;
            damage = enemyData.damage;
        }
        if(boss)
        {
            maxHealth = enemyData.health;
            currentHealth = maxHealth;
            moveSpeed = enemyData.speed;
            damage = enemyData.damage;
        }

        waitDeath = 1.455f;
    }

    public virtual void OnEnable()
    {
        isFlipped = false;
        GetComponent<Animator>().ResetTrigger(deathTrigger);
        GetComponent<BoxCollider2D>().enabled = true;
        if(!boss)
        {
            maxHealth = enemyData.health * GameController.instance.timeManager.healthMultiplier;
            currentHealth = enemyData.health * GameController.instance.timeManager.healthMultiplier;
        }
        
    }

    public virtual void FixedUpdate()
    {
        SortingLevel(5, 2);
        TeleportToPlayerDirection();
    }

    private bool RandomBoolean()
    {
        if (Random.value >= 0.5)
            {
                return true;
            }
        return false;
    }   

    public void SortingLevel(int layer1, int layer2)
    {
        float currentHeight = transform.position.y - player.position.y;
        if (currentHeight < 0)
        {
            spriteRenderer.sortingOrder = layer1;
        }
        else if (currentHeight > 0)
        {
            spriteRenderer.sortingOrder = layer2;
        }

    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TeleportToPlayerDirection()
    {
        if (playerMovement.facingRight && playerMovement.movingUp && Vector2.Distance(transform.position, player.position) > 16f)
        {
            if (RandomBoolean())
            {
                transform.position = new Vector2(player.position.x + 11, player.position.y + Random.Range(-6f, 7f));
            }
            else
            {
                transform.position = new Vector2(player.position.x + Random.Range(-8.5f, 8.6f), player.position.y + 6);
            }
        }
        else if (playerMovement.facingRight && !playerMovement.movingUp && Vector2.Distance(transform.position, player.position) > 16f)
        {
            if (RandomBoolean())
            {
                transform.position = new Vector2(player.position.x + 11, player.position.y + Random.Range(-6f, 7f));
            }
            else
            {
                transform.position = new Vector2(player.position.x + Random.Range(-8.5f, 8.6f), player.position.y - 6);
            }
        }
        else if (!playerMovement.facingRight && !playerMovement.movingUp && Vector2.Distance(transform.position, player.position) > 16f)
        {
            if (RandomBoolean())
            {
                transform.position = new Vector2(player.position.x - 11, player.position.y - Random.Range(-6f, 7f));
            }
            else
            {
                transform.position = new Vector2(player.position.x - Random.Range(-8.5f, 8.6f), player.position.y - 6);
            }
        }
        else if (!playerMovement.facingRight && playerMovement.movingUp && Vector2.Distance(transform.position, player.position) > 16f)
        {
            if (RandomBoolean())
            {
                transform.position = new Vector2(player.position.x - 11, player.position.y - Random.Range(-6f, 7f));
            }
            else
            {
                transform.position = new Vector2(player.position.x - Random.Range(-8.5f, 8.6f), player.position.y + 6);
            }
        }
    }

    public void Attack()
    {
        Vector3 pos = transform.position;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, enemyData.attackRange, attackMask);

        if (colInfo != null)
        {
            GameController.instance.playerCharacter.TakeDamage(damage);
        }
    }

    public void Flip()
    {
        float currentLocation = transform.position.x - player.position.x;


        if(currentLocation < -0.5 && !isFlipped)
        {
            healthBarPrefab.transform.Rotate(0f, 180f, 0f);
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (currentLocation > 0.5 && isFlipped)
        {
            healthBarPrefab.transform.Rotate(0f, 180f, 0f);
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    public virtual IEnumerator Death()
    {
        StopTakingDamage();
        GetComponent<Animator>().SetTrigger(deathTrigger);
        rb.velocity = Vector3.zero;
        GameObject xpOrb = ObjectPoolManager.SpawnObject(GameController.instance.xpOrb, xpDropPosition.position, Quaternion.identity, ObjectPoolManager.PoolType.Xp);
        GameController.instance.levelManager.AddToOrbList(xpOrb.GetComponent<XPOrb>());
        GameController.instance.progressManager.gameCoins++;
        GameController.instance.uiManager.UpdateCoins();
        if(deathAudio.Length > 0)
            GameController.instance.audioManager.PlaySoundEffectsRandom(deathAudio, transform, 0.5f);
        
        yield return new WaitForSeconds(waitDeath);

        if (Random.value < 0.0013)
        {
            Instantiate(collectAllGem, transform.position, Quaternion.identity);
        }

        if (!elite)
        {
            GameController.instance.enemyManager.enemyCount--;
            ObjectPoolManager.RemoveObjectToPool(gameObject);
        }
        else if(elite && !boss)
        {
            Instantiate(scroll, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (boss)
        {
            GameController.instance.uiManager.OnVicotryScreen();
            GetComponent<Animator>().SetTrigger("FireBeam");
            Destroy(gameObject);
        }
    }

    public void StuckByLightning()
    {
        StartCoroutine(BeenStruck());
    }

    private IEnumerator BeenStruck()
    {
        beenStruck = true;
        yield return new WaitForSeconds(0.4f);
        beenStruck = false;
    }

    private IEnumerator KeepTakingDamage(float minMultiplier, float maxMultiplier, float damageMultiplier)
    {
        while(keepTakingDamage)
        {
            yield return new WaitForSeconds(1 / GameController.instance.playerCharacter.attackSpeed / 2);
            if (Random.value < playerCharacter.critChance / 100)
            {
                TakeDamage(Random.Range(playerCharacter.damage * damageMultiplier * minMultiplier, playerCharacter.damage * damageMultiplier * maxMultiplier) * playerCharacter.critMultiplier, true, damageMultiplier, DamageType.tornado);
            }
            else
            {
                TakeDamage(Random.Range(playerCharacter.damage * damageMultiplier * minMultiplier, playerCharacter.damage * damageMultiplier * maxMultiplier), false, damageMultiplier, DamageType.tornado);
            }
        }
    }

    public void StartTakingDamage(float minMultiplier, float maxMultiplier, float damageMultiplier)
    {
        keepTakingDamage = true;
        StartCoroutine(KeepTakingDamage(minMultiplier, maxMultiplier, damageMultiplier));
    }

    public void StopTakingDamage()
    {
        keepTakingDamage = false;
        StopCoroutine(KeepTakingDamage(0, 0, 0));
    }

    public bool TakeDamage(float damage, bool crit, float damageMultiplier, DamageType damageType)
    {
        if (currentHealth < 0)
        {
            return false;
        }

        if(currentHealth - damage > 0)
        {
            currentHealth -= damage;

            switch (damageType)
            {
                case DamageType.fireball:
                    GameController.instance.damageDoneManager.fireballDamageDone += Mathf.RoundToInt(damage);
                    break;
                case DamageType.lightning:
                    GameController.instance.damageDoneManager.lightningDamageDone += Mathf.RoundToInt(damage);
                    break;
                case DamageType.tornado:
                    GameController.instance.damageDoneManager.tornadoDamageDone += Mathf.RoundToInt(damage);
                    break;
                default:
                    break;
            }

            if (elite)
            {
                UpdateHealthBar();
            }
            GameController.instance.uiManager.ShowDamage(transform, (int)damage, crit, damageMultiplier);
            return true;
        }
        else
        {
            switch (damageType)
            {
                case DamageType.fireball:
                    GameController.instance.damageDoneManager.fireballDamageDone += Mathf.RoundToInt(currentHealth);
                    break;
                case DamageType.lightning:
                    GameController.instance.damageDoneManager.lightningDamageDone += Mathf.RoundToInt(currentHealth);
                    break;
                case DamageType.tornado:
                    GameController.instance.damageDoneManager.tornadoDamageDone += Mathf.RoundToInt(currentHealth);
                    break;
                default:
                    break;
            }
            currentHealth -= damage;
            if(elite)
            {
                UpdateHealthBar();
            }
            GetComponent<BoxCollider2D>().enabled = false;
            GameController.instance.uiManager.ShowDamage(transform, (int)damage, crit, damageMultiplier);
            StartCoroutine(Death());
            return true;
        }
    }
}
