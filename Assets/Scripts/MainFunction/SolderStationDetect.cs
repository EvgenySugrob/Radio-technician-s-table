using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderStationDetect : MonoBehaviour
{
    [SerializeField] MoveToFromStation moveToFromStation;
    public bool detect { get; set; }

    public void StartMoveToStation()
    {
        moveToFromStation.PlayerToStation();
    }
}
