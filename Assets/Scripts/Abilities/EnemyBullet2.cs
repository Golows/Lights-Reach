using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2 : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void Shoot(float angle)
    {
        rb = GetComponent<Rigidbody2D>();
        var position = transform.position;
        float rot = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + angle);
        rb.velocity = transform.up * speed;
    }
}
