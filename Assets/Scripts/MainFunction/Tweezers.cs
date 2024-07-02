using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] bool isOrtoviewActive;

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
    [SerializeField] List<GameObject> slotsGroup;

    [SerializeField] private float offsetTweezerAfterRelease = 0.05f;

    [Header("CottonSwab")]
    [SerializeField] CottonSwabSpawn cottonSwabSpawn;
    [SerializeField] CottonSwabControl cottonSwabControl;
    [SerializeField]private Transform hideSlot;

    [Header("OrtoViewMeshOff")]
    [SerializeField] List<MeshRenderer> allPartTweezersRenderer;
    [SerializeField] Transform board;
    [SerializeField] RotationHolderPivot rotationHolderPivot;

    [SerializeField] QuickOutlineControllerRadioelement outlineControllerRadioelement;
    [SerializeField] bool outlineDetect;

    [Header("LogMessage")]
    [SerializeField] LogMessageSpawn logMessageSpawn;

    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
        firstPartStartRotation = firstPart.localRotation;
        secondPartStartRotation= secondPart.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RadioElement" && other.GetComponent<PrefabRisistNominalSetting>().GetTypeTweezers() == isLittleTweezers && isOrtoviewActive == false 
            && other.GetComponent<PrefabRisistNominalSetting>().ReturnIsSolderOnSlot() == false)
        {
            Debug.Log("TW+");
            takeDropRadioElement.TakeDropButtonActivate(isTakeElement, isLittleTweezers);
            radioElement = other.gameObject;

            if (radioElement.GetComponent<QuickOutlineControllerRadioelement>() && outlineDetect ==false)
            {
                outlineControllerRadioelement = radioElement.GetComponent<QuickOutlineControllerRadioelement>();
                outlineControllerRadioelement.EnableOutline();
                outlineDetect = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RadioElement" && other.GetComponent<PrefabRisistNominalSetting>().GetTypeTweezers() == isLittleTweezers && isOrtoviewActive == false && other.GetComponent<PrefabRisistNominalSetting>().ReturnIsSolderOnSlot() == false)
        {
            takeDropRadioElement.TakeDropButtonActivate(isTakeElement, isLittleTweezers);
            radioElement = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RadioElement" && other.GetComponent<PrefabRisistNominalSetting>().GetTypeTweezers() == isLittleTweezers && isOrtoviewActive == false && other.GetComponent<PrefabRisistNominalSetting>().ReturnIsSolderOnSlot() == false)
        {
            if (other.GetComponent<QuickOutlineControllerRadioelement>() && outlineDetect)
            {
                outlineControllerRadioelement.DisableOutline();
                outlineDetect= false;
            }
              
            takeDropRadioElement.AllButtonDisable();
            radioElement = null;
        }
    }
    public void PopupMenuOrtoviewButtons(bool isOrtoview)
    {
        isOrtoviewActive= isOrtoview;
        takeDropRadioElement.AllButtonDisable();
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
                    if(radioElement!=null && isTakeElement)
                    {
                        takeDropRadioElement.EnableButtonSlotsInfo(true, isLittleTweezers);
                    }
                    else if(isTakeElement ==false && nearSlot.GetComponent<SlotInfo>().isOccupied)
                    {
                        takeDropRadioElement.EnableButtonSlotRemove(true, isLittleTweezers);
                    }
                }
            }
            else
            {
                takeDropRadioElement.EnableButtonSlotsInfo(false, isLittleTweezers);
                takeDropRadioElement.EnableButtonSlotRemove(false, isLittleTweezers);
                
                nearSlot = null;
            }
        }
    }

    public void TakeElementRemove()
    {
        radioElement.transform.parent = transform;
        radioElement.transform.position = elementPoint.position;
        radioElement.TryGetComponent<PrefabRisistNominalSetting>(out var prefabNominal);
        prefabNominal.RigidbodyKinematic(true);
        prefabNominal.SetTweezers(transform);
        isTakeElement = true;
        takeDropRadioElement.AllButtonDisable();
        hideSlot.GetComponent<SlotInfo>().isOccupied= false;
        hideSlot.GetComponent<SlotInfo>().OccupiedSlot(false);
        hideSlot.GetComponent<SlotInfo>().isRedyToRemove= false;
        hideSlot.GetComponent<SlotInfo>().SetIsFluxed(false);
    }
    public void TakeElement()
    {
        radioElement.TryGetComponent<PrefabRisistNominalSetting>(out var prefabSetting);
        firstPart.localRotation = Quaternion.Euler(0,-prefabSetting.GetConectionAngle(),0);
        secondPart.localRotation = Quaternion.Euler(0, prefabSetting.GetConectionAngle(), 0);
        prefabSetting.RigidbodyKinematic(true);
        prefabSetting.SetTweezers(transform);
        radioElement.transform.parent = transform;
        radioElement.transform.position = elementPoint.position;
        isTakeElement = true;

        takeDropRadioElement.AllButtonDisable();

        outlineControllerRadioelement.DisableOutline();
        outlineControllerRadioelement.GlobalActivateDiactivate(false);
    }
    public void DropElement()
    {
        radioElement.TryGetComponent<PrefabRisistNominalSetting>(out var prefabSetting);
        firstPart.localRotation = firstPartStartRotation;
        secondPart.localRotation = secondPartStartRotation;
        prefabSetting.RigidbodyKinematic(false);
        prefabSetting.ResetTweeezers();
        radioElement.transform.parent = null;
        isTakeElement= false;

        takeDropRadioElement.AllButtonDisable();

        outlineControllerRadioelement.EnableOutline();
        outlineControllerRadioelement.GlobalActivateDiactivate(true);
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
    public void TransparentMaterial(bool isActive)
    {
        if (isActive)
        {
            foreach (MeshRenderer meshRenderer in allPartTweezersRenderer)
            {
                meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g,
                    meshRenderer.material.color.b, 0);
            }
        }
        else
        {
            foreach (MeshRenderer meshRenderer in allPartTweezersRenderer)
            {
                meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g,
                    meshRenderer.material.color.b, 1);
            }
        }
    }
    public void RemoveParent()
    {
        //transform.parent = null;
    }

    public bool IsLittleTweezers()
    {
        return isLittleTweezers;
    }
    private void CorrectSetSlot()
    {
        transform.TryGetComponent<IDrag>(out var drag);
        drag.onFreeze(true);

        isSlotSet = true;
        takeDropRadioElement.EnableButtonSlotsInfo(false, isLittleTweezers);
        radioElement.GetComponent<PrefabRisistNominalSetting>().SetSlot(nearSlot);
        SlotInfo slot = nearSlot.GetComponent<SlotInfo>();
        TypeRadioElement type = radioElement.GetComponent<PrefabRisistNominalSetting>().typeRadioElement;

        transform.position = slot.GetRadioElementTypePosition(type, radioElement).position;
        slot.OccupiedSlot(true);
        transform.parent = board;
        dragAndDrop.ClearHand();
    }
    private void SetSlotComponentSMD()
    {
        if(nearSlot.GetComponent<SlotInfo>().TrueContextSlotName(radioElement))
        {
            if (nearSlot.GetComponent<SlotInfo>().IsFluxed())
            {
                CorrectSetSlot();
            }
            else
            {
                logMessageSpawn.GetTextMessageInLog(false, "���������� ������� ����.");
                Debug.Log("���������� ������� ����");
            }
        }
        else
        {
            logMessageSpawn.GetTextMessageInLog(false, "�������� �������� ���������� �����");
        }
    }
    private void SetSlotComponentWithLegs()
    {
        TypeRadioElement type = radioElement.GetComponent<PrefabRisistNominalSetting>().typeRadioElement;

        if (nearSlot.GetComponent<SlotInfo>().IsPossibleInstalElement(type,radioElement))
        {
            if (nearSlot.GetComponent<SlotInfo>().IsFluxedElementsWithLegs(type,radioElement))
            {
                CorrectSetSlot();
            }
            else
            {
                logMessageSpawn.GetTextMessageInLog(false, "���������� ������� ����.");
                Debug.Log("���������� ������� ����");
            }
        }
        else
        {
            logMessageSpawn.GetTextMessageInLog(false, "���������� ���������� ���������.");
            Debug.Log("���������� ���������� ���������");
        }
    }
    public void SlotSet()
    {
        if(nearSlot.GetComponent<SlotInfo>().isOccupied == false)
        {
            if (nearSlot.GetComponent<SlotInfo>().IsComponentWithLegs())
            {
                if (radioElement.GetComponent<PrefabRisistNominalSetting>().IsLegsBendComplite())
                {
                    Debug.Log("���� ��� ������� � �������");
                    SetSlotComponentWithLegs();
                }
                else
                {
                    logMessageSpawn.GetTextMessageInLog(false, "���������� ��������� �������� �������.");
                    Debug.Log("���������� ��������� �������� �������");
                }

            }
            else
            {
                Debug.Log("���� ��� ������� ��� �����");
                SetSlotComponentSMD();
            }
        }
        else
        {
            logMessageSpawn.GetTextMessageInLog(false, "���������� ����� ������.");
            Debug.Log("���������� ����� ������.");
        }
        
        
    }
    public void SlotRemove()
    {
        transform.parent = null;
        SlotInfo slot = nearSlot.GetComponent<SlotInfo>();
        hideSlot = nearSlot;

        GameObject radioElementInSlot = slot.ReturnRadioelementInSlot();
        PrefabRisistNominalSetting nominalSetting = radioElementInSlot.GetComponent<PrefabRisistNominalSetting>();
        TypeRadioElement type = nominalSetting.typeRadioElement;
        Transform connectPosition = slot.GetRadioElementTypePosition(type, radioElementInSlot);
        transform.position = connectPosition.position;
        slot.isRedyToRemove= true;
        nominalSetting.SetTweezers(transform);
        nominalSetting.SetTweezersGrab(true);

        firstPart.localRotation = Quaternion.Euler(0, -nominalSetting.GetConectionAngle(), 0);
        secondPart.localRotation = Quaternion.Euler(0, nominalSetting.GetConectionAngle(), 0);

        popupMenuCustom.ExtrimeFreezeObj(transform.gameObject);
        dragAndDrop.ClearHand();

        takeDropRadioElement.AllButtonDisable();
    }

    public void ReleaseSolderedElement()
    {
        transform.parent = null;
        radioElement.TryGetComponent<PrefabRisistNominalSetting>(out var prefabSetting);
        firstPart.localRotation = firstPartStartRotation;
        secondPart.localRotation = secondPartStartRotation;

        
        radioElement.transform.parent = null;
        isTakeElement = false;
        isSlotSet = false;

        transform.TryGetComponent<IDrag>(out var drag);
        drag.onFreeze(false);
        TweezersFreezeWithoutPopupMenu();

        TweezersSetOffsetFreeze();
        prefabSetting.ResetTweeezers();
        takeDropRadioElement.AllButtonDisable();
    }

    public void TweezersReadyRemoveElement()
    {
        if(hideSlot != null && hideSlot.GetComponent<SlotInfo>().isRedyToRemove)
        {
            Debug.Log("TweezersReadyRemoveElement()");
            foreach (GameObject item in slotsGroup)
            {
                item.SetActive(false);
            }
        }
    }

    public void TweezersFreezeWithoutPopupMenu()
    {
        popupMenuCustom.ExtrimeFreezeObj(transform.gameObject);
    }
    public void TweezersSetOffsetFreeze()
    {
        if (rotationHolderPivot.IsChangeRotation())
        {
            transform.position= new Vector3(transform.position.x, transform.position.y + offsetTweezerAfterRelease,
            transform.position.z);
            transform.rotation = startRotation;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + offsetTweezerAfterRelease,
            transform.position.z);
        }
    }
}
