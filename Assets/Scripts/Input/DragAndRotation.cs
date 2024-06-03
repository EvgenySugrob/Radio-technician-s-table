using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndRotation : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputAction mouseInput;
    [SerializeField] private InputAction mouseRightInput;
    [SerializeField] private InputAction axis;

    [Header("Setting")]
    [SerializeField] private float speedRotation;
    [SerializeField] Camera mainCamera;
    [SerializeField] private bool isRotate;
    [SerializeField] private bool isInvert;
    [SerializeField] private GameObject draggedObject;
    private Vector2 rotation;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseInput.Enable();
        mouseRightInput.Enable();
        axis.Enable();

        mouseInput.performed += x => { isRotate = true; };
        mouseInput.canceled += x => { isRotate = false; };
        mouseRightInput.performed += MouseRightInput;
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    private void MouseRightInput(InputAction.CallbackContext obj)
    {
        if (draggedObject != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<IRotation>() != null && draggedObject == hit.collider.gameObject)
                {
                    draggedObject.TryGetComponent<PopupMenuObjectType>(out var popupMenu);
                    popupMenu.Show();
                }
            }
            
        }
    }

    private void OnDisable()
    {
        mouseInput.Disable();
        mouseRightInput.Disable();
        axis.Disable();
    }

    private void Update()
    {
        if(isRotate)
        {
            Debug.Log(rotation);
            rotation *= speedRotation;
            draggedObject.transform.Rotate(Vector3.down, rotation.x,Space.World);
            draggedObject.transform.Rotate(mainCamera.transform.right * (isInvert?-1:1), rotation.y,Space.World);
        }
    }

    public void SetObjectRotation(GameObject gameObject)
    {
        draggedObject= gameObject;
    }
}
