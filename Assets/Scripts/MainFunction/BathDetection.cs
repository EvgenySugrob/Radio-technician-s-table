using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathDetection : MonoBehaviour
{
    [SerializeField] PlayerMoveToBath playerMoveToBath;
    public bool detect { get; set; }

    public void StartMoveToStation()
    {
        playerMoveToBath.PlayerToStation();
    }
}
