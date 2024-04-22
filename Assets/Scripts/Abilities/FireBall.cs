using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;
    private PlayerCharacter playerCharacter;
    private Rigidbody2D rb;
    public float force;

    private Vector3 rotation;

    private int pierceCount;

    [SerializeField] private float minMultiplier = 0.80f;
    [SerializeField] private float maxMultiplier = 1.2f;

    [SerializeField] private float damageMultiplier;
    private bool hit = false;

    private void Awake()
    {
        playerCharacter = GameController.instance.playerCharacter;   
    }

    private void Start()
    {
        //Destroy(gameObject, 5f);
    }

    public void SetMovement(int angle)
    {
        mainCamera = GameController.instance.character.GetComponent<PlayerCharacter>().mainCamera;
        rb = GetComponent<Rigidbody2D>(); 
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        rotation = transform.position - mousePos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + angle);
        rb.velocity = transform.up * force;
    }

    private void OnEnable()
    {
        StartCoroutine(EnableStart());
    }

    IEnumerator EnableStart()
    {
        pierceCount = GameController.instance.upgradeManager.pierceCount;
        yield return new WaitForSeconds(1f);
        ObjectPoolManager.RemoveObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pierceCount > 0)
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if(Random.value < playerCharacter.critChance / 100)
            {
                if(enemy != null)
                {
                    hit = enemy.TakeDamage(Random.Range(playerCharacter.damage * damageMultiplier * minMultiplier, playerCharacter.damage * damageMultiplier * maxMultiplier) * playerCharacter.critMultiplier, true, damageMultiplier);
                    if(hit)
                        pierceCount--;
                }  
            }
            else
            {
                if (enemy != null)
                {
                    hit = enemy.TakeDamage(Random.Range(playerCharacter.damage * damageMultiplier * minMultiplier, playerCharacter.damage * damageMultiplier * maxMultiplier), false, damageMultiplier);
                    if(hit)
                        pierceCount--;
                }
            }

            if(pierceCount <= 0)
            {
                ObjectPoolManager.RemoveObjectToPool(gameObject);
            }
        }
        
    }
}
