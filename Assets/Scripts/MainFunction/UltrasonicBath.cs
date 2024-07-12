using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UltrasonicBath : MonoBehaviour
{
    public bool ultrasonicBathEnable { get; set; }

    public bool plugInSocket { get; set; }

    [SerializeField] bool isStartWork;

    [Header("UltrasonicBath material")]
    [SerializeField] MeshRenderer ultrasonicIndicator;
    [SerializeField] MeshRenderer heatIndicator;

    [Header("WorldUI")]
    [SerializeField] GameObject uiBath;
    [SerializeField] List<GameObject> buttonsControl;

    [Header("TabloTime")]
    [SerializeField] TMP_Text timeText;
    [SerializeField] float currentTime = 10;
    [SerializeField] float timeStep = 1;
    private float minTime = 1;
    private float maxTime = 99;
    private float timeBeforStart = 10;

    [Header("TemperatureTime")]
    [SerializeField] TMP_Text temperatureText;
    [SerializeField] float currentTemp = 20;
    [SerializeField] float temperatureStep = 1;
    private float minTemperature = 20;
    private float maxTemperature = 80;

    [Header("BoardDetection")]
    [SerializeField] UltrasonicBathBoardDetection boardDetection;

    [Header("Log")]
    [SerializeField] LogMessageSpawn logMessageSpawn;

    [SerializeField] bool isTimerWaitStart;
    [SerializeField] float timerToStart;
    private float secondInMinute = 60;    

    public void SetTimeJob(float step)
    {
        if (isStartWork == false)
        {
            float timeStep = currentTime;
            timeStep += step;

            if (minTime <= timeStep && timeStep <= maxTime)
            {
                currentTime = timeStep;
                timeText.text = MathF.Round(currentTime).ToString();
            }
        }
    }

    public void SetTemperatureJob(float step)
    {
        if (isStartWork == false)
        {
            float temperatureStep = currentTemp;
            temperatureStep += step;

            if (minTemperature <= temperatureStep && temperatureStep <= maxTemperature)
            {
                currentTemp = temperatureStep;
                temperatureText.text = MathF.Round(currentTemp).ToString();
            }
        }
    }

    private void Update()
    {
        if(ultrasonicBathEnable && plugInSocket)
        {
            if (isStartWork)
            {
                CountdownTimerWork();
            }
        }
        else
        {
            isStartWork = false;
            uiBath.SetActive(false);
            ultrasonicIndicator.material.DisableKeyword("_EMISSION");
            heatIndicator.material.DisableKeyword("_EMISSION");
            currentTime = timeBeforStart;
            timeText.text = MathF.Round(currentTime).ToString();
        }
    }

    private void CountdownTimerWork()
    {
        if(currentTime>0)
        {
            currentTime -= Time.deltaTime;
            timeText.text = MathF.Round(currentTime).ToString();
        }
        else
        {
            isStartWork = false;
            currentTime = timeBeforStart;
            timeText.text = MathF.Round(currentTime).ToString();
            ultrasonicIndicator.material.DisableKeyword("_EMISSION");
            heatIndicator.material.DisableKeyword("_EMISSION");
            foreach (GameObject item in buttonsControl)
            {
                item.SetActive(true);
            }
        }
    }

    public void StartUltrasonicBath()
    {
        if(boardDetection.boardIsPoint)
        {
            if (isStartWork)
            {
                isStartWork = false;
                foreach (GameObject item in buttonsControl)
                {
                    item.SetActive(true);
                }

                currentTime = timeBeforStart;
                ultrasonicIndicator.material.DisableKeyword("_EMISSION");
                heatIndicator.material.DisableKeyword("_EMISSION");
            }
            else
            {
                foreach (GameObject item in buttonsControl)
                {
                    item.SetActive(false);
                }
                timeBeforStart = currentTime;
                ultrasonicIndicator.material.EnableKeyword("_EMISSION");
                heatIndicator.material.EnableKeyword("_EMISSION");
                isStartWork = true;
            }
        }
        else
        {
            logMessageSpawn.GetTextMessageInLog(false, "Отсутствует плата в УЗ-ванне.");
        }
       
    }
}
