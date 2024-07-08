using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressButtonRotationModeSwap : MonoBehaviour
{
    [SerializeField] InputAction spaceKey;
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;

    [SerializeField] private GameObject draggedObject;
    [SerializeField] OrtoViewCameraPosition ortoView;

    private void OnEnable()
    {
        spaceKey.Enable();
        spaceKey.performed += SpaceKey_performed;
    }
    private void OnDisable()
    {
        spaceKey.performed-= SpaceKey_performed;
        spaceKey.Disable(); 
    }

    private void SpaceKey_performed(InputAction.CallbackContext obj)
    {
        if(draggedObject != null && ortoView.isOrtoViewActive ==false)
        {
            draggedObject.TryGetComponent<PopupMenuObjectType>(out var popupObj);
            popupObj.RotateObjectSpacePress();
        }
    }

    public void SetDraggedObject(GameObject draggObject)
    {
        draggedObject= draggObject;
    }
}
