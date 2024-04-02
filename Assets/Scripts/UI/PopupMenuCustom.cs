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

    public void OpenPopupMenu(GameObject selectObject, TypeInterectableObject typeInterectable)
    {
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
        isSwap = !isSwap;

        if (isSwap)
        {
            buttonsMenuList[0].transform.GetChild(1).GetComponent<TMP_Text>().text = "�����������";

            dragAndRotation.enabled = true;
            draggedObject.GetComponent<RotationItem>()?.onStartRotation();
            dragAndDrop.enabled = false;
            draggedObject.GetComponent<TestDragItem>()?.onEndDrag();
        }
        else 
        {
            buttonsMenuList[0].transform.GetChild(1).GetComponent<TMP_Text>().text = "���������";

            dragAndRotation.enabled = false;
            draggedObject.GetComponent<RotationItem>()?.onStopRotation();
            dragAndDrop.enabled = true;
            draggedObject.GetComponent<TestDragItem>()?.onStartDrag();
        }
        ClosePopupMenu();
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
            buttonsMenuList[2].transform.GetChild(1).GetComponent<TMP_Text>().text = "�������������";
            drag.onFreeze(false);
        }
        else
        {
            buttonsMenuList[2].transform.GetChild(1).GetComponent<TMP_Text>().text = "�������������";
            drag.onFreeze(true);
            dragAndDrop.ClearHand();
        }
        ClosePopupMenu();
    }
    private void ClosePopupMenu()
    {
        isOpen=false;
        gameObject.SetActive(false);
    }
    private void CheckTypeInterectable(TypeInterectableObject type)
    {
        draggedObject.TryGetComponent<IDrag>(out var dragInfo);
       
        //�������� �� ��� ������� � ��������� ������������ ������� � ���������
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
}
