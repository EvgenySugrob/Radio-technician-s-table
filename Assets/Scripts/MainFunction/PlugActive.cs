using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugActive : MonoBehaviour
{
    [SerializeField] SolderStation solderStation;
    [SerializeField] SwitchOnOff stationSwitch;

    private BoxCollider boxCollider;
    private Animator animator;
    private bool inSocket;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider= GetComponent<BoxCollider>();
    }

    public void PlugInSocket()
    {
        if (inSocket) 
        {
            animator.SetBool("EnablePlug", false);
            boxCollider.enabled = false;
            StartCoroutine(WaitToEndTurnOffAnimmation());
        }
        else
        {
            animator.SetBool("EnablePlug", true);
            boxCollider.enabled = false;
            StartCoroutine(WaitToEndTurnOnAnimation());
        }
    }

    IEnumerator WaitToEndTurnOnAnimation()
    {
        yield return new WaitForSeconds(0.45f);
        solderStation.plugInSocket = true;
        boxCollider.enabled = true;
        inSocket= true;
        stationSwitch.EnebleStationPower();
    }

    IEnumerator WaitToEndTurnOffAnimmation()
    {
        solderStation.plugInSocket = false;
        stationSwitch.DisableStationPower();
        yield return new WaitForSeconds(0.45f);
        boxCollider.enabled = true;
        inSocket= false;
    }
}
