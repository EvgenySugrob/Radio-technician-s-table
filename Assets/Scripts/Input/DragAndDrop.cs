using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Vmaya.UI.Menu;

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
    [SerializeField]private bool isDrag;
    [SerializeField]private bool isRotation;
    [SerializeField]private GameObject draggedObject;


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
        if (draggedObject != null && checkOpenUIComponent.NonActiveUIComponent())
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
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
        if (NonOpenPopupMenu() && checkOpenUIComponent.NonActiveUIComponent()) 
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,100f,layerMask))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider.GetComponent<IDrag>() != null && draggedObject == null)
                {       
                    draggedObject = hit.collider.gameObject;
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

                //if(hit.collider.GetComponent<SolderStationDetect>()!=null && draggedObject == null)
                //{
                //    hit.collider.GetComponent<SolderStationDetect>().StartMoveToStation();
                //}

                if(hit.collider.GetComponent<PlugActive>()!=null && draggedObject == null)
                {
                    hit.collider.GetComponent<PlugActive>().PlugInSocket();
                }

                if (hit.collider.GetComponent<SwitchOnOff>()!=null && draggedObject == null)
                {
                    hit.collider.GetComponent<SwitchOnOff>().ButtonTurnOnOff();
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
        if (Input.mouseScrollDelta.y > 0 && currentDistanceToObject < maxDist)
        {
            currentDistanceToObject += stepsDistance;
        }
        if (Input.mouseScrollDelta.y < 0 && currentDistanceToObject > minDist)
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

    private void Update()
    {
        if (isDrag && NonOpenPopupMenu() && checkOpenUIComponent.NonActiveUIComponent())
        {
            DragObject();
        }
        if(!NonOpenPopupMenu() && draggedObject!=null)
        {
            draggedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
}
