using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltrasonicBathBoardDetection : MonoBehaviour
{
    public bool boardIsPoint { get; set; }

    [SerializeField] Transform boardPoint;
    [SerializeField] GameObject lockBoardInBath;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] BoxCollider boardPickupCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ForHolder")
        {
            lockBoardInBath.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ForHolder")
        {
            lockBoardInBath.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ForHolder")
        {
            lockBoardInBath.SetActive(false);
        }
    }

    public void DisableTriggerZone(bool isOn)
    {
        boxCollider.enabled = isOn;
        boardPickupCollider.enabled = !isOn;
        boardIsPoint = !isOn;
    }
}
