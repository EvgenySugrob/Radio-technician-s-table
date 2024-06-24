using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CottonSwabControl : MonoBehaviour
{
    public bool isFluxed { get; set; }

    [SerializeField] CottonSwabSpawn cottonSwabSpawn;
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] Fluxing flux;
    [SerializeField] List<GameObject> triggerZoneList;
    [SerializeField] Transform raycastPoint0;
    [SerializeField] Transform raycastPoint1;   
    [SerializeField] GameObject ortoViewBt;
    [SerializeField] Transform target;
    private bool raycastRotationTotarget;

    [Header("raycastSetting")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] float distance = 3;
    [SerializeField] Transform nearSlot;
    [SerializeField] GameObject statusFluxSlot;
    [SerializeField] Image fluxBar;

    [Header("TweezersChekRemove")]
    [SerializeField] List<Tweezers> tweezers;
    

    public void DeleteSwap()
    {
        popupMenuCustom.ClosePopupMenu();
        cottonSwabSpawn.RemoveSpawnSwab();
    }

    public void StartFluxing()
    {
        popupMenuCustom.ClosePopupMenu();
        foreach (GameObject go in triggerZoneList) 
        {
            go.GetComponent<CottonSwabTrigerZone>().DisableStartBt();
            go.SetActive(false);
        }
        flux.StartFlux(cottonSwabSpawn.ReturnCurrenSwab().GetComponent<CottonSwabControl>());
    }

    public void EndFluxing()
    {
        isFluxed = true;
    }

    public void SpawnEnableTriggerZone()
    {
        foreach (GameObject go in triggerZoneList)
        {
            go.SetActive(true);
        }
    }

    public void ActiveOrtoview(bool isActive)
    {
        if (isFluxed)
        {
            ortoViewBt.SetActive(isActive);
        }
        if(isActive == false)
        {
            popupMenuCustom.ClosePopupMenu();
        }
    }

    public void RaycastPointActive(bool isActive)
    {
        raycastRotationTotarget = isActive;
    }
    private void Update()
    {
       if(raycastRotationTotarget)
       {
            CheckDistanceFromRaycastPoint();
       }
        
    }
    private void CheckDistanceFromRaycastPoint()
    {
        Vector3 pointFloor = new Vector3(transform.position.x, target.position.y, transform.position.z);
        float distanceFromFirstPoint = Vector3.Distance(raycastPoint0.position, pointFloor);
        float distanceFromSecondPoint = Vector3.Distance(raycastPoint1.position, pointFloor);
        if (distanceFromFirstPoint < distanceFromSecondPoint)
        {
            RaycastCheck(raycastPoint0);
        }
        else if (distanceFromSecondPoint < distanceFromFirstPoint)
        {
            RaycastCheck(raycastPoint1);
        }
    }
    private void RaycastCheck(Transform pointRaycast)
    {
        if(isFluxed)
        {
            RaycastHit hit;
            if (Physics.Raycast(pointRaycast.position, pointRaycast.TransformDirection(Vector3.forward), out hit, distance, layerMask))
            {
                Debug.DrawRay(pointRaycast.position, pointRaycast.forward, Color.green, distance, true);
                if (hit.collider.GetComponent<SlotInfo>())
                {
                    nearSlot = hit.collider.transform;
                    if (nearSlot.GetComponent<SlotInfo>().IsFluxed() == false)
                    {
                        statusFluxSlot.SetActive(true);
                        fluxBar.fillAmount = nearSlot.GetComponent<SlotInfo>().FluxingProcess();
                    }
                    else
                    {
                        statusFluxSlot.SetActive(false);
                    }
                    
                }
                else if(hit.collider.GetComponent<LegsSolderingProgress>())
                {
                    LegsSolderingProgress elementLeg = hit.collider.GetComponent<LegsSolderingProgress>();

                    if(elementLeg.GetFluxingLeg() == false && elementLeg.GetStatusLegs() == true)
                    {
                        statusFluxSlot.SetActive(true);
                        fluxBar.fillAmount = elementLeg.GetComponent<LegsSolderingProgress>().FluxinglegsElement();
                    }
                    else
                    {
                        statusFluxSlot.SetActive(false);
                    }
                }
                else
                {
                    statusFluxSlot.SetActive(false);
                    Debug.Log("Не слот");
                }
            }
            else
            {
                statusFluxSlot.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Необходимо взять флюс");
        }
        
    }

    public void CheckTweezersReadyToRemoveElement()
    {
        foreach(Tweezers tweezer in tweezers)
        {
            tweezer.TweezersReadyRemoveElement();
        }
    }
}
