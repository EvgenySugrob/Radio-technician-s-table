using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFromStation : MonoBehaviour
{
    [Header("MoveScript")]
    [SerializeField] Transform startStationPoint;
    [SerializeField] float speedMove;
    [SerializeField] float speedRotation;
    
    [Header("PlayerComponent")]
    [SerializeField] Transform player;
    [SerializeField] DragAndDrop dragAndDrop;
    private Transform playerCamera;
    private CinemachineBrain brainCamera;

    [Header("UI Component")]
    [SerializeField] GameObject backToMainViewBt;

    private CharacterController characterController;
    private PlayerController playerController;
    private Vector3 positionBeforeMoving;
    private Quaternion rotationBeforeMoving;

    private float minDistance = 0.005f;
    private bool stationDetect;

    [SerializeField] bool isStationPosition;
    [SerializeField] bool isMove;

    private void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        playerController = player.GetComponent<PlayerController>();
        playerCamera = player.GetChild(0).transform;
        brainCamera = player.GetChild(0).GetComponent<CinemachineBrain>();
    }
    private void Update()
    {
        if(isStationPosition == false && isMove)
        {
            MoveToStation();
        }
        else if(isStationPosition && isMove)
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
            isMove = false;
            isStationPosition = false;
            EnableMainFunctionPlayer();
        }
    }

    private void EnableMainFunctionPlayer()
    {
        
        dragAndDrop.enabled = true;
        characterController.enabled = true;
        playerController.enabled = true;
        brainCamera.enabled = true;
    }

    public void PlayerToStation()
    {
        positionBeforeMoving = player.position;
        rotationBeforeMoving = playerCamera.rotation;
        dragAndDrop.enabled = false;
        characterController.enabled = false;
        playerController.enabled = false;
        brainCamera.enabled = false;

        isMove = true;
    }

    public void PlayerFromStation()
    {
        isMove = true;
        backToMainViewBt.SetActive(false);
    }
}
