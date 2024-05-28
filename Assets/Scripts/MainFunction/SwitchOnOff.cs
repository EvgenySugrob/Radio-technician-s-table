using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    [SerializeField] SolderStation solderStation;
    [SerializeField] bool isMainSwitch;

    [SerializeField] MeshRenderer solderPowerOnIndicator;
    private Vector3 startAngle;
    private Vector3 targetAngle = new Vector3(0,0,0);

    private bool powerOn;

    private void Start()
    {
        startAngle = transform.localEulerAngles;
    }

    public void ButtonTurnOnOff()
    {
        if (isMainSwitch)
        {
            EnableMainSwitch();
        }
        else
        {
            EnableStationSwitch();
        }
    }

    private void EnableMainSwitch()
    {
        if (powerOn)
        {
            powerOn = false;
            transform.localEulerAngles = startAngle;
            solderStation.powerIsEnable = powerOn;
            DisableStationPower();
        }
        else
        {
            powerOn = true;
            transform.localEulerAngles = targetAngle;
            solderStation.powerIsEnable =powerOn;
        }
    }
    private void EnableStationSwitch()
    {
        if (powerOn)
        {
            DisableStationPower();
        }
        else
        {
            powerOn = true;
            transform.localEulerAngles = targetAngle;
            solderStation.stationIsEnable = powerOn;

            if (solderStation.plugInSocket && solderStation.powerIsEnable)
            {
                solderPowerOnIndicator.material.EnableKeyword("_EMISSION");
            }
            else
            {
                Debug.Log("Не включен");
            }
        }
    }

    private void DisableStationPower()
    {
        powerOn = false;
        transform.localEulerAngles = startAngle;
        solderStation.stationIsEnable = powerOn;

        solderPowerOnIndicator.material.DisableKeyword("_EMISSION");
    }
}
