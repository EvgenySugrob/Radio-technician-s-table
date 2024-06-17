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

    [SerializeField] private bool isTakeElement;

    [Header("BoardSlots")]
    [SerializeField] bool isSlotSet;
    [SerializeField] Transform nearSlot;
    [SerializeField] Transform raycastPoint;
    [SerializeField] LayerMask layerMask;

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
            radioElement = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RadioElement" && other.GetComponent<PrefabRisistNominalSetting>().GetTypeTweezers() == isLittleTweezers)
        {
            takeDropRadioElement.AllButtonDisable();
            radioElement = null;
        }
    }

    private void Update()
    {
        if(isSlotSet == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward), out hit, 10f, layerMask))
            {
                if (hit.collider.GetComponent<SlotInfo>())
                {
                    nearSlot = hit.collider.transform;
                    takeDropRadioElement.EnableButtonSlotsInfo(true, isLittleTweezers);

                }
            }
            else
            {
                takeDropRadioElement.EnableButtonSlotsInfo(false, isLittleTweezers);
                nearSlot = null;
            }
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

    public bool IsLittleTweezers()
    {
        return isLittleTweezers;
    }


    public void SlotSet()
    {
        transform.TryGetComponent<IDrag>(out var drag);
        drag.onFreeze(true);

        isSlotSet = true;
        takeDropRadioElement.EnableButtonSlotsInfo(false, isLittleTweezers);

        SlotInfo slot = nearSlot.GetComponent<SlotInfo>();
        TypeRadioElement type = radioElement.GetComponent<PrefabRisistNominalSetting>().typeRadioElement;

        transform.position = slot.GetRadioElementTypePosition(type).position;
        //transform.parent = slot.GetRadioElementTypePosition(type); подумать??
        slot.OccupiedSlot(true);
        dragAndDrop.ClearHand();
    }
    public void SlotRemove()
    {

    }
}
