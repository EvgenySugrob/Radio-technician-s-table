using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderSlotsDetect : MonoBehaviour
{
    [Header("Main param")]
    [SerializeField] SolderInteract solderInteract;
    [SerializeField] Transform targetLock;

    [Header("RaycastSettings")]
    [SerializeField] Transform slotDetect;
    [SerializeField] float distance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool detectActivation;

    [Header("PopupMenu button")]
    [SerializeField] GameObject startSolderingBt;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, targetLock.position.y, transform.position.z);
        transform.LookAt(targetPosition);
 
        Debug.DrawRay(transform.position, transform.forward, Color.green,distance, true);

        if (detectActivation)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward,out hit,distance,layerMask))
            {
                if(hit.collider.tag == "LegsSoldering" )
                {
                    solderInteract.SetRadioelement(hit.collider.transform);
                    solderInteract.IsSolderingPointEnable(true);
                }
                else
                {
                    solderInteract.IsSolderingPointEnable(false);
                    solderInteract.SetRadioelement(null);
                }
            }
            else
            {
                solderInteract.IsSolderingPointEnable(false);
            }
        }
    }

    public void DetecActive(bool isActive)
    {
        detectActivation= isActive;
    }
}
