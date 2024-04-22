using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private PlayerMovement playerMovement;
    public EnemyData enemyData;
    public bool isFlipped = false;
    public LayerMask attackMask;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform xpDropPosition;
    public float waitDeath;
    private string deathTrigger = "Died";
    private Rigidbody2D rb;

    private float currentHealth;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {
        player = GameController.instance.character.GetComponent<Transform>();
        playerMovement = GameController.instance.character.GetComponent<PlayerMovement>();
        currentHealth = enemyData.health;
        waitDeath = 1.455f;
    }

    public virtual void OnEnable()
    {
        isFlipped = false;
        GetComponent<Animator>().ResetTrigger(deathTrigger);
        GetComponent<BoxCollider2D>().enabled = true;
        currentHealth = enemyData.health;
    }

    public virtual void FixedUpdate()
    {
        SortingLevel(5,2);
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
            GameController.instance.playerCharacter.TakeDamage(enemyData.damage);
        }
    }

    public void Flip()
    {
        float currentLocation = transform.position.x - player.position.x;


        if(currentLocation < -0.5 && !isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (currentLocation > 0.5 && isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    public virtual IEnumerator Death()
    {
        GetComponent<Animator>().SetTrigger(deathTrigger);
        rb.velocity = Vector3.zero;
        GameObject xpOrb = ObjectPoolManager.SpawnObject(GameController.instance.xpOrb, xpDropPosition.position, Quaternion.identity, ObjectPoolManager.PoolType.None);
        GameController.instance.levelManager.AddToOrbList(xpOrb.GetComponent<XPOrb>());
        yield return new WaitForSeconds(waitDeath);
        GameController.instance.enemyManager.enemyCount--;
        ObjectPoolManager.RemoveObjectToPool(gameObject);
    }

    public bool TakeDamage(float damage, bool crit, float damageMultiplier)
    {
        if (currentHealth < 0)
        {
            return false;
        }

        if(currentHealth - damage > 0)
        {
            currentHealth -= damage;
            GameController.instance.uiManager.ShowDamage(transform, (int)damage, crit, damageMultiplier);
            return true;
        }
        else
        {
            currentHealth -= damage;
            GetComponent<BoxCollider2D>().enabled = false;
            GameController.instance.uiManager.ShowDamage(transform, (int)damage, crit, damageMultiplier);
            StartCoroutine(Death());
            return true;
        }
    }
}
