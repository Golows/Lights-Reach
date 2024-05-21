using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress 
{
    public int coins;
    public int healthUpgrade;
    public int damageUpgrade;
    public int attackSpeedUpgrade;
    public int pickupRangeUpgrade;
    public int moveSpeedUpgrade;
    public int critChangeUpgrade;
    public int damageReductionUpgrade;
    public int healthRegenUpgrade;
    public int dashCooldownUpgrade;

    public PlayerProgress(int coins ,int healthUpgrade, int damageUpgrade, int attackSpeedUpgrade, int pickupRangeUpgrade,
                          int moveSpeedUpgrade, int critChangeUpgrade, int damageReductionUpgrade,
                          int healthRegenUpgrade, int dashCooldownUpgrade)
    {
        this.coins = coins;
        this.healthUpgrade = healthUpgrade;
        this.damageUpgrade = damageUpgrade;
        this.attackSpeedUpgrade = attackSpeedUpgrade;
        this.pickupRangeUpgrade = pickupRangeUpgrade;
        this.moveSpeedUpgrade = moveSpeedUpgrade;
        this.critChangeUpgrade = critChangeUpgrade;
        this.damageReductionUpgrade = damageReductionUpgrade;
        this.healthRegenUpgrade = healthRegenUpgrade;
        this.dashCooldownUpgrade = dashCooldownUpgrade;
    }
}
