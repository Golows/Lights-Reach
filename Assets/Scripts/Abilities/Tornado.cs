using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private GameObject character;
    private PlayerCharacter playerCharacter;
    private Enemy enemy;

    public float angle;

    [SerializeField] private float minMultiplier = 0.80f;
    [SerializeField] private float maxMultiplier = 1.2f;
    [SerializeField] private float damageMultiplier;

    private void Start()
    {
        character = GameController.instance.character;
        playerCharacter = GameController.instance.playerCharacter;
        angle = angle * Mathf.PI / 180.0f;
    }
    private void FixedUpdate()
    {
        if(character != null && playerCharacter != null)
        {
            transform.position = new Vector3(character.transform.position.x + Mathf.Sin(angle) * 4, character.transform.position.y + Mathf.Cos(angle) * 4, 0);
            angle += Time.fixedDeltaTime * playerCharacter.moveSpeed * 0.4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.StartTakingDamage(minMultiplier, maxMultiplier, damageMultiplier);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.StopTakingDamage();
        }
    }
}
