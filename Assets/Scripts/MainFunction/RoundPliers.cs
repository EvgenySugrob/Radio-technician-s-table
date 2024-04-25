using Deform;
using Obi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class RoundPliers : MonoBehaviour
{
    [Header("Main function")]
    [SerializeField] public GameObject currentHitObject;
    [SerializeField] private bool isTruePosition;
    [SerializeField] GameObject modelingActiveButton;

    [Header("DragDrop/Rotation")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;

    private BendDeformer bendDeformer;

    //[SerializeField] float radiusSphere;
    //public float maxDistance;
    //[SerializeField] LayerMask layerMask;

    //private Vector3 origin;
    //private Vector3 direction;
    //public float currentHitDistance;
    //public Vector3 currentPosition;

    //private bool isLeg;

    //private void Update()
    //{
    //    origin = transform.position;
    //    direction = transform.right;

    //    RaycastHit hit;

    //    if (Physics.SphereCast(origin, radiusSphere, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
    //    {
            
    //        currentHitDistance = hit.distance;
    //        currentPosition = hit.point;
    //        if (hit.transform.tag == "Leg")
    //        {
    //            isTruePosition = false;
    //            currentHitObject = hit.transform.gameObject;
    //            Debug.Log(currentHitObject.name);
    //            modelingActiveButton.SetActive(true);
    //            if (currentHitObject.GetComponent<BendDeformer>())
    //            {
    //                isTruePosition = true;
    //            }
    //            else
    //            {
    //                isTruePosition = false;
    //            }
    //        }
    //        else
    //        {
    //            modelingActiveButton.SetActive(true);
    //            isTruePosition = false;
    //        }
            
            
    //    }
    //    else
    //    {
    //        currentHitDistance = maxDistance;
    //        currentHitObject = null;
    //        isTruePosition = false;
    //        modelingActiveButton.SetActive(false);
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Debug.DrawLine(origin, origin + direction * currentHitDistance);
    //    Gizmos.DrawWireSphere(origin + direction * currentHitDistance, radiusSphere);
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<Deformable>() || other.GetComponent<SetTruePositionRoundPliers>())
        {
            modelingActiveButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Deformable>())
        {
            Debug.Log("Exit " + other.name);
            modelingActiveButton.SetActive(false);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<SetTruePositionRoundPliers>())
        {
            isTruePosition = true;
            modelingActiveButton.SetActive(true);
            currentHitObject = other.gameObject;
            Debug.Log("Stay " + other.name);
        }
        else if(other.GetComponent<SetTruePositionRoundPliers>() == false)
        {
            isTruePosition = false;
            currentHitObject= null;
        }
        else
        {
            isTruePosition = false;
        }
    }

    public void StartModelingRadioLegs()
    {
        if (isTruePosition)
        {
            FrezeObjectModelingLegs();
            //Переместить на позицию для формавки ножки элемента через Lerp
            transform.position = currentHitObject.transform.GetChild(0).position;
            transform.rotation = currentHitObject.transform.GetChild(0).rotation;
        }
        else
        {
            Debug.Log("Неверное место формовки");
        }
    }

    private void FrezeObjectModelingLegs()
    {
        transform.TryGetComponent<IDrag>(out var drag);

        if (drag.isFreeze)
        {
            bendDeformer = null;
            dragAndDrop.enabled = true;
            dragAndRotation.enabled = true;
            drag.onFreeze(false);
        }
        else
        {
            bendDeformer = currentHitObject.GetComponent<BendDeformer>();
            drag.onFreeze(true);
            dragAndDrop.enabled=false;
            dragAndRotation.enabled=false;
        }
    }

    public void AngleModelingSet(float value)
    {
        bendDeformer.Angle = 90f * value;
    }
}
