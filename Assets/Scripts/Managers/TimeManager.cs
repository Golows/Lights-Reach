using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float loopTime = 600;
    [SerializeField] private TextMeshProUGUI timer;
    public double timeLeft = 0;
    public bool beginPlay = false;

    public bool timeLeft7 = false;
    public bool timeLeft3 = false;

    private void Start()
    {
        timeLeft = loopTime;
        beginPlay = true;
    }

    private void Update()
    {
        if (beginPlay)
        {
            RunTimer();
        }
    }

    private void RunTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeLeft = System.Math.Round(timeLeft, 2);
            timer.text = Mathf.FloorToInt((float)timeLeft / 60).ToString() + ":" + Mathf.RoundToInt((float)timeLeft % 60).ToString();
        }
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }

        if (timeLeft < (loopTime - 180))
        {
            timeLeft7 = true;
        }
        if (timeLeft < (loopTime - 420))
        {
            timeLeft3 = true;
        }
    }
}
