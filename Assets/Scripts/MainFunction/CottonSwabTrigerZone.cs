using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonSwabTrigerZone : MonoBehaviour
{
    [SerializeField] CottonSwabControl cottonSwabControl;
    [SerializeField] GameObject startFluxingBt;
    [SerializeField] QuickOutlineController quickOutlineController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Flux" && cottonSwabControl.isFluxed == false)
        {
            startFluxingBt.SetActive(true);
            if(other.GetComponent<QuickOutlineController>())
            {
                quickOutlineController = other.GetComponent<QuickOutlineController>();
                quickOutlineController.EnableOutline();
            }
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
            if (other.GetComponent<QuickOutlineController>())
            {
                quickOutlineController = other.GetComponent<QuickOutlineController>();
                quickOutlineController.DisableOutline();
            }
        }
    }
    public void DisableStartBt()
    {
        startFluxingBt.SetActive(false);
    }
    public void DisableTriggerOutline()
    {
        if (quickOutlineController!=null)
        {
            quickOutlineController.DisableOutline();
        }
        quickOutlineController= null;
    }
}
