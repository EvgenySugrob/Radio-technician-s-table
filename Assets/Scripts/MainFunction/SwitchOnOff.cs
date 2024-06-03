using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    [SerializeField] SolderStation solderStation;
    [SerializeField] Transform onOffButton;

    [Header("Optional")]
    [SerializeField] bool isMainSwitch;
    [SerializeField] MeshRenderer solderPowerOnIndicator;
    private Vector3 startAngle;
    private Vector3 targetAngle = new Vector3(0,0,0);

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
            onOffButton.localEulerAngles = startAngle;
            solderStation.powerIsEnable = powerOn;
            DisableStationPower();
        }
        else
        {
            powerOn = true;
            EnebleStationPower();
            onOffButton.localEulerAngles = targetAngle;
            solderStation.powerIsEnable =powerOn;
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
                Debug.Log("Не включен");
            }
        }
    }

    public void DisableStationPower()
    {
        solderPowerOnIndicator.material.DisableKeyword("_EMISSION");
    }
    public void EnebleStationPower()
    {
        if(solderStation.stationIsEnable)
        {
            solderPowerOnIndicator.material.EnableKeyword("_EMISSION");
        }
    }
}
