using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotInfo : MonoBehaviour
{
    public bool isOccupied { set; get; }
    public bool isRedyToRemove { get; set; } //убирать после распайки
    [SerializeField] bool isFluxed;

    [SerializeField] GameObject radioelementInSlot;
    [SerializeField] Transform capasitorTransform;
    [SerializeField] Transform resistTransform;
    [SerializeField] Transform defaultCapasitor;
    [SerializeField] Transform defaultResist;

    [SerializeField] Transform boardParent;
    private BoxCollider boxCollider;

    [SerializeField] float fluxindDuration = 1.5f;
    [SerializeField] float fluxingTimer;
    private float amountBarProgress;

    [Header("Adjacent slots")]
    [SerializeField] bool isComponentWithLegs;
    [SerializeField] List<SlotInfo> adjacentSlotsList;



    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        resistTransform = transform.GetChild(0).transform;
        capasitorTransform = transform.GetChild(1).transform;
        boardParent = transform.parent.parent;
    }

    public Transform GetRadioElementTypePosition(TypeRadioElement typeRadioElement, GameObject radioelement)
    {
        Transform radioelementTransform = capasitorTransform;
        radioelementInSlot = radioelement;
        switch (typeRadioElement)
        {
            case TypeRadioElement.SMDCapacitor:
                radioelementTransform = capasitorTransform;
                break;

            case TypeRadioElement.SMDResist:
                radioelementTransform = resistTransform;
                break;
                
            case TypeRadioElement.Capacitor:
                radioelementTransform = defaultCapasitor;
                break;

            case TypeRadioElement.FilmResist:
                radioelementTransform = defaultResist;
                break;
        }
        return radioelementTransform;
    }
    public void OccupiedSlot(bool isTrue)
    {
        isOccupied = isTrue;
    }
    public void RemoveRadioelementInSlot()
    {
        radioelementInSlot = null;
    }
    public GameObject ReturnRadioelementInSlot()
    {
        return radioelementInSlot;
    }


    public Transform ReturnParenRadioelement()
    {
        return boardParent;
    }

    public bool IsFluxed()
    {
        return isFluxed;
    }

    public void SetIsFluxed(bool isActive)
    {
        isFluxed = isActive;
    }
    public float FluxingProcess()
    {
        if(isFluxed==false)
        {
            fluxingTimer += Time.deltaTime;
            amountBarProgress = fluxingTimer / fluxindDuration;
            if (fluxingTimer>=fluxindDuration)
            {
                isFluxed = true;
            }
        }

        return amountBarProgress;
    }
 
}
