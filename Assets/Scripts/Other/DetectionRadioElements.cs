using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadioElements : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

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
        else if(other.tag == "Multimeter")
        {
            other.GetComponent<MultimeterMain>().ActiveOrtoBt(true);
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
        else if(other.tag == "SideCutters")
        {
            other.GetComponent<SideCuttersMain>().ActiveOrtoviewBt(true);
        }
        else if (other.tag == "Multimeter")
        {
            other.GetComponent<MultimeterMain>().ActiveOrtoBt(true);
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
        else if (other.tag == "SideCutters")
        {
            other.GetComponent<SideCuttersMain>().ActiveOrtoviewBt(false);
        }
        else if (other.tag == "Multimeter")
        {
            other.GetComponent<MultimeterMain>().ActiveOrtoBt(false);
        }
    }
}
