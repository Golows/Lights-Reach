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

    public float healthMultiplier = 1;

    public int min, sec;

    private void Start()
    {
        timeLeft = loopTime;
        beginPlay = true;
        StartCoroutine(Every1Min());
    }

    private void FixedUpdate()
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
            timeLeft -= Time.fixedDeltaTime;
            timeLeft = System.Math.Round(timeLeft, 2);
            min = Mathf.FloorToInt((float)timeLeft / 60);
            sec = Mathf.FloorToInt((float)timeLeft % 60);
            timer.text = min.ToString() + ":" + sec.ToString();
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

    IEnumerator Every1Min()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f);
            healthMultiplier += 0.3f;
        }
    }
}
