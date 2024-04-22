using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<XPOrb> xpOrbs = new List<XPOrb>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CollectAllOrbs();
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
