using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlotInfo : MonoBehaviour
{
    public bool isOccupied { set; get; }
    public bool isRedyToRemove { get; set; } //������� ����� ��������
    [SerializeField] bool isFluxed;

    [SerializeField] GameObject radioelementInSlot;
    [SerializeField] Transform capasitorTransform;
    [SerializeField] Transform resistTransform;
    [SerializeField] Transform defaultCapasitor;
    //[SerializeField] Transform defaultResist;//������

    [Header("ResistDifferentSize")]
    [SerializeField] Transform resist12WPosition;
    [SerializeField] Transform resist14WPosition;
    [SerializeField] Transform resist18WPosition;

    [Header("FluxingSlots")]
    [SerializeField] Transform boardParent;
    private BoxCollider boxCollider;

    [SerializeField] float fluxindDuration = 1.5f;
    [SerializeField] float fluxingTimer;
    private float amountBarProgress;

    [Header("Adjacent slots")]
    [SerializeField] bool isComponentWithLegs;
    [SerializeField] List<SlotInfo> adjacentSlotsList;
    private TypeRadioElement typeElement;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        resistTransform = transform.GetChild(0).transform;
        capasitorTransform = transform.GetChild(1).transform;
        boardParent = transform.parent.parent;
    }

    public Transform GetRadioElementTypePosition(TypeRadioElement typeRadioElement, GameObject radioelement)
    {
        typeElement = typeRadioElement;
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
                radioelementTransform = GetCorrectPositionFilmResist(radioelementInSlot.GetComponent<PrefabRisistNominalSetting>().
                    ReturnContextFilmResistName()); 
                break;
        }
        return radioelementTransform;
    }
    public void OccupiedSlot(bool isTrue)
    {
        isOccupied = isTrue;

        if (adjacentSlotsList.Count>0 && isComponentWithLegs)
        {
            switch (typeElement)
            {
                case TypeRadioElement.Capacitor:
                    CapasitorSlotsIsOccupied(isTrue);
                    break;
                case TypeRadioElement.FilmResist:
                    FilmResistSlotsIsOccupied(isTrue, radioelementInSlot.GetComponent<PrefabRisistNominalSetting>().
                    ReturnContextFilmResistName());
                    break;
            }
        }
    }
    public void SetRadioelement(GameObject element)
    {
        radioelementInSlot = element;
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
    public bool IsComponentWithLegs()
    {
        return isComponentWithLegs;
    }


    public bool IsPossibleInstalElement(TypeRadioElement type, GameObject radioelement)
    {
        bool isPossible = false;
        switch (type)
        {
            case TypeRadioElement.Capacitor:
                isPossible = CheckAddjacentCapasitorSlotIsOccupied();
                break;

            case TypeRadioElement.FilmResist:
                isPossible = CheckAddjacentResistSlotIsOccupied(radioelement.GetComponent<PrefabRisistNominalSetting>().
                    ReturnContextFilmResistName());
                break;

        }

        return isPossible;
    }

    public bool IsFluxedElementsWithLegs(TypeRadioElement type, GameObject radioelement)
    {
        bool isFlux = false;
        switch (type)
        {
            case TypeRadioElement.Capacitor:
                isFlux = isFluxed;
                break;

            case TypeRadioElement.FilmResist:
                isFlux = CheckAddjacentResistIsFlux(radioelement.GetComponent<PrefabRisistNominalSetting>().
                    ReturnContextFilmResistName());
                break;
        }

        return isFlux;
    }
    private bool CheckAddjacentResistIsFlux(string contextNameResist)
    {
        bool isCheck=false;

        if (isFluxed)
        {
            isCheck = true;
            if (adjacentSlotsList.Last().isFluxed)
            {
                isCheck = true;
            }
        }
        else
        {
            isCheck = false;
            
        }
        return isCheck;
    }
    private bool CheckAddjacentCapasitorSlotIsOccupied()
    {
        bool isCheck=false;

        if (isOccupied)
        {
            isCheck = false;
        }
        else
        {
            isCheck= true;
            foreach (SlotInfo slot in adjacentSlotsList)
            {
                if (slot.isOccupied) 
                {
                    isCheck = false;
                    break;
                }
            }
        }
        return isCheck;
    }
    private bool CheckAddjacentResistSlotIsOccupied(string contextNameResist)
    {
        bool isCheck = false;
        switch (contextNameResist)
        {
            case "1_2W":
                if(isOccupied)
                {
                    isCheck = false;
                }
                else
                {
                    isCheck= true;
                    foreach (SlotInfo slot in adjacentSlotsList)
                    {
                        if (slot.isOccupied)
                        {
                            isCheck = false;
                            break;
                        }
                    }
                }
                break;

            case "1_4W":
                if (isOccupied)
                {
                    isCheck = false;
                }
                else
                {
                    isCheck = true;
                    if(adjacentSlotsList.Last().isOccupied)
                    {
                        isCheck = false;
                    }
                }
                break;

            case "1_8W":
                if (isOccupied)
                {
                    isCheck = false;
                }
                else
                {
                    isCheck = true;
                    if (adjacentSlotsList.Last().isOccupied)
                    {
                        isCheck = false;
                    }
                }
                break;
        }
        return isCheck;
    }



    private Transform GetCorrectPositionFilmResist(string correctName)
    {
        Transform corretcPosition = null;

        switch (correctName)
        {
            case "1_2W":
                corretcPosition = resist12WPosition;
                break;

            case "1_4W":
                corretcPosition = resist14WPosition;
                break;

            case "1_8W":
                corretcPosition = resist18WPosition;
                break;
        }

        return corretcPosition;
    }

    private void CapasitorSlotsIsOccupied(bool isLock)
    {
        foreach (SlotInfo slot in adjacentSlotsList)
        {
            slot.isOccupied = isLock;
            slot.SetRadioelement(radioelementInSlot);
        }
    }

    private void FilmResistSlotsIsOccupied(bool isLock, string contextName)
    {
        switch (contextName)
        {
            case "1_2W":
                foreach (SlotInfo slot in adjacentSlotsList)
                {
                    slot.isOccupied = isLock;
                    slot.SetRadioelement(radioelementInSlot);
                }
                break;

            case "1_4W":
                adjacentSlotsList.Last().isOccupied = isLock;
                adjacentSlotsList.Last().SetRadioelement(radioelementInSlot);
                break;

            case "1_8W":
                adjacentSlotsList.Last().isOccupied = isLock;
                adjacentSlotsList.Last().SetRadioelement(radioelementInSlot);
                break;
        }
    }
}
