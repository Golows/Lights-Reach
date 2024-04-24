using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<XPOrb> xpOrbs = new List<XPOrb>();

    [SerializeField] private float currentXp = 0f;
    [SerializeField] private float requiredXp = 0f;
    [SerializeField] private float requiredXpAdditive = 0f;
    [SerializeField] private int currentLevel = 0;

    [SerializeField] private float multiplier = 1.05f;
    [SerializeField] private int baseXpMult = 5;
    [SerializeField] private int baseXpAdd = 70;
    [SerializeField] private int afterMultiplyAdd = 0;

    private void Start()
    {
        NextLevel();
        TotalXpAdditive();
    }

    private void Update()
    {
#if CHEAT
        if (Input.GetKeyDown(KeyCode.C))
        {
            CollectAllOrbs();
        }
#endif
    }

    private void NextLevel()
    {
        currentLevel++;
        requiredXp = (baseXpMult * Mathf.Pow(multiplier, currentLevel) + baseXpAdd) * currentLevel + afterMultiplyAdd;
    }

    private void TotalXpAdditive()
    {
        requiredXpAdditive += requiredXp;
    }

    public void AddXp(int amount)
    {
        currentXp += amount;
        if(currentXp >= requiredXpAdditive)
        {
            NextLevel();
            TotalXpAdditive();
            Time.timeScale = 0;
            GameController.instance.upgradeManager.GetCards();
        }
    }

    public void CollectAllOrbs()
    {
        foreach (XPOrb orb in xpOrbs)
        {
            orb.collected = true;
        }
    }
    
    public void AddToOrbList(XPOrb xpOrb)
    {
        xpOrbs.Add(xpOrb);
    }

    public void RemoveFromOrbList(XPOrb xpOrb)
    {
        xpOrbs.Remove(xpOrb);
    }
}
