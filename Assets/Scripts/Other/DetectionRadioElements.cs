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
            Debug.Log(other.name + " " + tweezers.RadioelementIsNull());
            if (tweezers.RadioelementIsNull() == false)
            {
                Debug.Log(other.name + " " + "Кнопка активна");
                tweezers.ActiveOrtoViewBt(true);
            }
        }
        else if(other.tag == "Solder")
        {
            Debug.Log(other.name + " Вход");
            other.GetComponent<SolderInteract>().ActiveOrtoBt(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tweezers")
        {
            Tweezers tweezers = other.GetComponent<Tweezers>();

            if (tweezers.RadioelementIsNull() == false)
            {
                tweezers.ActiveOrtoViewBt(true);
            }
        }
        else if (other.tag == "Solder")
        {
            other.GetComponent<SolderInteract>().ActiveOrtoBt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tweezers")
        {
            Tweezers tweezers = other.GetComponent<Tweezers>();

            if (tweezers.RadioelementIsNull() == false)
            {
                tweezers.ActiveOrtoViewBt(false);
            }
        }
        else if (other.tag == "Solder")
        {
            Debug.Log(other.name + " Выход");
            other.GetComponent<SolderInteract>().ActiveOrtoBt(false);
        }
    }
}
