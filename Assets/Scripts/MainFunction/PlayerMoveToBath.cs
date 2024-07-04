using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveToBath : MonoBehaviour
{
    [Header("MoveScript")]
    [SerializeField] Transform startStationPoint;
    [SerializeField] float speedMove;
    [SerializeField] float speedRotation;
    [SerializeField] BathDetection bathDetect;
    [SerializeField] List<GameObject> hideObject;
    [SerializeField] BoxCollider bathCollider;

    [Header("PlayerComponent")]
    [SerializeField] Transform player;
    [SerializeField] DragAndDrop dragAndDrop;
    private Transform playerCamera;
    private CinemachineBrain brainCamera;

    [Header("UI Component")]
    [SerializeField] GameObject backToMainViewBt;

    [Header("Bath Interact")]
    [SerializeField] GameObject switchOnOff;
    [SerializeField] List<GameObject> buttonsControl;
    [SerializeField] private List<GameObject> enabledButtonsControl;

    private BoxCollider switchOnOffCollider;


    [Header("Status")]
    [SerializeField] bool isStationPosition;
    [SerializeField] bool isMove;

    private CharacterController characterController;
    private PlayerController playerController;
    private Vector3 positionBeforeMoving;
    private Quaternion rotationBeforeMoving;

    private float minDistance = 0.005f;
    private bool stationDetect;



    private void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        playerController = player.GetComponent<PlayerController>();
        playerCamera = player.GetChild(0).transform;
        brainCamera = player.GetChild(0).GetComponent<CinemachineBrain>();

        switchOnOffCollider = switchOnOff.GetComponent<BoxCollider>();

    }
    private void Update()
    {
        if (isStationPosition == false && isMove)
        {
            MoveToStation();
        }
        else if (isStationPosition && isMove)
        {
            MoveFromStation();
        }


    }
    private void MoveToStation()
    {
        if (Vector3.Distance(player.position, startStationPoint.position) > minDistance)
        {
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, startStationPoint.rotation, speedRotation * Time.deltaTime);
            player.position = Vector3.Lerp(player.position, startStationPoint.position, speedMove * Time.deltaTime);
        }
        else
        {
            isMove = false;
            isStationPosition = true;
            backToMainViewBt.SetActive(true);
            switchOnOffCollider.enabled = true;

            if(enabledButtonsControl.Count!=0)
            {
                foreach (GameObject item in enabledButtonsControl)
                {
                    item.SetActive(true);
                    enabledButtonsControl.Remove(item);
                }
            }

        }
    }
    private void MoveFromStation()
    {
        if (Vector3.Distance(player.position, positionBeforeMoving) > minDistance)
        {
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, rotationBeforeMoving, speedMove * Time.deltaTime);
            player.position = Vector3.Lerp(player.position, positionBeforeMoving, speedMove * Time.deltaTime);
        }
        else
        {
            //switchOnOffCollider.enabled = true;

            isMove = false;
            bathCollider.enabled = true;
            isStationPosition = false;
            bathDetect.detect = false;
            EnableMainFunctionPlayer();
        }
    }

    private void EnableMainFunctionPlayer()
    {
        //dragAndDrop.enabled = true;

        characterController.enabled = true;
        playerController.enabled = true;
        brainCamera.enabled = true;
    }

    public void PlayerToStation()
    {
        foreach(GameObject item in hideObject)
        {
            item.SetActive(false);
        }

        bathDetect.detect = true;
        bathCollider.enabled = false;
        positionBeforeMoving = player.position;
        rotationBeforeMoving = playerCamera.rotation;
        //dragAndDrop.enabled = false;
        characterController.enabled = false;
        playerController.enabled = false;
        brainCamera.enabled = false;

        isMove = true;
    }

    public void PlayerFromStation()
    {
        foreach(GameObject item in buttonsControl)
        {
            if(item.activeSelf)
            {
                enabledButtonsControl.Add(item);
                item.SetActive(false);
            }
        }
        foreach (GameObject item in hideObject)
        {
            item.SetActive(true);
        }

        isMove = true;
        switchOnOffCollider.enabled = false;
        backToMainViewBt.SetActive(false);
    }
}
