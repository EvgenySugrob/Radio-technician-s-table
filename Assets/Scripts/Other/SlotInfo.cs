using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotInfo : MonoBehaviour
{
    public bool isOccupied { set; get; }
    public float distanceToTweezers { set; get; }

    [SerializeField] Transform capasitorTransform;
    [SerializeField] Transform resistTransform;
    private BoxCollider boxCollider;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        resistTransform = transform.GetChild(0).transform;
        capasitorTransform = transform.GetChild(1).transform;
    }
}
