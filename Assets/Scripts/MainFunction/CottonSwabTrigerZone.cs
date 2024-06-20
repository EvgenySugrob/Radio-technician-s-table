using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonSwabTrigerZone : MonoBehaviour
{
    [SerializeField] CottonSwabControl cottonSwabControl;
    [SerializeField] GameObject startFluxingBt;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Flux" && cottonSwabControl.isFluxed == false)
        {
            startFluxingBt.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Flux" && cottonSwabControl.isFluxed == false)
        {
            startFluxingBt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Flux" && cottonSwabControl.isFluxed == false)
        {
            startFluxingBt.SetActive(false);
        }
    }
    public void DisableStartBt()
    {
        startFluxingBt.SetActive(false);
    }
}
