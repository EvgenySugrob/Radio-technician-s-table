using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweezers : MonoBehaviour
{
    [Header("Drop and Rotation")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;

    [Header("Point element and object")]
    [SerializeField] Transform elementPoint;
    [SerializeField] GameObject radioElement;

    [Header("UI poopUpMenu TakeButton")]
    [SerializeField] GameObject takeButton;
    [SerializeField] GameObject dropButton;
    [SerializeField] TakeDropRadioElement takeDropRadioElement;

    [Header("RotatePart")]
    [SerializeField] Transform firstPart;
    [SerializeField] Transform secondPart;

    [SerializeField] bool isLittleTweezers;


    private Quaternion firstPartStartRotation;
    private Quaternion secondPartStartRotation;

    [SerializeField]private bool isTakeElement;

    private void Start()
    {
        firstPartStartRotation = firstPart.localRotation;
        secondPartStartRotation= secondPart.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RadioElement" && other.GetComponent<PrefabRisistNominalSetting>().GetTypeTweezers() == isLittleTweezers)
        {
            Debug.Log("TW+");
            takeDropRadioElement.TakeDropButtonActivate(isTakeElement, isLittleTweezers);
            //takeButton.gameObject.SetActive(true);
            radioElement = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RadioElement")
        {
            
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RadioElement" && other.GetComponent<PrefabRisistNominalSetting>().GetTypeTweezers() == isLittleTweezers)
        {
            takeDropRadioElement.AllButtonDisable();
            //takeButton.gameObject.SetActive(false);
            //dropButton.gameObject.SetActive(false);
            radioElement = null;
        }
    }

    public void TakeElement()
    {
        radioElement.TryGetComponent<PrefabRisistNominalSetting>(out var prefabSetting);
        firstPart.localRotation = Quaternion.Euler(0,-prefabSetting.GetConectionAngle(),0);
        secondPart.localRotation = Quaternion.Euler(0, prefabSetting.GetConectionAngle(), 0);
        prefabSetting.RigidbodyKinematic(true);
        radioElement.transform.parent = transform;
        radioElement.transform.position = elementPoint.position;
        isTakeElement = true;

        takeDropRadioElement.AllButtonDisable();
        //takeDropRadioElement.TakeDropButtonActivate(isTakeElement, isLittleTweezers);
    }
    public void DropElement()
    {
        radioElement.TryGetComponent<PrefabRisistNominalSetting>(out var prefabSetting);
        firstPart.localRotation = firstPartStartRotation;
        secondPart.localRotation = secondPartStartRotation;
        prefabSetting.RigidbodyKinematic(false);
        radioElement.transform.parent = null;
        isTakeElement= false;

        takeDropRadioElement.AllButtonDisable();
    }

    public bool RadioelementIsNull()
    {
        if (radioElement == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActiveOrtoViewBt(bool isActive)
    {
        takeDropRadioElement.EnableButtonOrtoView(isActive,isLittleTweezers);
    }
}
