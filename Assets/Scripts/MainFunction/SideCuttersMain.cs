using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCuttersMain : MonoBehaviour
{
    [Header("DetectionFakeLegs")]
    [SerializeField] Transform raycastPoint;
    [SerializeField] float distanceRay = 3f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LegPrepareToCut currentLeg;

    [Header("UI")]
    [SerializeField] GameObject ortoviewGoBt;
    [SerializeField] GameObject cutBt;
    [SerializeField] PopupMenuCustom popupMenuCustom;

    [Header("Inputs")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;
    [SerializeField] PlayerController playerController;

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward),Color.red,distanceRay);
        if (Physics.Raycast(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward), out hit, distanceRay, layerMask))
        {
            Debug.Log(hit.collider.name);
            if(hit.collider.GetComponent<LegPrepareToCut>())
            {
                currentLeg = hit.collider.GetComponent<LegPrepareToCut>();

                if (currentLeg.IsRedyToCat() && currentLeg.isCutDone == false)
                {
                    cutBt.SetActive(true);
                }
            }
            else
            {
                cutBt.SetActive(false);
            }
        }
        else
        {
            cutBt.SetActive(false);
        }
    }

    public void CutLeg()
    {
        currentLeg.CutLeg();
        popupMenuCustom.ClosePopupMenu();
    }

    public void ActiveOrtoviewBt(bool isActive)
    {
        ortoviewGoBt.SetActive(isActive);
    }
}
