using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : Enemy
{
    private int randomOverlap1, randomOverlap2;
    private string playerTag = "Player";
    [SerializeField] private CircleCollider2D slimeCollision; 

    public override void Start()
    {
        base.Start();
        waitDeath = 0.929f;
        isFlipped = true;
        randomOverlap1 = Random.Range(5, 7);
        randomOverlap2 = Random.Range(2, 4);
    }

    public override void FixedUpdate()
    {
        SortingLevel(randomOverlap1, randomOverlap2);
        TeleportToPlayerDirection();
    }

    public override void OnEnable()
    {
        slimeCollision.isTrigger = false;
        base.OnEnable();
        isFlipped=true;
    }

    public override IEnumerator Death()
    {
        slimeCollision.isTrigger = true;
        return base.Death();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            GameController.instance.playerCharacter.TakeDamage(enemyData.damage);
        }
    }
}
