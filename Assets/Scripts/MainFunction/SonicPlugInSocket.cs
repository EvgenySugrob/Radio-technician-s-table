using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicPlugInSocket : MonoBehaviour
{
    [SerializeField] UltrasonicBath ultrasonicBath;
    [SerializeField] SwitchOnOff ultrasonicBathSwitch;

    private BoxCollider boxCollider;
    private Animator animator;
    private bool inSocket;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void PlugInSocket()
    {
        if (inSocket) 
        {
            animator.SetBool("SonicEnablePlug", false);
            boxCollider.enabled = false;
            StartCoroutine(WaitToEndTurnOffAnimmation());
        }
        else
        {
            animator.SetBool("SonicEnablePlug", true);
            boxCollider.enabled = false;
            StartCoroutine(WaitToEndTurnOnAnimation());
        }
    }

    IEnumerator WaitToEndTurnOnAnimation()
    {
        yield return new WaitForSeconds(0.45f);
        ultrasonicBath.plugInSocket = true;
        boxCollider.enabled = true;
        inSocket = true;
        ultrasonicBathSwitch.EnebleStationPower();
    }

    IEnumerator WaitToEndTurnOffAnimmation()
    {
        ultrasonicBath.plugInSocket = false;
        ultrasonicBathSwitch.DisableStationPower();
        yield return new WaitForSeconds(0.45f);
        boxCollider.enabled = true;
        inSocket = false;
    }
}
