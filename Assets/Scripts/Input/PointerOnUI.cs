using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOnUI : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        dragAndDrop.PointerState(true);
        dragAndRotation.PointerState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dragAndDrop.PointerState(false);
        dragAndRotation.PointerState(false);
    }
}
