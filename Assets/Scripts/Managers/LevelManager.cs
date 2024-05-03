using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<XPOrb> xpOrbs = new List<XPOrb>();

    public float currentXp = 0f;
    public float currentXpNotAddative = 0f;
    public float requiredXp = 0f;
    public float requiredXpAdditive = 0f;
    public int currentLevel = 0;

    [SerializeField] private float multiplier = 1.05f;
    [SerializeField] private int baseXpMult = 5;
    [SerializeField] private int baseXpAdd = 70;
    [SerializeField] private int afterMultiplyAdd = 0;

    private void Start()
    {
        NextLevel(0);
        GameController.instance.uiManager.UpdateExperience();
        GameController.instance.uiManager.UpdateLevel();
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

    private void NextLevel(float overCap)
    {
        currentLevel++;
        requiredXp = (baseXpMult * Mathf.Pow(multiplier, currentLevel) + baseXpAdd) * currentLevel + afterMultiplyAdd;
        currentXpNotAddative = overCap;
        requiredXpAdditive += requiredXp;
        GameController.instance.uiManager.UpdateLevel();
    }

    public void AddXp(int amount)
    {
        currentXp += amount;
        currentXpNotAddative += amount;
        
        if(currentXp >= requiredXpAdditive)
        {
            NextLevel(currentXp - requiredXpAdditive);
            Time.timeScale = 0;
            GameController.instance.upgradeManager.GetCards();
        }
        GameController.instance.uiManager.UpdateExperience();
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
