using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public PlayerProgress playerProgress;
    private PlayerCharacter playerCharacter;
    public int gameCoins = 0;

    public float healthIncrease;
    public float damageIncrease;
    public float attackSpeedIncrease;
    public float pickupRangeIncrease;
    public float moveSpeedIncrease;
    public float critChangeIncrease;
    public float damageReductionIncrease;
    public float healthRegenIncrease;
    public float dashCooldownDecrease;

    public bool startingArea = false;

    public void UpdateStatsFromProgress()
    {
        gameCoins = 0;

        playerCharacter = GameController.instance.playerCharacter;

        if (SaveSystem.FileExists())
        {
            playerProgress = SaveSystem.LoadProgress();
            playerCharacter.SetStartStats(playerProgress);
        }
        else
        {
            playerProgress = new PlayerProgress(0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            SaveSystem.SaveProgress(playerProgress);
            playerCharacter.SetStartStats(playerProgress);
        }

        if(startingArea)
        {
            GameController.instance.uiManager.UpdateToalCoins();
        }
    }

    public void SaveCoinProgress()
    {
        playerProgress.coins += gameCoins;
        SaveSystem.SaveProgress(playerProgress);
    }
   
}

