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
    [SerializeField] Transform boardOrtoviewPositionSwap;
    [SerializeField] BoxCollider triggerZonePopupBt;
    [SerializeField] Tweezers tweezers;
    [SerializeField] List<GameObject> boardSlots;
    private GameObject objectInHandNow;
    private Vector3 eulerCameraRotation = new Vector3(90,0,0);

    [Header("UI")]
    [SerializeField] GameObject buttonBack;

    [SerializeField] Tweezers bigTweezer;

    public void StartOrtoView()
    {
        player.GetComponent<PlayerController>().enabled = false;
        Debug.Log(player.GetComponent<PlayerController>().enabled);
        objectInHandNow = dragAndDrop.GetDraggedObject();

        if(objectInHandNow.GetComponent<Tweezers>())
        {
            TweezersInHand();
        }
        if(objectInHandNow.GetComponent<SolderInteract>())
        {
            SolderInHand();
        }
        if(objectInHandNow.GetComponent<SideCuttersMain>())
        {
            SiderCuttersInHand();
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
        bigTweezer.TransparentMaterial(true);
        virtualCamera.enabled = false;
        startPosition = player.position;
        startRotation = playerCamera.rotation;

        player.GetComponent<PlayerController>().ActiveOrtoView(true);
        playerCamera.GetComponent<Camera>().orthographic = true;
        dragAndDrop.OrtoViewParam();

        if(Vector3.Distance(player.position,boardOrtoViewPosition.position) < Vector3.Distance(player.position, boardOrtoviewPositionSwap.position))
        {
            player.position = boardOrtoViewPosition.position;
            //playerCamera.rotation = boardOrtoViewPosition.rotation;
            playerCamera.eulerAngles = eulerCameraRotation;
        }
        else
        {
            player.position = boardOrtoviewPositionSwap.position;
            playerCamera.eulerAngles = eulerCameraRotation;
            //playerCamera.rotation = boardOrtoviewPositionSwap.rotation;
        }

        buttonBack.SetActive(true);

        player.GetComponent<PlayerController>().enabled = true;
        Debug.Log(player.GetComponent<PlayerController>().enabled);
        //isMove = true;
    }
    private void SiderCuttersInHand()
    {
        objectInHandNow.GetComponent<SideCuttersMain>().ActiveOrtoviewBt(false);
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
        tweezers.PopupMenuOrtoviewButtons(true);
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
        Debug.Log(player.GetComponent<PlayerController>().enabled);

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
                //objectInHandNow.GetComponent<Tweezers>().TransparentMaterial(false);
                objectInHandNow.GetComponent<Tweezers>().RemoveParent();
                objectInHandNow.GetComponent<Tweezers>().PopupMenuOrtoviewButtons(false);
            }
            playerCamera.GetComponent<Camera>().orthographic = false;
        }
        bigTweezer.TransparentMaterial(false);

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
        Debug.Log(player.GetComponent<PlayerController>().enabled);
    }
}
