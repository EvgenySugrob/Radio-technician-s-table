using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderingIronTipTriggerZone : MonoBehaviour
{
    [SerializeField] SolderInteract solderInteract;

    [SerializeField] BoxCollider triggerZone;
    [SerializeField] string rosinTag;
    [SerializeField] string solderTag;



    private void Start()
    {
        triggerZone= GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == rosinTag)
        {
            solderInteract.RosinCheck(true);
        }
        else if (other.tag == solderTag)
        {
            solderInteract.IronTinningCheck(true,other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == rosinTag)
        {
            solderInteract.RosinCheck(true);
        }
        else if (other.tag == solderTag)
        {
            Debug.Log(other.name);
            solderInteract.IronTinningCheck(true,other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == rosinTag)
        {
            solderInteract.RosinCheck(false);
        }
        else if (other.tag == solderTag)
        {
            solderInteract.IronTinningCheck(false,other.transform);
        }
    }
}