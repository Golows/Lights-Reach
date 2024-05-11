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
    

    public GameObject fireBeam1;
    public GameObject fireBeam2;
    public GameObject fireBeam3;
    public GameObject fireBeam4;


    private string beamTrigger = "FireBeam";
    private string walkTrigger = "Walk";

    public Animator animator;

    public GameObject bullet;
    public Transform bulletTransform;

    public GameObject bullet2;

    public int bulletsToSpawn = 15;
    private int bulletsCount = 0;


    private void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        StartCoroutine(BossFight());

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

    IEnumerator BossFight()
    {
        while(currentHealth > 0)
        {
            yield return new WaitForSeconds(3f);

            if(currentHealth > enemyData.health * 0.6f)
            {
                fireBeam1 = Instantiate(fireBeam, transform.position, Quaternion.identity);
                fireBeam1.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(LaserAttack1());
            }
            else if(currentHealth <  enemyData.health *0.6f && currentHealth > enemyData.health * 0.3f)
            {
                fireBeam1 = Instantiate(fireBeam, transform.position, Quaternion.identity);
                fireBeam1.GetComponent<BoxCollider2D>().enabled = false;
                fireBeam2 = Instantiate(fireBeam, transform.position, Quaternion.identity);
                fireBeam2.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(LaserAttack2());
                bulletsToSpawn = 20;
            }
            else
            {
                fireBeam1 = Instantiate(fireBeam, transform.position, Quaternion.identity);
                fireBeam1.GetComponent<BoxCollider2D>().enabled = false;
                fireBeam2 = Instantiate(fireBeam, transform.position, Quaternion.identity);
                fireBeam2.GetComponent<BoxCollider2D>().enabled = false;
                fireBeam3 = Instantiate(fireBeam, transform.position, Quaternion.identity);
                fireBeam3.GetComponent<BoxCollider2D>().enabled = false;
                fireBeam4 = Instantiate(fireBeam, transform.position, Quaternion.identity);
                fireBeam4.GetComponent<BoxCollider2D>().enabled = false;

                StartCoroutine(LaserAttack3());
                bulletsToSpawn = 30;
            }



            yield return new WaitForSeconds(7f);
            StartCoroutine(SpawnBullets());
            yield return new WaitForSeconds(2f);
            ShootAround();
            yield return new WaitForSeconds(0.6f);
            ShootAround();
            yield return new WaitForSeconds(0.6f);
            ShootAround();
            yield return new WaitForSeconds(3f);

        }
    }


    IEnumerator LaserAttack1()
    {
        animator.SetTrigger(beamTrigger);
        fireBeam1.transform.rotation *= Quaternion.Euler(0, 0, 0);
        Quaternion startingRotation = fireBeam1.transform.rotation;
        
        float time = 0f;
        yield return new WaitForSeconds(1.5f);
        fireBeam1.GetComponent<BoxCollider2D>().enabled = true;
        while (time <  3f)
        {
            time += Time.deltaTime;
            fireBeam1.transform.rotation = startingRotation * Quaternion.AngleAxis(time / 2f * 360f, Vector3.forward);
            
            yield return null;
        }
        fireBeam1.transform.rotation = startingRotation;
        Destroy(fireBeam1);
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger(beamTrigger);
        animator.SetTrigger(walkTrigger);
        yield break;
    }

    IEnumerator LaserAttack2()
    {
        animator.SetTrigger(beamTrigger);
        fireBeam1.transform.rotation *= Quaternion.Euler(0, 0, 0);
        Quaternion startingRotation1 = fireBeam1.transform.rotation;

        fireBeam2.transform.rotation *= Quaternion.Euler(0, 0, 180);
        Quaternion startingRotation2 = fireBeam2.transform.rotation;

        float time = 0f;
        yield return new WaitForSeconds(1.5f);
        fireBeam1.GetComponent<BoxCollider2D>().enabled = true;
        fireBeam2.GetComponent<BoxCollider2D>().enabled = true;
        while (time < 4f)
        {
            time += Time.deltaTime;
            fireBeam1.transform.rotation = startingRotation1 * Quaternion.AngleAxis(time / 3f * 360f, Vector3.forward);
            fireBeam2.transform.rotation = startingRotation2 * Quaternion.AngleAxis(time / 3f * 360f, Vector3.forward);
            yield return null;
        }
        fireBeam1.transform.rotation = startingRotation1;
        fireBeam2.transform.rotation = startingRotation2;
        Destroy(fireBeam1);
        Destroy(fireBeam2);
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger(beamTrigger);
        animator.SetTrigger(walkTrigger);
        yield break;
    }

    IEnumerator LaserAttack3()
    {
        animator.SetTrigger(beamTrigger);
        fireBeam1.transform.rotation *= Quaternion.Euler(0, 0, 0);
        Quaternion startingRotation1 = fireBeam1.transform.rotation;

        fireBeam2.transform.rotation *= Quaternion.Euler(0, 0, 180);
        Quaternion startingRotation2 = fireBeam2.transform.rotation;

        fireBeam3.transform.rotation *= Quaternion.Euler(0, 0, 90);
        Quaternion startingRotation3 = fireBeam3.transform.rotation;

        fireBeam4.transform.rotation *= Quaternion.Euler(0, 0, -90);
        Quaternion startingRotation4 = fireBeam4.transform.rotation;

        float time = 0f;
        yield return new WaitForSeconds(1.5f);
        fireBeam1.GetComponent<BoxCollider2D>().enabled = true;
        fireBeam2.GetComponent<BoxCollider2D>().enabled = true;
        fireBeam3.GetComponent<BoxCollider2D>().enabled = true;
        fireBeam4.GetComponent<BoxCollider2D>().enabled = true;
        while (time < 4f)
        {
            time += Time.deltaTime;
            fireBeam1.transform.rotation = startingRotation1 * Quaternion.AngleAxis(time / 4f * 360f, Vector3.forward);
            fireBeam2.transform.rotation = startingRotation2 * Quaternion.AngleAxis(time / 4f * 360f, Vector3.forward);
            fireBeam3.transform.rotation = startingRotation3 * Quaternion.AngleAxis(time / 4f * 360f, Vector3.forward);
            fireBeam4.transform.rotation = startingRotation4 * Quaternion.AngleAxis(time / 4f * 360f, Vector3.forward);
            yield return null;
        }
        fireBeam1.transform.rotation = startingRotation1;
        fireBeam2.transform.rotation = startingRotation2;
        fireBeam3.transform.rotation = startingRotation1;
        fireBeam4.transform.rotation = startingRotation2;
        Destroy(fireBeam1);
        Destroy(fireBeam2);
        Destroy(fireBeam3);
        Destroy(fireBeam4);

        yield return new WaitForSeconds(1f);
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
            angle += 24;
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
