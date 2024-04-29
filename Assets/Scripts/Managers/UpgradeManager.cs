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
    private float scalingMoveSpeed;

    private int moveSpeedCount = 0;
    public int projectileCount = 1;

    private float attackSpeedMult = 0, dmgMult = 0, msMult = 0, critMult = 0;

    private int random1, random2, random3;

    private GameObject card1, card2, card3;


    //Stat card prefabs
    [SerializeField] private GameObject CardDamage1, CardDamage2, CardAttackSpeed1, CardAttackSpeed2,
                                        CardDamageReduction, CardMoveSpeed, CardCritChance, CardPierce,
                                        CardCritMulti, CardFireballPlus;

    private List<GameObject> powerCards = new List<GameObject>();
    private List<GameObject> utilityCards = new List<GameObject>(); 

    private void Start()
    {
        playerCharacter = GameController.instance.character.GetComponent<PlayerCharacter>();

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
        }
        for (int i = 0; i < 17; i++)
        {
            powerCards.Add(CardCritMulti);
        }
        for (int i = 0; i < 4; i++)
        {
            powerCards.Add(CardDamage2);
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
    }

    public void GetCards()
    {
        Vector3 uiPosition = GameController.instance.UI.transform.position;

        random1 = Random.Range(0, powerCards.Count);
        card1 = powerCards[random1];

        random2 = Random.Range(0, powerCards.Count);
        card2 = powerCards[random2];

        if(utilityCards.Count > 0)
        {
            random3 = Random.Range(0, utilityCards.Count);
            card3 = utilityCards[random3];
            card3.transform.position = new Vector3(uiPosition.x + 500f, uiPosition.y, uiPosition.z);
            card3.SetActive(true);
        }

        while(card1.name == card2.name || (card1 == CardDamage2 && card2 == CardDamage1) || (card1 == CardDamage1 && card2 == CardDamage2)
                                       || (card1 == CardAttackSpeed1 && card2 == CardAttackSpeed2) || (card1 == CardAttackSpeed2 && card2 == CardAttackSpeed1))
        {
            random2 = Random.Range(0, powerCards.Count);
            card2 = powerCards[random2];
        }

        card1.transform.position = new Vector3(uiPosition.x - 500f, uiPosition.y, uiPosition.z);
        card2.transform.position = uiPosition;
        
        card1.SetActive(true);
        card2.SetActive(true);
        
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
    }

    public void IncreasePierce()
    {
        if(playerCharacter.pierce < 5)
        {
            playerCharacter.pierce += 1;
        }

        if(playerCharacter.pierce == 5)
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
    }

    public void UpgradeDamage(float amount)
    {
        dmgMult += amount;
        playerCharacter.damage = playerCharacter.baseDamage * (1 + dmgMult);
        RemoveCards();
    }


    public void UpgradeAttackSpeed(float amount)
    {
        attackSpeedMult += amount;
        playerCharacter.attackSpeed = playerCharacter.baseAttackSpeed * (1 + attackSpeedMult);
        RemoveCards();
    }

    public void UpgradeCritMulti(float amount)
    {
        critMult += amount;
        playerCharacter.critMultiplier = playerCharacter.baseCritMultiplier * (1 + critMult);
        RemoveCards();
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
    }

    public void UpgradeCritChance(float amount)
    {
        playerCharacter.critChance += amount;
        RemoveCards();
    }

    private void RemoveCards()
    {
        if (card1 != null && card2 != null)
        {
            card1.SetActive(false);
            card2.SetActive(false);
            card3.SetActive(false);
        }
        

        Time.timeScale = 1;
    }
}
