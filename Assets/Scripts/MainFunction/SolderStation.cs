using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderStation : MonoBehaviour
{
    public bool plugInSocket { get; set; }
    public bool powerIsEnable { get; set; }
    public bool stationIsEnable { get; set; }

    public int solderTemperature { get; set; }

    public bool FunctionalityCheck()
    {
        if(plugInSocket && powerIsEnable && stationIsEnable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
