using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderStationDetect : MonoBehaviour
{
    [SerializeField] MoveToFromStation moveToFromStation;
    [SerializeField] bool detect;

    public void StartMoveToStation()
    {
        moveToFromStation.PlayerToStation();
    }
}
