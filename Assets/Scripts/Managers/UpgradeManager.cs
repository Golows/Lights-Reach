using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private PlayerCharacter character;
    private WeaponData weaponData;
    [SerializeField] private float scalingMoveSpeedMax;
    [SerializeField] private float moveSpeedScaler;
    private float scalingMoveSpeed;

    private int moveSpeedCount = 0;
    public int projectileCount = 1;

    public int pierceCount = 1;

    private void Start()
    {
        character = GameController.instance.character.GetComponent<PlayerCharacter>();
        weaponData = character.weaponData;
    }

    public void Update()
    {
        // For testing
        if (Input.GetKeyDown(KeyCode.I))
        {
            MoveSpeedUpgrade(1.15f);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            FireBallCountUpgrade();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseFireBallPierce();
        }
    }

    public void MoveSpeedUpgrade(float increase)
    {
        if(moveSpeedCount < 7)
        {
            character.moveSpeed = character.moveSpeed * increase;
            moveSpeedCount++;
            scalingMoveSpeed = scalingMoveSpeedMax - (moveSpeedScaler * moveSpeedCount);
            character.dashSpeed = character.moveSpeed * (float)Math.Sqrt(scalingMoveSpeed);
            character.dashTime = character.dashLenght / character.dashSpeed;
        }
    }

    public void FireBallCountUpgrade()
    {
        if(projectileCount < 5)
            projectileCount += 1;
    }

    public void IncreaseFireBallPierce()
    {
        if(pierceCount < 5)
        {
            pierceCount += 1;
        }
    }
}
