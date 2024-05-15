using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.UI.Menu;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    //[SerializeField] private float jumpHeight = 1.0f;
    //[SerializeField] private float gravityValue = -9.81f;
    [SerializeField] CinemachineInputProvider inputProvider;
    [SerializeField] private PopupMenuCustom popupMenuCustom;
    [SerializeField] List<PopupMenuCustom> popupMenuCustomList;
    [SerializeField] CheckOpenUIComponent checkOpenUIComponent;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;



    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if(NonOpenPopupMenu() && checkOpenUIComponent.NonActiveUIComponent())
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 movement = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (inputManager.GetCameraRotationState())
                inputProvider.enabled = true;
            else
                inputProvider.enabled = false;
        }
        else
        {
            inputProvider.enabled = false;
        }
    }
    private bool NonOpenPopupMenu()
    {
        bool isClose = true;
        foreach (PopupMenuCustom popupItem in popupMenuCustomList)
        {
            if (popupItem.GetStatusPopupMenu())
            {
                isClose = false;
                break;
            }
        }
        return isClose;
    }
}
