using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PopupMenuCustom : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] List<Button> buttonsMenuList;
    [SerializeField] GameObject draggedObject;
    [SerializeField] RectTransform rectTransform;

    [Header("DragDrop/DragRotation")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;

    [Header("Hand Slots")]
    [SerializeField] private HandSlotsManager handSlotsManager;

    [SerializeField]private bool isOpen;
    [SerializeField] private bool isSwap = false;

    [Header("ObjectToCheck")]
    [SerializeField] List<GameObject> objectInteractList;

    private string freezeBtName = "LockObj";

    public void OpenPopupMenu(GameObject selectObject, TypeInterectableObject typeInterectable)
    {
        isOpen = true;
        draggedObject = selectObject;
        rectTransform.position = Mouse.current.position.ReadValue();
        CheckTypeInterectable(typeInterectable);
        gameObject.SetActive(true);
    }

    public bool GetStatusPopupMenu()
    {
        return isOpen;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpen = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOpen= false;
        draggedObject = null;
        gameObject.SetActive(false);
    }

    public void RotationSwap()
    {
        Rotate();
        ClosePopupMenu();
    }
    public void SpacePressRotationSwap(GameObject selectedObject)
    {
        draggedObject= selectedObject;
        Rotate();
        draggedObject = null;
    }
    private void Rotate()
    {
        isSwap = !isSwap;

        if (isSwap)
        {
            buttonsMenuList[0].transform.GetChild(1).GetComponent<TMP_Text>().text = "Переместить";

            dragAndRotation.enabled = true;
            draggedObject.GetComponent<RotationItem>()?.onStartRotation();
            dragAndDrop.enabled = false;
            draggedObject.GetComponent<DragItem>()?.onEndDrag();
        }
        else
        {
            buttonsMenuList[0].transform.GetChild(1).GetComponent<TMP_Text>().text = "Повернуть";

            dragAndRotation.enabled = false;
            draggedObject.GetComponent<RotationItem>()?.onStopRotation();
            dragAndDrop.enabled = true;
            draggedObject.GetComponent<DragItem>()?.onStartDrag();
        }
    }
    public void TakeObject()
    {
        GameObject revDragObj = draggedObject;
        draggedObject.GetComponent<IDrag>().onFreeze(true);
        dragAndDrop.ClearHand();
        handSlotsManager.CheckHandSlot(revDragObj);

        ClosePopupMenu();
    }

    public void FreezeObject()
    {
        draggedObject.TryGetComponent<IDrag>(out var drag);

        if(drag.isFreeze)
        {
            Button freezeBt = buttonsMenuList.Find(s => s.name == freezeBtName);
            freezeBt.transform.GetChild(1).GetComponent<TMP_Text>().text = "Зафиксировать";
            drag.onFreeze(false);
        }
        else
        {
            Button freezeBt = buttonsMenuList.Find(s => s.name == freezeBtName);
            freezeBt.transform.GetChild(1).GetComponent<TMP_Text>().text = "Разблокирвать";
            drag.onFreeze(true);
            dragAndDrop.ClearHand();
        }
        ClosePopupMenu();
    }
    public void ClosePopupMenu()
    {
        isOpen=false;
        gameObject.SetActive(false);
    }
    private void CheckTypeInterectable(TypeInterectableObject type)
    {
        draggedObject.TryGetComponent<IDrag>(out var dragInfo);
       
        //проверка на тип объекта и включение определенных менюшек в сплывашке
        switch (type)
        {
            case TypeInterectableObject.FullInterectable:

                for (int i = 0; i < buttonsMenuList.Count; i++)
                {
                    buttonsMenuList[i].gameObject.SetActive(true);
                }
                break;

            case TypeInterectableObject.HalfInterectable:
                buttonsMenuList[2].gameObject.SetActive(false);
                break;

            case TypeInterectableObject.None:
                Debug.Log("CloseTheHole");
                break;
        }
    }

    public void ExtrimeFreezeObj(GameObject gameObject)
    {
        gameObject.TryGetComponent<IDrag>(out var drag);

        if (drag.isFreeze)
        {
            Button freezeBt = buttonsMenuList.Find(s => s.name == freezeBtName);
            freezeBt.transform.GetChild(1).GetComponent<TMP_Text>().text = "Зафиксировать";
            drag.onFreeze(false);
        }
        else
        {
            Button freezeBt = buttonsMenuList.Find(s => s.name == freezeBtName);
            freezeBt.transform.GetChild(1).GetComponent<TMP_Text>().text = "Разблокирвать";
            drag.onFreeze(true);
        }
    }
}
