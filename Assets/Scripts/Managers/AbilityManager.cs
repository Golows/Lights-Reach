using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityManager : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject chainLightning;
    public GameObject tornado;

    [SerializeField] private float speed;
    private bool useFireBall = true;
    private bool onCooldownFireball = false;
    private float fireballCooldown = 0;
    private List<GameObject> tornados = new List<GameObject>();

    public void StartFireBall()
    {
        speed = 1 / GameController.instance.playerCharacter.attackSpeed;
        StartCoroutine(SpawnFireBall());
    }

    private void Update()
    {
        if(onCooldownFireball && fireballCooldown - Time.deltaTime >= 0)
        {
            fireballCooldown -= Time.deltaTime;
        }
    }

    public void SpawnLightning()
    {
        StartCoroutine(SpawnChainLightning());
    }

    public void SpawnTornado()
    {
        int angle = 0;
        for(int i = 0; i < 4; i++)
        {
            GameObject newTornado = Instantiate(tornado, GameController.instance.character.transform.position, Quaternion.identity);
            newTornado.GetComponent<Tornado>().angle = angle;
            tornados.Add(newTornado);
            angle += 90;
        }
    }

    public IEnumerator SpawnChainLightning()
    {
        while(true)
        {
            yield return new WaitForSeconds(speed * 2f);
            var lightning = Instantiate(chainLightning, GameController.instance.character.transform.position, Quaternion.identity);
            lightning.GetComponent<ChainLightning>().basePierce += GameController.instance.playerCharacter.pierce;
        }
    }

    public IEnumerator SpawnFireBall()
    {
        while(useFireBall)
        {
            onCooldownFireball = true;
            yield return new WaitForSeconds(speed);
            onCooldownFireball = false;
            speed = 1 / GameController.instance.playerCharacter.attackSpeed;
            fireballCooldown = speed;
            if(GameController.instance.upgradeManager.projectileCount == 1)
                FireBallCreate1();
            else if (GameController.instance.upgradeManager.projectileCount == 2)
                FireBallCreate2();
            else if (GameController.instance.upgradeManager.projectileCount == 3)
                FireBallCreate3();
            else if (GameController.instance.upgradeManager.projectileCount == 4)
                FireBallCreate4();
            else if (GameController.instance.upgradeManager.projectileCount == 5)
                FireBallCreate5();
        }
        
    }

    private void FireBallCreate1()
    {
        //GameObject newFireBall = Instantiate(fireBall, GameController.instance.character.transform.position, Quaternion.identity);

        GameObject newFireBall = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall.GetComponent<FireBall>().SetMovement(90);
        
    }
    private void FireBallCreate2()
    {
        GameObject newFireBall = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall.GetComponent<FireBall>().SetMovement(87);

        GameObject newFireBall2 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall2.GetComponent<FireBall>().SetMovement(93);
    }
    private void FireBallCreate3()
    {
        GameObject newFireBall = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall.GetComponent<FireBall>().SetMovement(84);

        GameObject newFireBall2 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall2.GetComponent<FireBall>().SetMovement(90);

        GameObject newFireBall3 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall3.GetComponent<FireBall>().SetMovement(96);
    }
    private void FireBallCreate4()
    {
        GameObject newFireBall = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall.GetComponent<FireBall>().SetMovement(81);

        GameObject newFireBall2 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall2.GetComponent<FireBall>().SetMovement(87);

        GameObject newFireBall3 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall3.GetComponent<FireBall>().SetMovement(93);

        GameObject newFireBall4 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall4.GetComponent<FireBall>().SetMovement(99);
    }
    private void FireBallCreate5()
    {
        GameObject newFireBall = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall.GetComponent<FireBall>().SetMovement(78);

        GameObject newFireBall2 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall2.GetComponent<FireBall>().SetMovement(84);

        GameObject newFireBall3 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall3.GetComponent<FireBall>().SetMovement(90);

        GameObject newFireBall4 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall4.GetComponent<FireBall>().SetMovement(96);

        GameObject newFireBall5 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall5.GetComponent<FireBall>().SetMovement(102);
    }
}
