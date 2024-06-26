using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrtoViewCameraPosition : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform player;
    [SerializeField] Transform playerCamera;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] float speed = 3f;
    [SerializeField]Vector3 startPosition;
    [SerializeField]Quaternion startRotation;

    [Header("BoardPoint")]
    [SerializeField] Transform boardOrtoViewPosition;
    [SerializeField] BoxCollider triggerZonePopupBt;
    [SerializeField] Tweezers tweezers;
    [SerializeField] List<GameObject> boardSlots;
    private GameObject objectInHandNow;

    [Header("UI")]
    [SerializeField] GameObject buttonBack;

    public void StartOrtoView()
    {
        player.GetComponent<PlayerController>().enabled =false;
        objectInHandNow = dragAndDrop.GetDraggedObject();

        if(objectInHandNow.GetComponent<Tweezers>())
        {
            TweezersInHand();
        }
        if(objectInHandNow.GetComponent<SolderInteract>())
        {
            SolderInHand();
        }

        triggerZonePopupBt.enabled = false;
        foreach (GameObject slotsGroup in boardSlots)
        {
            slotsGroup.SetActive(true);
        }
        if (objectInHandNow.GetComponent<CottonSwabControl>())
        {
            SwabInHand();
        }

        virtualCamera.enabled = false;
        startPosition = player.position;
        startRotation = playerCamera.rotation;

        player.GetComponent<PlayerController>().ActiveOrtoView(true);
        playerCamera.GetComponent<Camera>().orthographic = true;
        dragAndDrop.OrtoViewParam();

        player.position = boardOrtoViewPosition.position;
        playerCamera.rotation = boardOrtoViewPosition.rotation;

        buttonBack.SetActive(true);

        player.GetComponent<PlayerController>().enabled = true;
        //isMove = true;
    }
    private void SolderInHand()
    {
        objectInHandNow.GetComponent<SolderInteract>().ActiveOrtoBt(false);
        objectInHandNow.GetComponent<SolderInteract>().StartSolderingDetectionSlot(true);
    }
    private void TweezersInHand()
    {
        tweezers = dragAndDrop.GetDraggedObject().GetComponent<Tweezers>();
        tweezers.ActiveOrtoViewBt(false);
        tweezers.TransparentMaterial(true);
    }
    private void SwabInHand()
    {
        CottonSwabControl cottonSwab = objectInHandNow.GetComponent<CottonSwabControl>();
        cottonSwab.ActiveOrtoview(false);
        cottonSwab.RaycastPointActive(true);
        cottonSwab.CheckTweezersReadyToRemoveElement();
    }
    private void DisableSwabInHand()
    {
        objectInHandNow.GetComponent<CottonSwabControl>().RaycastPointActive(false);
    }
    private void DisableSolderDetection()
    {
        objectInHandNow.GetComponent<SolderInteract>().StartSolderingDetectionSlot(false);
    }

    public void ReturnToMainView()
    {
        dragAndDrop.RecoveryCurrentDistance();
        player.GetComponent<PlayerController>().enabled = false;

        if (objectInHandNow != null)
        {
            if (objectInHandNow.GetComponent<SolderInteract>())
            {
                DisableSolderDetection();
            }
            else if (objectInHandNow.GetComponent<CottonSwabControl>())
            {
                DisableSwabInHand();
            }
            else if(objectInHandNow.GetComponent<Tweezers>())
            {
                objectInHandNow.GetComponent<Tweezers>().TransparentMaterial(false);
            }
            playerCamera.GetComponent<Camera>().orthographic = false;
        }
        buttonBack.SetActive(false);

        player.GetComponent<PlayerController>().ActiveOrtoView(false);
        foreach (GameObject slotsGroup in boardSlots)
        {
            slotsGroup.SetActive(false);
        }
        triggerZonePopupBt.enabled = true;
        virtualCamera.enabled = true;
        tweezers = null;
        objectInHandNow = null;

        player.position = startPosition;
        playerCamera.rotation = startRotation;
        player.GetComponent<PlayerController>().enabled = true;
    }
}
