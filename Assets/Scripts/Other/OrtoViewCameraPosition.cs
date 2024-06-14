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
    [SerializeField] List<BoxCollider> slotColliderList;
    [SerializeField] Tweezers tweezers;

    [Header("UI")]
    [SerializeField] GameObject buttonBack;

    private bool isMove;
    private bool isOrto;
    float minDistationBetweenTwoPoints = 0.02f;

    public void StartOrtoView()
    {
        tweezers = dragAndDrop.GetDraggedObject().GetComponent<Tweezers>();
        triggerZonePopupBt.enabled = false;
        foreach (BoxCollider boxCollider in slotColliderList)
        {
            boxCollider.enabled = true;
        }
        tweezers.ActiveOrtoViewBt(false);

        virtualCamera.enabled = false;
        startPosition = player.position;
        startRotation = playerCamera.rotation;

        isMove = true;
    }

    public void ReturnToMainView()
    {
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
            foreach (BoxCollider boxCollider in slotColliderList)
            {
                boxCollider.enabled = false;
            }
            triggerZonePopupBt.enabled = true;
            virtualCamera.enabled = true;
            tweezers = null;
        }
    }
}
