using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    [SerializeField] private GameObject chainLightning;
    private Enemy enemy;

    [SerializeField] private float minMultiplier = 0.80f;
    [SerializeField] private float maxMultiplier = 1.2f;
    [SerializeField] private float damageMultiplier;
    public int basePierce;

    private GameObject startObject;
    private GameObject endObject;

    [SerializeField] private ParticleSystem particleSystemLightning;

    private PlayerCharacter playerCharacter;

    private int singleHit;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        particleSystemLightning = GetComponent<ParticleSystem>();
        playerCharacter = GameController.instance.playerCharacter;
    }

    private void Start()
    {
        Destroy(gameObject, 0.4f);
        if (basePierce == 0) 
            Destroy(gameObject);
        startObject = gameObject;
        singleHit = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<Enemy>();
        if(enemy != null && !enemy.beenStruck)
        {
            if(singleHit !=0)
            {
                endObject = collision.gameObject;

                basePierce--;
                singleHit--;

                var temp = Instantiate(chainLightning, collision.gameObject.transform.position, Quaternion.identity);
                temp.name = gameObject.name;

                enemy.StuckByLightning();

                if(enemy !=null)
                {
                    if (Random.value < playerCharacter.critChance / 100)
                    {
                        enemy.TakeDamage(Random.Range(playerCharacter.damage * damageMultiplier * minMultiplier, playerCharacter.damage * damageMultiplier * maxMultiplier) * playerCharacter.critMultiplier, true, damageMultiplier, Enemy.DamageType.lightning);
                    }
                    else
                    {
                        enemy.TakeDamage(Random.Range(playerCharacter.damage * damageMultiplier * minMultiplier, playerCharacter.damage * damageMultiplier * maxMultiplier), false, damageMultiplier, Enemy.DamageType.lightning);
                    }
                }
                

                circleCollider.enabled = false;

                var emitParams = new ParticleSystem.EmitParams();

                emitParams.position = startObject.transform.position;

                particleSystemLightning.Emit(emitParams, 1);

                emitParams.position = endObject.transform.position;

                particleSystemLightning.Emit(emitParams, 1);
            }
        }
    }
}
