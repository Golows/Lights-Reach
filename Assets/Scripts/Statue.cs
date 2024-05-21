using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int charge = 0;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject eliteBull;
    public GameObject arrow;
    private bool charging = false;
    private bool charged = false;

    private string enter = "Enter";
    private string exit = "Exit";

    private void Start()
    {
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        while(!charged)
        {
            if(charge == 100)
            {
                charged = true;
                charging = false;
                textObject.SetActive(false);
                circle.SetActive(false);
                animator.SetTrigger(exit);
                SpawnBullBoss();
                arrow.SetActive(false);
                StopCoroutine(Charge());
            }
            yield return new WaitForSeconds(1);
            if(charging)
            {
                charge += 5;
                text.text = charge.ToString() + "%";
            }
            if(charge != 0 && !charging)
            {
                charge -= 5;
                text.text = charge.ToString() + "%";
            }
        }
    }

    private void SpawnBullBoss()
    {
        GameObject elite = Instantiate(eliteBull, new Vector3(transform.position.x, transform.position.y-2f, transform.position.z), Quaternion.identity);

        Enemy enemy = elite.GetComponent<Enemy>();
        enemy.currentHealth = 10000;
        enemy.moveSpeed = 3.8f;
        elite.GetComponent<BoxCollider2D>().isTrigger = true;
        enemy.attackRange = 1.2f;
        enemy.damage = 50;
        enemy.maxHealth = 10000;
        enemy.healthBarPrefab.SetActive(true);
        elite.GetComponent<SpriteRenderer>().color = new Color(0.8301887f, 0.4268423f, 0.4268423f);
        enemy.elite = true;
        elite.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!charged)
        {
            animator.SetTrigger(enter);
            animator.ResetTrigger(exit);
            charging = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!charged)
        {
            animator.SetTrigger(exit);
            animator.ResetTrigger(enter);
            charging = false;
        }
    }
}
