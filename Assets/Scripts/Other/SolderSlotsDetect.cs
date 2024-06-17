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
        Vector3 targetPoint = targetLock.position;
        targetPoint.z = transform.position.z;
        transform.LookAt(targetPoint);

        if(detectActivation)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward,out hit,distance,layerMask))
            {
                if(hit.collider.GetComponent<SlotInfo>())
                {
                    solderInteract.SetRadioelement(hit.collider.transform);
                    startSolderingBt.SetActive(true);
                }
            }
            else
            {
                startSolderingBt.SetActive(false);
                solderInteract.SetRadioelement(null);
            }
        }
    }
}
