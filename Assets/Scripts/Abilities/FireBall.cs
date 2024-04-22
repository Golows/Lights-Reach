using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force;

    private Vector3 rotation;

    private int pierceCount;

    public float damage;

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
            EnemyFlying flyingEnemy = collision.GetComponent<EnemyFlying>();

            if (enemy != null)
            {
                enemy.TakeDamage(Random.Range(22f, 150f));
                pierceCount--;
            }
            if (flyingEnemy != null)
            {
                flyingEnemy.TakeDamage(Random.Range(22f, 150f));
                pierceCount--;
            }
            
            if(pierceCount <= 0)
            {
                ObjectPoolManager.RemoveObjectToPool(gameObject);
                //Destroy(gameObject);
            }
        }
        
    }
}
