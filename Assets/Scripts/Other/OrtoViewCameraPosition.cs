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
    Vector3 startPosition;
    Quaternion startRotation;

    [Header("BoardPoint")]
    [SerializeField] Transform boardOrtoViewPosition;
    [SerializeField] BoxCollider triggerZonePopupBt;
    [SerializeField] Tweezers tweezers;
    [SerializeField] List<GameObject> boardSlots;
    private GameObject objectInHandNow;

    [Header("UI")]
    [SerializeField] GameObject buttonBack;

    private bool isMove;
    private bool isOrto;
    float minDistationBetweenTwoPoints = 0.02f;

    public void StartOrtoView()
    {
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
       

        virtualCamera.enabled = false;
        startPosition = player.position;
        startRotation = playerCamera.rotation;

        isMove = true;
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
    }
    private void DisableSolderDetection()
    {
        objectInHandNow.GetComponent<SolderInteract>().StartSolderingDetectionSlot(false);
    }
    public void ReturnToMainView()
    {
        if(objectInHandNow.GetComponent<SolderInteract>())
        {
            DisableSolderDetection();
        }
        playerCamera.GetComponent<Camera>().orthographic = false;
        buttonBack.SetActive(false);
        isMove = true;
    }

    private void Update()
    {
        if(isOrto && isMove)
        {
            MoveFromPoint();
        }
        else if(isOrto == false && isMove)
        {
            MoveToPoint();
        }
    }

    private void MoveToPoint()
    {
        if(Vector3.Distance(player.position,boardOrtoViewPosition.position)>minDistationBetweenTwoPoints)
        {
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation,boardOrtoViewPosition.rotation, speed);
            player.position = Vector3.Lerp(player.position, boardOrtoViewPosition.position, speed);
        }
        else
        {
            isMove = false;
            isOrto = true;

            player.GetComponent<PlayerController>().ActiveOrtoView(true);
            playerCamera.GetComponent<Camera>().orthographic = true;
            dragAndDrop.OrtoViewParam();

            buttonBack.SetActive(true);
        }
    }

    private void MoveFromPoint()
    {
        if (Vector3.Distance(player.position, startPosition) > minDistationBetweenTwoPoints)
        {
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, startRotation, speed);
            player.position = Vector3.Lerp(player.position, startPosition, speed);
        }
        else
        {
            isMove = false;
            isOrto = false;

            player.GetComponent<PlayerController>().ActiveOrtoView(false);
            foreach (GameObject slotsGroup in boardSlots)
            {
                slotsGroup.SetActive(false);
            }
            triggerZonePopupBt.enabled = true;
            virtualCamera.enabled = true;
            tweezers = null;
            objectInHandNow = null;
        }
    }
}
