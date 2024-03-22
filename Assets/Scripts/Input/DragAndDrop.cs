using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private InputAction mouseInput;

    [SerializeField] private float mouseDragSpeed = 1f;
    [SerializeField] private float mouseDragPhysicsSpeed = 1f;
    [SerializeField] private float currentDistanceToObject;
    [SerializeField] private float stepsDistance = 0.2f;
    [SerializeField] private float minDist = 0.2f;
    [SerializeField] private float maxDist = 2f;

    private float distanceToObject = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private float mouseScrollY;

    [SerializeField] private bool isDrag;
    [SerializeField] GameObject draggedObject;

    private void Awake()
    {
        mainCamera = Camera.main;
        
        currentDistanceToObject = distanceToObject;
    }

    private void OnEnable()
    {
        mouseInput.Enable();
        mouseInput.performed += MousePressed;
    }
    private void OnDisable()
    {
        mouseInput.performed-= MousePressed;
        mouseInput.Disable();
    }

    private void MousePressed(InputAction.CallbackContext obj)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            if(hit.collider.GetComponent<IDrag>()!=null && draggedObject == null)
            {
                //StartCoroutine(DragUpdate(hit.collider.gameObject));
                draggedObject = hit.collider.gameObject;
                isDrag = true;
            }
            else if (draggedObject != null)
            {
                draggedObject.TryGetComponent<IDrag>(out var iDragComponent);
                iDragComponent?.onEndDrag();
                draggedObject = null;
                isDrag = false;
            }
        }
    }

    private void DragObject()
    {
        if (Input.mouseScrollDelta.y > 0 && currentDistanceToObject < maxDist)
        {
            currentDistanceToObject += stepsDistance;
            Debug.Log("da");
        }
        if (Input.mouseScrollDelta.y < 0 && currentDistanceToObject > minDist)
        {
            currentDistanceToObject -= stepsDistance;
            Debug.Log("net");
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
        if (isDrag)
        {
            if (Input.mouseScrollDelta.y > 0 && currentDistanceToObject < maxDist)
            {
                currentDistanceToObject += stepsDistance;
                Debug.Log("da");
            }
            if (Input.mouseScrollDelta.y < 0 && currentDistanceToObject > minDist)
            {
                currentDistanceToObject -= stepsDistance;
                Debug.Log("net");
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
    }
}
