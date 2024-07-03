using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    [SerializeField] SolderStation solderStation;
    [SerializeField] UltrasonicBath ultrasonicBath;
    [SerializeField] Transform onOffButton;

    [Header("Optional")]
    [SerializeField] bool isMainSwitch;
    [SerializeField] bool isSolderStationSwitch;
    [SerializeField] bool isUltrasonicBath;
    [SerializeField] MeshRenderer solderPowerOnIndicator;
    [SerializeField] MeshRenderer ultrasonicBathPowerIndicator;
    [SerializeField] GameObject uiBath;
    private Vector3 startAngle;
    private Vector3 targetAngle = new Vector3(0, 0, 0);

    [Header("LogMessage")]
    [SerializeField] LogMessageSpawn logMessageSpawn;

    private bool powerOn;

    private void Start()
    {
        startAngle = onOffButton.localEulerAngles;
    }

    public void ButtonTurnOnOff()
    {
        if (isMainSwitch)
        {
            EnableMainSwitch();
        }
        else if (isSolderStationSwitch) 
        {
            EnableStationSwitch();
        }
        else if(isUltrasonicBath)
        {
            EnableUltrasonicBath();
        }
    }

    private void EnableMainSwitch()
    {
        if (powerOn)
        {
            powerOn = false;
            onOffButton.localEulerAngles = startAngle;
            solderStation.powerIsEnable = powerOn;
            ultrasonicBath.ultrasonicBathEnable = powerOn;
            DisableStationPower();
        }
        else
        {
            powerOn = true;
            EnebleStationPower();
            onOffButton.localEulerAngles = targetAngle;
            solderStation.powerIsEnable =powerOn;
            ultrasonicBath.ultrasonicBathEnable=powerOn;
        }
    }
    private void EnableStationSwitch()
    {
        if (powerOn)
        {
            powerOn = false;
            onOffButton.localEulerAngles = startAngle;
            solderStation.stationIsEnable = powerOn;

            solderPowerOnIndicator.material.DisableKeyword("_EMISSION");
        }
        else
        {
            powerOn = true;
            onOffButton.localEulerAngles = targetAngle;
            solderStation.stationIsEnable = powerOn;

            if (solderStation.plugInSocket && solderStation.powerIsEnable)
            {
                solderPowerOnIndicator.material.EnableKeyword("_EMISSION");
            }
            else
            {
                logMessageSpawn.GetTextMessageInLog(false, "Станция не включена в розетку.");
                Debug.Log("Не включен");
            }
        }
    }

    public void EnableUltrasonicBath()
    {
        if (powerOn)
        {
            powerOn = false;
            onOffButton.localEulerAngles = startAngle;
            ultrasonicBath.ultrasonicBathEnable= false;

            uiBath.SetActive(false);
            ultrasonicBathPowerIndicator.material.DisableKeyword("_EMISSION");
        }
        else
        {
            powerOn= true;
            onOffButton.localEulerAngles= targetAngle;
            ultrasonicBath.ultrasonicBathEnable = true;

            if (ultrasonicBath.plugInSocket && ultrasonicBath.ultrasonicBathEnable)
            {
                ultrasonicBathPowerIndicator.material.EnableKeyword("_EMISSION");
                uiBath.SetActive(true);
            }
            else
            {
                logMessageSpawn.GetTextMessageInLog(false, "УЗ-ванна не включена в розетку.");
                Debug.Log("Не включен");
            }
        }
    }

    public void DisableStationPower()
    {
        uiBath.SetActive(false);
        solderPowerOnIndicator.material.DisableKeyword("_EMISSION");
        ultrasonicBathPowerIndicator.material.DisableKeyword("_EMISSION");
    }
    public void EnebleStationPower()
    {
        if(solderStation.stationIsEnable)
        {
            solderPowerOnIndicator.material.EnableKeyword("_EMISSION");
        }
        if(ultrasonicBath.ultrasonicBathEnable)
        {
            ultrasonicBathPowerIndicator.material.EnableKeyword("_EMISSION");
        }
            
    }
}
