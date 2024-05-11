using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    [SerializeField] private float scalingMoveSpeedMax;
    [SerializeField] private float moveSpeedScaler;
    private AbilityManager abilityManager;
    private float scalingMoveSpeed;

    private int moveSpeedCount = 0;
    public int projectileCount = 1;

    private float attackSpeedMult = 0, dmgMult = 0, msMult = 0, critMult = 0, healthMult = 0;

    private int random1, random2, random3;

    private GameObject card1, card2, card3;
    private Vector3 uiPosition;

    public int maxPierce = 5;


    //Stat card prefabs
    [SerializeField] private GameObject CardDamage1, CardDamage2, CardAttackSpeed1, CardAttackSpeed2,
                                        CardDamageReduction, CardMoveSpeed, CardCritChance, CardPierce,
                                        CardCritMulti, CardFireballPlus, CardHealthRegen, CardHealth, CardCurrentHealth;
    [SerializeField] private GameObject AbilityCardLightning, AbilityCardTornados;

    private List<GameObject> powerCards = new List<GameObject>();
    private List<GameObject> utilityCards = new List<GameObject>();
    private List<GameObject> abilityCards = new List<GameObject>();


    public bool pickingPowers = false;
    public bool pickingAbilities = false;

    private void Start()
    {
        playerCharacter = GameController.instance.character.GetComponent<PlayerCharacter>();
        abilityManager = GameController.instance.abilityManager;

        AddCardsToTable();
    }

    public void Update()
    {

#if CHEAT
        if (Input.GetKeyDown(KeyCode.I))
        {
            MoveSpeedUpgrade(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            FireBallCountUpgrade();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreasePierce();
        }
#endif

    }

    private void AddCardsToTable()
    {
        for (int i = 0; i < 25; i++)
        {
            utilityCards.Add(CardMoveSpeed);
        }
        for (int i = 0; i < 20; i++)
        {
            powerCards.Add(CardDamage1);
            powerCards.Add(CardAttackSpeed1);
            powerCards.Add(CardCritChance);

            utilityCards.Add(CardDamageReduction);
            utilityCards.Add(CardHealth);
        }
        for (int i = 0; i < 17; i++)
        {
            powerCards.Add(CardCritMulti);
        }
        for (int i = 0; i < 15; i++)
        {
            utilityCards.Add(CardHealthRegen);
        }
        for (int i = 0; i < 4; i++)
        {
            powerCards.Add(CardDamage2);
            utilityCards.Add(CardCurrentHealth);
        }
        for (int i = 0; i < 3; i++)
        {
            powerCards.Add(CardAttackSpeed2);
        }
        for (int i = 0; i < 5; i++)
        {
            powerCards.Add(CardPierce);
            powerCards.Add(CardFireballPlus);
        }

        abilityCards.Add(AbilityCardLightning);
        abilityCards.Add(AbilityCardTornados);
    }

    public void GetCards()
    {
        pickingPowers = true;
        if(!pickingAbilities)
        {
            ClearCards();

            uiPosition = GameController.instance.UI.transform.position;

            random1 = Random.Range(0, powerCards.Count);
            card1 = powerCards[random1];

            random2 = Random.Range(0, powerCards.Count);
            card2 = powerCards[random2];

            random3 = Random.Range(0, utilityCards.Count);
            card3 = utilityCards[random3];

            while (card1.name == card2.name || (card1 == CardDamage2 && card2 == CardDamage1) || (card1 == CardDamage1 && card2 == CardDamage2)
                                           || (card1 == CardAttackSpeed1 && card2 == CardAttackSpeed2) || (card1 == CardAttackSpeed2 && card2 == CardAttackSpeed1))
            {
                random2 = Random.Range(0, powerCards.Count);
                card2 = powerCards[random2];
            }

            card1.transform.position = new Vector3(uiPosition.x - Screen.width / 4, uiPosition.y, uiPosition.z);
            card2.transform.position = uiPosition;
            card3.transform.position = new Vector3(uiPosition.x + Screen.width / 4, uiPosition.y, uiPosition.z);

            card1.SetActive(true);
            card2.SetActive(true);
            card3.SetActive(true);
        }
    }

    public void GetAbilities()
    {
        pickingAbilities = true;
        ClearCards(); 

        uiPosition = GameController.instance.UI.transform.position;
        random1 = Random.Range(0, abilityCards.Count);
        card1 = abilityCards[random1];

        if(abilityCards.Count > 1)
        {
            random2 = Random.Range(0, abilityCards.Count);
            card2 = abilityCards[random2];
            while (card1.name == card2.name)
            {
                random2 = Random.Range(0, abilityCards.Count);
                card2 = abilityCards[random2];
            }
            card2.transform.position = uiPosition;
            card2.SetActive(true);
        }

        card1.transform.position = new Vector3(uiPosition.x - Screen.width / 4, uiPosition.y, uiPosition.z);

        card1.SetActive(true);
        
    }

    public void UnlockLightning()
    {
        abilityManager.SpawnLightning();
        abilityCards.Remove(AbilityCardLightning);
        RemoveCards();
        pickingAbilities = false;
    }

    public void UnlockTornado()
    {
        abilityManager.SpawnTornado();
        abilityCards.Remove(AbilityCardTornados);
        RemoveCards();
        pickingAbilities = false;
    }

    public void MoveSpeedUpgrade(float increase)
    {
        if(moveSpeedCount < 12)
        {
            msMult += increase;
            playerCharacter.moveSpeed = playerCharacter.baseMoveSpeed * (1 + msMult);
            moveSpeedCount++;
            scalingMoveSpeed = scalingMoveSpeedMax - (moveSpeedScaler * moveSpeedCount);
            playerCharacter.dashSpeed = playerCharacter.moveSpeed * Mathf.Sqrt(scalingMoveSpeed);
            playerCharacter.dashTime = playerCharacter.dashLenght / playerCharacter.dashSpeed;
        }

        if(moveSpeedCount == 12)
        {
            for (int i = 0; i < utilityCards.Count; i++)
            {
                if (utilityCards[i] == CardMoveSpeed)
                {
                    utilityCards.RemoveAt(i);
                    i--;
                }
            }
        }
        RemoveCards();
        pickingPowers = false;
    }

    public void CurrentHealthIncrease(float increase)
    {
        if(playerCharacter.currentHealth + increase <= playerCharacter.health)
        {
            playerCharacter.currentHealth += increase;
        }
        else
        {
            playerCharacter.currentHealth += playerCharacter.health - playerCharacter.currentHealth;
        }
        GameController.instance.uiManager.UpdateHealth();
        RemoveCards();
        pickingPowers = false;
    }

    public void FireBallCountUpgrade()
    {            
        if (projectileCount < 5)
        {
                projectileCount += 1;
        }

        if (projectileCount == 5)
        {
            for (int i = 0; i < powerCards.Count; i++)
            {
                if (powerCards[i] == CardFireballPlus)
                {
                    powerCards.RemoveAt(i);
                    i--;
                }
            }
        }

        RemoveCards();
        pickingPowers = false;
    }

    public void IncreasePierce()
    {
        if(playerCharacter.pierce < maxPierce)
        {
            playerCharacter.pierce += 1;
        }

        if(playerCharacter.pierce == maxPierce)
        {
            for(int i = 0; i < powerCards.Count; i++)
            {
                if (powerCards[i] == CardPierce)
                {
                    powerCards.RemoveAt(i);
                    i--;
                }
            }
        }

        RemoveCards();
        pickingPowers = false;
    }

    public void UpgradeDamage(float amount)
    {
        dmgMult += amount;
        playerCharacter.damage = playerCharacter.baseDamage * (1 + dmgMult);
        RemoveCards();
        pickingPowers = false;
    }

    public void UpgradeHpRegeneration(float amount)
    {
        playerCharacter.healthRegen += amount;
        RemoveCards();
        pickingPowers = false;
    }

    public void UpgradeHealth(float amount)
    {
        healthMult += amount;
        float increaseAmount = playerCharacter.baseHealth * (1 + healthMult) - playerCharacter.health;
        playerCharacter.currentHealth += increaseAmount;
        playerCharacter.health += increaseAmount;
        GameController.instance.uiManager.UpdateHealth();
        RemoveCards();
        pickingPowers = false;
    }

    public void UpgradeAttackSpeed(float amount)
    {
        attackSpeedMult += amount;
        playerCharacter.attackSpeed = playerCharacter.baseAttackSpeed * (1 + attackSpeedMult);
        RemoveCards();
        pickingPowers = false;
    }

    public void UpgradeCritMulti(float amount)
    {
        critMult += amount;
        playerCharacter.critMultiplier = playerCharacter.baseCritMultiplier * (1 + critMult);
        RemoveCards();
        pickingPowers = false;
    }

    public void UpgradeDamageReduction(float amount)
    {
        playerCharacter.damageReduction += amount;

        RemoveCards();
        if (playerCharacter.damageReduction == 50)
        {
            for (int i = 0; i < utilityCards.Count; i++)
            {
                if (utilityCards[i] == CardDamageReduction)
                {
                    utilityCards.RemoveAt(i);
                    i--;
                }
            }
        }
        pickingPowers = false;
    }

    public void UpgradeCritChance(float amount)
    {
        playerCharacter.critChance += amount;
        RemoveCards();
        pickingPowers = false;
    }

    private void ClearCards()
    {
        if (card1 != null)
        {
            card1.SetActive(false);
            card1 = null;
        }
        if (card2 != null)
        {
            card2.SetActive(false);
            card3 = null;
        }
        if (card3 != null)
        {
            card3.SetActive(false);
            card3 = null;
        }
    }

    private void RemoveCards()
    {
        if (card1 != null && card2 != null)
        {
            card1.SetActive(false);
            card2.SetActive(false);
            if(card3 != null)
                card3.SetActive(false);
        }

        Time.timeScale = 1;
    }
}
