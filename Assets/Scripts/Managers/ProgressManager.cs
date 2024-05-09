using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI dmgText;
    public TextMeshProUGUI asText;
    public TextMeshProUGUI pickupText;
    public TextMeshProUGUI msText;
    public TextMeshProUGUI critText;
    public TextMeshProUGUI dmgRedText;
    public TextMeshProUGUI hpRegenText;
    public TextMeshProUGUI dashCdText;

    public Button hpButton;
    public Button dmgButton;
    public Button asButton;
    public Button pickupButton;
    public Button msButton;
    public Button critButton;
    public Button dmgRedButton;
    public Button hpRegenButton;
    public Button dashCdButton;

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
        if (startingArea)
        {
            UpdateProgressMenue();
            GameController.instance.uiManager.UpdateToalCoins();
        }
    }

    public void SaveCoinProgress()
    {
        playerProgress.coins += gameCoins;
        SaveSystem.SaveProgress(playerProgress);
    }

    public void UpdateProgressMenue()
    {
        hpText.text = "base health +" + (healthIncrease * playerProgress.healthUpgrade).ToString();
        dmgText.text = "base damage +" + (damageIncrease * playerProgress.damageUpgrade).ToString();
        asText.text = "base attack speed +" + (attackSpeedIncrease * playerProgress.attackSpeedUpgrade).ToString();
        pickupText.text = "pickup range +" + (pickupRangeIncrease * playerProgress.pickupRangeUpgrade).ToString();
        msText.text = "base move speed +" + (moveSpeedIncrease * playerProgress.moveSpeedUpgrade).ToString();
        critText.text = "crit chance +" + (critChangeIncrease * playerProgress.critChangeUpgrade).ToString();
        dmgRedText.text = "damage reduction +" + (damageReductionIncrease * playerProgress.damageReductionUpgrade).ToString();
        hpRegenText.text = "health regeneration +" + (healthRegenIncrease * playerProgress.healthRegenUpgrade).ToString();
        dashCdText.text = "dash cooldown -" + (dashCooldownDecrease * playerProgress.dashCooldownUpgrade).ToString();

        if (playerProgress.healthUpgrade == 5) { hpButton.gameObject.SetActive(false); }
        if (playerProgress.damageUpgrade == 5) { dmgButton.gameObject.SetActive(false); }
        if (playerProgress.attackSpeedUpgrade == 5) { asButton.gameObject.SetActive(false); }
        if (playerProgress.pickupRangeUpgrade == 5) { pickupButton.gameObject.SetActive(false); }
        if (playerProgress.moveSpeedUpgrade == 5) { msButton.gameObject.SetActive(false); }
        if (playerProgress.critChangeUpgrade == 5) { critButton.gameObject.SetActive(false); }
        if (playerProgress.damageReductionUpgrade == 5) { dmgRedButton.gameObject.SetActive(false); }
        if (playerProgress.healthRegenUpgrade == 5) { hpRegenButton.gameObject.SetActive(false); }
        if (playerProgress.dashCooldownUpgrade == 5) { dashCdButton.gameObject.SetActive(false); }
    }
   
    public void UpgradeHealth()
    {
        if(playerProgress.healthUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.healthUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }

    public void UpgradeDamage()
    {
        if (playerProgress.damageUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.damageUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (playerProgress.attackSpeedUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.attackSpeedUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }

    public void UpgradePickUpRange()
    {
        if (playerProgress.pickupRangeUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.pickupRangeUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }

    public void UpgradeMoveSpeed()
    {
        if (playerProgress.moveSpeedUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.moveSpeedUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }

    public void UpgradeCrit()
    {
        if (playerProgress.critChangeUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.critChangeUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }

    public void UpgradeDamageRed()
    {
        if (playerProgress.damageReductionUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.damageReductionUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }

    public void UpgradeHealthRegen()
    {
        if (playerProgress.healthRegenUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.healthRegenUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }

    public void UpgradeDashCd()
    {
        if (playerProgress.dashCooldownUpgrade < 5 && playerProgress.coins >= 2000)
        {
            playerProgress.dashCooldownUpgrade += 1;
            playerProgress.coins -= 2000;
            GameController.instance.uiManager.UpdateToalCoins();
            UpdateProgressMenue();
            SaveSystem.SaveProgress(playerProgress);
        }
    }
}

