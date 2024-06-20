using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputAction mouseInput;
    [SerializeField] private InputAction mouseRightInput;

    [Header("Setting drag")]
    [SerializeField] private float mouseDragSpeed = 1f;
    [SerializeField] private float mouseDragPhysicsSpeed = 1f;
    [SerializeField] private float currentDistanceToObject;
    [SerializeField] private float stepsDistance = 0.2f;
    [SerializeField] private float minDist = 0.2f;
    [SerializeField] private float maxDist = 2f;
    [SerializeField] LayerMask layerMask;

    [Header("Rotation")]
    [SerializeField] private DragAndRotation dragAndRotation;

    [Header("Popup Menu and UI")]
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] List<PopupMenuCustom> popupMenuCustomList;
    [SerializeField] CheckOpenUIComponent checkOpenUIComponent;

    private float distanceToObject = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;
    [SerializeField] private bool isDrag;
    [SerializeField] private bool isHoldMouse;
    [SerializeField] private bool isRotation;
    [SerializeField] private GameObject draggedObject;

    [Header("Solder")]
    [SerializeField] SolderStationDetect solderStationDetect;
    [SerializeField] private bool isHoldingSolder;
    [SerializeField] float ortoViewDistance = 1f;


    [Header("Regulator rotation")]
    [SerializeField] Transform regulator;
    TemperatureRegulatorSetting settingRegulator;
    [SerializeField] private bool regulatorCheck;
    [SerializeField] private int curretntIndexTemerature = 0;
    private int stepIndexTemperature = 1;

    [SerializeField] PlayerController playerController;

    private void Awake()
    {
        mainCamera = Camera.main;
        
        currentDistanceToObject = distanceToObject;
    }

    private void OnEnable()
    {
        mouseInput.Enable();
        mouseRightInput.Enable();

       
        mouseRightInput.performed += MouseRightInput;
        mouseInput.performed += MousePressed;
    }


    private void OnDisable()
    {
        mouseRightInput.performed -= MouseRightInput;
        mouseInput.performed-= MousePressed;

        mouseRightInput.Disable();
        mouseInput.Disable();
    }


    private void MouseRightInput(InputAction.CallbackContext obj)
    {
        if (draggedObject != null && checkOpenUIComponent.NonActiveUIComponent() && draggedObject.GetComponent<NoPopupMenu>() == null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,100f,layerMask))
            {
                if (hit.collider.GetComponent<IDrag>() != null && draggedObject == hit.collider.gameObject)
                {
                    draggedObject.TryGetComponent<Rigidbody>(out var rb);
                    rb.velocity = Vector3.zero;
                    draggedObject.TryGetComponent<PopupMenuObjectType>(out var popupMenu);
                    popupMenu.Show();
                }
            }
        }
    }

    private void MousePressed(InputAction.CallbackContext obj)
    {
        if (NonOpenPopupMenu() && checkOpenUIComponent.NonActiveUIComponent() && isHoldMouse == false)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                
                if (hit.collider.GetComponent<IDrag>() != null && draggedObject == null && solderStationDetect.detect == false)
                {
                    draggedObject = hit.collider.gameObject;
                    if (draggedObject.GetComponent<SolderInteract>() != null)
                    {
                        isHoldMouse = true;
                        draggedObject.GetComponent<SolderInteract>().EnableSolderDetectionZone();
                    }
                    
                    dragAndRotation.SetObjectRotation(draggedObject);
                    isDrag = true;
                    hit.collider.GetComponent<IDrag>().isMovebale = isDrag;

                }
                else if (draggedObject != null)
                {
                    draggedObject.TryGetComponent<IDrag>(out var iDragComponent);
                    iDragComponent?.onEndDrag();
                    iDragComponent.isMovebale = false;
                    draggedObject = null;
                    dragAndRotation.SetObjectRotation(draggedObject);
                    isDrag = false;
                    //hit.collider.GetComponent<IDrag>().isMovebale = isDrag;
                }

                if (hit.collider.GetComponent<SolderStationDetect>() != null && draggedObject == null 
                    && solderStationDetect.detect == false && playerController.IsActiveOrtoView() == false)
                {
                    hit.collider.GetComponent<SolderStationDetect>().StartMoveToStation();
                }

                if (hit.collider.GetComponent<PlugActive>() != null && draggedObject == null)
                {
                    hit.collider.GetComponent<PlugActive>().PlugInSocket();
                }

                if (hit.collider.GetComponent<SwitchOnOff>() != null && draggedObject == null)
                {
                    hit.collider.GetComponent<SwitchOnOff>().ButtonTurnOnOff();
                }

                if (hit.collider.GetComponent<TemperatureRegulatorSetting>() != null && draggedObject == null 
                    && solderStationDetect.detect == true)
                {
                    //    regulator = hit.collider.GetComponent<TemperatureRegulatorSetting>().transform;

                    //    pressPoint = Input.mousePosition;
                    //    startRotation = regulator.transform.localRotation;
                    //    startAxisRotationX = endAxisRotationX;

                    regulatorCheck = true;
                }

                if(hit.collider.GetComponent<CottonSwabSpawn>()!=null && draggedObject == null)
                {
                    hit.collider.GetComponent<CottonSwabSpawn>().SpawnCottonSwab();
                }
            }
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
    private void DragObject()
    {
        if (!isHoldingSolder)
        {
            if (Input.mouseScrollDelta.y > 0 && currentDistanceToObject < maxDist && playerController.IsActiveOrtoView() == false)
            {
                currentDistanceToObject += stepsDistance;
            }
            if (Input.mouseScrollDelta.y < 0 && currentDistanceToObject > minDist && playerController.IsActiveOrtoView() == false)
            {
                currentDistanceToObject -= stepsDistance;
            }

            draggedObject.TryGetComponent<Rigidbody>(out var rb);
            draggedObject.TryGetComponent<IDrag>(out var iDragComponent);
            iDragComponent?.onStartDrag();

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(currentDistanceToObject) - draggedObject.transform.position;
                rb.velocity = direction * mouseDragPhysicsSpeed;
            }
            else
            {
                draggedObject.transform.position = Vector3.SmoothDamp(draggedObject.transform.position, ray.GetPoint(currentDistanceToObject),
                   ref velocity, mouseDragSpeed);
            }
        }
        else
        {
            draggedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
       
    }

    private void Update()
    {
        if (isDrag && NonOpenPopupMenu() && checkOpenUIComponent.NonActiveUIComponent())
        {
            DragObject();
            SolderJobStartStop();
        }
        if(!NonOpenPopupMenu() && draggedObject!=null)
        {
            draggedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        //solderJob


        //test Regulator Rotation
        if (regulatorCheck)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider.GetComponent<TemperatureRegulatorSetting>() != null)
                {
                    settingRegulator = hit.collider.GetComponent<TemperatureRegulatorSetting>();
                    RotationRegulator();
                }
                else
                {
                    settingRegulator= null;
                    regulatorCheck = false;
                }
            }
                


            //if (mouseInput.ReadValue<float>() != 0)
            //{
            //    float currentDistanceBetweenMousePosition = (Input.mousePosition - pressPoint).x;

            //    endAxisRotationX = startAxisRotationX - currentDistanceBetweenMousePosition;

            //    Debug.Log(endAxisRotationX);
            //    regulator.transform.localRotation = startRotation * Quaternion.Euler
            //        (Vector3.left * currentDistanceBetweenMousePosition
            //        );

            //}
            //else
            //{
            //    regulatorCheck = false;
            //    regulator= null;
            //}
        }
    }

    private void SolderJobStartStop()
    {
        if (mouseInput.IsPressed() && isHoldMouse)
        {
            isHoldingSolder = true;
            draggedObject.GetComponent<SolderInteract>().StartSoldering();
        }
        else if (mouseInput.IsPressed() == false && isHoldMouse)
        {
            isHoldingSolder = false;
            draggedObject.GetComponent<SolderInteract>().StopSoldering();
        }
    }

    private void RotationRegulator()
    {
        if (Input.mouseScrollDelta.y > 0 && curretntIndexTemerature < 11)
        {
            curretntIndexTemerature += stepIndexTemperature;
            settingRegulator.SetRegulatorTemperature(curretntIndexTemerature);
        }
        if (Input.mouseScrollDelta.y < 0 && curretntIndexTemerature > 0)
        {
            curretntIndexTemerature -= stepIndexTemperature;
            settingRegulator.SetRegulatorTemperature(curretntIndexTemerature);
        }
    }

    public void ClearHand()
    {
        isDrag = false;
        draggedObject.GetComponent<IDrag>().isMovebale = isDrag;
        draggedObject.GetComponent<IDrag>().onEndDrag();
        draggedObject = null;
        dragAndRotation.SetObjectRotation(draggedObject);
    }
    public void SetDraggedObject(GameObject gameObject)
    {
        draggedObject= gameObject;
        isDrag = true;
        dragAndRotation.SetObjectRotation(draggedObject);
    }

    public void RemoveSolderHand()
    {
        draggedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.onEndDrag();
        iDragComponent.isMovebale = false;
        draggedObject = null;
        dragAndRotation.SetObjectRotation(draggedObject);
        isDrag = false;
        isHoldMouse = false;
    }

    public GameObject GetDraggedObject()
    {
        return draggedObject;
    }

    public void OrtoViewParam()
    {
        if (draggedObject.GetComponent<Tweezers>())
        {
            if(draggedObject.GetComponent<Tweezers>().IsLittleTweezers())
            {
                currentDistanceToObject = 0.19f;
            }
            
        }
        else if (draggedObject.GetComponent<SolderInteract>())
        {
            currentDistanceToObject = ortoViewDistance;
        }
        else if(draggedObject.GetComponent<CottonSwabControl>())
        {
            currentDistanceToObject = 0.19f;
        }
    }

    public void TweezersSlotSet()
    {

    }
    public void SetCustomCurrentDistanceToObject(float distance)
    {
        currentDistanceToObject = distance;
    }
}
