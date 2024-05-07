using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class XPOrb : MonoBehaviour
{
    private int playerLayer;
    public bool collected = false;
    public int index;
    private Transform playerTransform;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10;
    [SerializeField] private int xpAmount;
    [SerializeField] private Light2D lightAura;

    [SerializeField] private float distanceToCollect = 0.5f;

    [SerializeField] private float minMultiplier = 0.85f;
    [SerializeField] private float maxMultiplier = 1.15f;

    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerTransform = GameController.instance.playerTransform;
    }

    private void FixedUpdate()
    {
        if(collected)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, new Vector2(playerTransform.position.x, playerTransform.position.y), speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (Vector3.SqrMagnitude(playerTransform.position - transform.position) < distanceToCollect * distanceToCollect)
            {
                GameController.instance.levelManager.AddXp(Random.Range((int)(xpAmount * minMultiplier), (int)(xpAmount * maxMultiplier) + 1));
                GameController.instance.audioManager.PlaySoundEffectsInARow(audioClips, transform, 0.5f);
                DestroyObject();
            }
        }
    }

    private void DestroyObject()
    {
        collected = false;
        GameController.instance.levelManager.RemoveFromOrbList(this);
        ObjectPoolManager.RemoveObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            collected = true;
        }
    }
}
