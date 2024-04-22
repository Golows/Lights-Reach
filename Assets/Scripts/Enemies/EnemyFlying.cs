using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    private string playerTag = "Player";
    [SerializeField] private GameObject shadow;

    public override void OnEnable()
    {
        shadow.SetActive(true);
        base.OnEnable();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(playerTag))
        {
            GameController.instance.playerCharacter.TakeDamage(enemyData.damage);
        }
    }

    public override IEnumerator Death()
    {
        shadow.SetActive(false);
        return base.Death();
    }
}
