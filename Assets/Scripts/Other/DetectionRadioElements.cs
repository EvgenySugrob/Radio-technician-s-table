using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadioElements : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tweezers")
        {
            
            Tweezers tweezers = other.GetComponent<Tweezers>();
            Debug.Log(other.name + " " + tweezers.RadioelementIsNull());
            if (tweezers.RadioelementIsNull() == false)
            {
                Debug.Log(other.name + " " + "������ �������");
                tweezers.ActiveOrtoViewBt(true);
            }
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
    }
}
