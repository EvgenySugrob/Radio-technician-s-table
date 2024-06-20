using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonSwabControl : MonoBehaviour
{
    public bool isFluxed { get; set; }

    [SerializeField] CottonSwabSpawn cottonSwabSpawn;
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] Fluxing flux;
    [SerializeField] List<GameObject> triggerZoneList;
    [SerializeField] List<GameObject> raycastPointList;
    [SerializeField] GameObject ortoViewBt; 

    private bool raycastRotationTotarget;

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
}
