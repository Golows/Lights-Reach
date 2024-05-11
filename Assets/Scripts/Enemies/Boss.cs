using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Boss : Enemy
{
    private string playerTag = "Player";
    private bool inRange = false;
    private float angle = 0;

    public GameObject fireBeam;
    private string beamTrigger = "FireBeam";
    private string walkTrigger = "Walk";

    public Animator animator;

    public GameObject bullet;
    public Transform bulletTransform;

    public GameObject bullet2;

    public int bulletsToSpawn = 10;
    private int bulletsCount = 0;


    private void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        fireBeam = Instantiate(fireBeam, transform.position, Quaternion.identity);
        fireBeam.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(LaserAttack());
        StartCoroutine(SpawnBullets());
        ShootAround();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            inRange = true;
            StartCoroutine(KeepTakingDamage());
            
        }
    }

    private void Update()
    {
        if(fireBeam != null) 
        {
            fireBeam.transform.position = transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            inRange = false;
            StopCoroutine(KeepTakingDamage());
        }
    }


    IEnumerator LaserAttack()
    {
        animator.SetTrigger(beamTrigger);
        Quaternion startingRotation = fireBeam.transform.rotation;
        float time = 0f;
        yield return new WaitForSeconds(1.5f);
        fireBeam.GetComponent<BoxCollider2D>().enabled = true;
        while (time <  3f)
        {
            time += Time.deltaTime;
            fireBeam.transform.rotation = startingRotation * Quaternion.AngleAxis(time / 3f * 360f, Vector3.forward);
            
            yield return null;
        }
        fireBeam.transform.rotation = startingRotation;
        Destroy(fireBeam);
        animator.ResetTrigger(beamTrigger);
        animator.SetTrigger(walkTrigger);
        yield break;
    }

    IEnumerator SpawnBullets()
    {
        bulletsCount = bulletsToSpawn;
        while(bulletsCount > 0)
        {
            bulletsCount--;
            yield return new WaitForSeconds(0.1f);
            Shoot();
        }
    }

    private void ShootAround()
    {
        int angle = 0;
        while(angle < 360)
        {
            var bullet = Instantiate(bullet2, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet2>().Shoot(angle);
            angle += 15;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }


    IEnumerator KeepTakingDamage()
    {
        while(inRange)
        {
            GameController.instance.playerCharacter.TakeDamage(enemyData.damage);
            yield return new WaitForSeconds(1f);
        }
    }
}
