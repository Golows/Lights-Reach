using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public GameObject fireBall;

    private float speed;
    private bool useFireBall = true;

    void Start()
    {
        speed = 0.8f;
        StartCoroutine(SpawnFireBall());
    }

    public IEnumerator SpawnFireBall()
    {
        while(useFireBall)
        {
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

            yield return new WaitForSeconds(speed);
            
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
        newFireBall.GetComponent<FireBall>().SetMovement(82);

        GameObject newFireBall2 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall2.GetComponent<FireBall>().SetMovement(98);
    }
    private void FireBallCreate3()
    {
        GameObject newFireBall = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall.GetComponent<FireBall>().SetMovement(75);

        GameObject newFireBall2 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall2.GetComponent<FireBall>().SetMovement(90);

        GameObject newFireBall3 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall3.GetComponent<FireBall>().SetMovement(105);
    }
    private void FireBallCreate4()
    {
        GameObject newFireBall = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall.GetComponent<FireBall>().SetMovement(67);

        GameObject newFireBall2 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall2.GetComponent<FireBall>().SetMovement(82);

        GameObject newFireBall3 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall3.GetComponent<FireBall>().SetMovement(98);

        GameObject newFireBall4 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall4.GetComponent<FireBall>().SetMovement(113);
    }
    private void FireBallCreate5()
    {
        GameObject newFireBall = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall.GetComponent<FireBall>().SetMovement(75);

        GameObject newFireBall2 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall2.GetComponent<FireBall>().SetMovement(105);

        GameObject newFireBall3 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall3.GetComponent<FireBall>().SetMovement(90);

        GameObject newFireBall4 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall4.GetComponent<FireBall>().SetMovement(60);

        GameObject newFireBall5 = ObjectPoolManager.SpawnObject(fireBall, GameController.instance.character.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Abilities);
        newFireBall5.GetComponent<FireBall>().SetMovement(120);
    }
}
