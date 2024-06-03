using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureRegulatorSetting : MonoBehaviour
{
    [SerializeField] Transform regulator;
    [SerializeField] SolderStation solderStation;
    [SerializeField] List<Vector3> regulatorPositionList;

    public void SetRegulatorTemperature(int index)
    {
        regulator.localRotation = Quaternion.Euler(regulatorPositionList[index]);

        switch (index) 
        {
            case 0:
                solderStation.solderTemperature = 200;
                break; 
                
            case 1:
                solderStation.solderTemperature = 225;
                break;

            case 2:
                solderStation.solderTemperature = 250;
                break;

            case 3:
                solderStation.solderTemperature = 275;
                break;

            case 4:
                solderStation.solderTemperature = 300;
                break;

            case 5:
                solderStation.solderTemperature = 325;
                break;

            case 6:
                solderStation.solderTemperature = 350;
                break;

            case 7:
                solderStation.solderTemperature = 375;
                break;

            case 8:
                solderStation.solderTemperature = 400;
                break;

            case 9:
                solderStation.solderTemperature = 425;
                break;

            case 10:
                solderStation.solderTemperature = 450;
                break;

            case 11:
                solderStation.solderTemperature = 480;
                break;

        }
    }
}