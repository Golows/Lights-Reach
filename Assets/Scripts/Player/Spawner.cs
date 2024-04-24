using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int SpawnLineTop(GameObject enemy)
    {
        Vector3 currentPosition = new Vector3(transform.position.x - 8, transform.position.y + 6, transform.position.z);
        float movePositionX = 0;
        int spawnCount = 16;
        int index = spawnCount;
        while (index > 0)
        {
            index--;
            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + movePositionX, currentPosition.y, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
            movePositionX += 1.1f;
        }
        return spawnCount;
    }

    public int SpawnLineBottom(GameObject enemy)
    {
        Vector3 currentPosition = new Vector3(transform.position.x - 8, transform.position.y - 6, transform.position.z);
        float movePositionX = 0;
        int spawnCount = 16;
        int index = spawnCount;
        while (index > 0)
        {
            index--;
            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + movePositionX, currentPosition.y, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
            movePositionX += 1.1f;
        }
        return spawnCount;
    }

    public int SpawnLineRight(GameObject enemy)
    {
        Vector3 currentPosition = new Vector3(transform.position.x + 9.5f, transform.position.y + 4.5f, transform.position.z);
        float movePositionY = 0;
        int spawnCount = 9;
        int index = spawnCount;
        while (index > 0)
        {
            index--;
            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x, currentPosition.y - movePositionY, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
            movePositionY += 1.2f;
        }

        return spawnCount;
    }

    public int SpawnLineLeft(GameObject enemy)
    {
        Vector3 currentPosition = new Vector3(transform.position.x - 9.5f, transform.position.y + 4.5f, transform.position.z);
        float movePositionY = 0;
        int spawnCount = 9;
        int index = spawnCount;
        while (index > 0)
        {
            index--;
            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x, currentPosition.y - movePositionY, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
            movePositionY += 1.2f;
        }
        return spawnCount;
    }

    public int SpawnLineSwarm(GameObject enemy, bool isRight)
    {        
        int spawnCount = 13;

        if (isRight)
        {
            Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + 12, currentPosition.y + 2.2f, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);

            int spawn3 = 3;
            float movePositionX = 11;
            float movePositionY = 1.1f;
            
            while (spawn3 > 0)
            {
                ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + movePositionX, currentPosition.y + movePositionY, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                movePositionX += 1f;
                spawn3--;
            }

            int spawn5 = 5;
            movePositionX = 10;
            while (spawn5 > 0)
            {
                ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + movePositionX, currentPosition.y, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                movePositionX += 1f;
                spawn5--;
            }

            spawn3 = 3;
            movePositionX = 11;
            movePositionY = 1.1f;

            while (spawn3 > 0)
            {
                ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + movePositionX, currentPosition.y - movePositionY, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                movePositionX += 1f;
                spawn3--;
            }
            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + 12, currentPosition.y - 2.2f, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);

        }
        else
        {
            Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x - 12, currentPosition.y + 2.2f, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);

            int spawn3 = 3;
            float movePositionX = 11;
            float movePositionY = 1.1f;

            while (spawn3 > 0)
            {
                ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x - movePositionX, currentPosition.y + movePositionY, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                movePositionX += 1f;
                spawn3--;
            }

            int spawn5 = 5;
            movePositionX = 10;
            while (spawn5 > 0)
            {
                ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x - movePositionX, currentPosition.y, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                movePositionX += 1f;
                spawn5--;
            }

            spawn3 = 3;
            movePositionX = 11;
            movePositionY = 1.1f;

            while (spawn3 > 0)
            {
                ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x - movePositionX, currentPosition.y - movePositionY, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                movePositionX += 1f;
                spawn3--;
            }
            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x - 12, currentPosition.y - 2.2f, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
        }
        
        return spawnCount;
    }

    public int SpawnRandomEnemiesTop(GameObject enemy, int count, bool isTop)
    {
        Vector3 currentPosition;
        if (isTop)
        {
            currentPosition = new Vector3(transform.position.x, transform.position.y + 7, transform.position.z);
        }
        else
        {
            currentPosition = new Vector3(transform.position.x, transform.position.y - 7, transform.position.z);
        }
        int index = count;
        float movePositionX;
        float movePositionY;
        while (index > 0)
        {
            index--;
            movePositionX = Random.Range(-8.5f, 8.6f);
            movePositionY = Random.Range(-1, 2);
            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + movePositionX, currentPosition.y + movePositionY, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
        }
        return count;
    }

    public int SpawnRandomEnemiesRight(GameObject enemy, int count, bool isRight)
    {
        Vector3 currentPosition;
        if (isRight)
        {
            currentPosition = new Vector3(transform.position.x + 11, transform.position.y, transform.position.z);
        }
        else
        {
            currentPosition = new Vector3(transform.position.x - 11, transform.position.y, transform.position.z);
        }
        int index = count;
        float movePositionX;
        float movePositionY;
        while (index > 0)
        {
            index--;
            movePositionY = Random.Range(-6, 7);
            movePositionX = Random.Range(-1, 2);
            ObjectPoolManager.SpawnObject(enemy, new Vector3(currentPosition.x + movePositionX, currentPosition.y + movePositionY, currentPosition.z), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
        }
        return count;
    }
}
