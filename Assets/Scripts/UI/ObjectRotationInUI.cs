using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotationInUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform pivotRotation;
    private Vector3 speedRotation = new Vector3(0,50f,0);
    private Quaternion startRotation;
    private bool isRot;

    private void Start()
    {
        startRotation = pivotRotation.rotation;
    }

    private void Update()
    {
        if (isRot) 
        {
            pivotRotation.Rotate(speedRotation*Time.deltaTime,Space.World);
        }
        else
        {
            pivotRotation.rotation = startRotation;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isRot= true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isRot= false;
    }
}
