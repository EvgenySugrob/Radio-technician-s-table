using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadioElements : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " Без условий");
        if (other.tag == "Tweezers")
        {
            Tweezers tweezers = other.GetComponent<Tweezers>();
            tweezers.ActiveOrtoViewBt(true);
            //if (tweezers.RadioelementIsNull() == false)
            //{
                
            //}
        }
        else if(other.tag == "Solder")
        {
            other.GetComponent<SolderInteract>().ActiveOrtoBt(true);
        }
        else if(other.tag == "SwabGroup")
        {
            other.GetComponent<CottonSwabControl>().ActiveOrtoview(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tweezers")
        {
            Tweezers tweezers = other.GetComponent<Tweezers>();
            tweezers.ActiveOrtoViewBt(true);
            //if (tweezers.RadioelementIsNull() == false)
            //{
                
            //}
        }
        else if (other.tag == "Solder")
        {
            other.GetComponent<SolderInteract>().ActiveOrtoBt(true);
        }
        else if (other.tag == "SwabGroup")
        {
            other.GetComponent<CottonSwabControl>().ActiveOrtoview(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tweezers")
        {
            Tweezers tweezers = other.GetComponent<Tweezers>();
            tweezers.ActiveOrtoViewBt(false);
            //if (tweezers.RadioelementIsNull() == false)//Переделать для режима разпайки
            //{
               
            //}
        }
        else if (other.tag == "Solder")
        {
            Debug.Log(other.name + " Выход");
            other.GetComponent<SolderInteract>().ActiveOrtoBt(false);
        }
        else if (other.tag == "SwabGroup")
        {
            other.GetComponent<CottonSwabControl>().ActiveOrtoview(false);
        }
    }
}
