using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotInfo : MonoBehaviour
{
    public bool isOccupied { set; get; }

    [SerializeField] Transform capasitorTransform;
    [SerializeField] Transform resistTransform;
    private BoxCollider boxCollider;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        resistTransform = transform.GetChild(0).transform;
        capasitorTransform = transform.GetChild(1).transform;
    }

    public Transform GetRadioElementTypePosition(TypeRadioElement typeRadioElement)
    {
        switch (typeRadioElement)
        {
            case TypeRadioElement.SMDCapacitor:
                return capasitorTransform;

            case TypeRadioElement.SMDResist:
                return resistTransform;
                
            case TypeRadioElement.Capacitor:
                break;

            case TypeRadioElement.FilmResist:
                break;

            case TypeRadioElement.None:
                break;
        }
        return resistTransform;
    }
    public void OccupiedSlot(bool isTrue)
    {
        isOccupied = isTrue;
    }
}
