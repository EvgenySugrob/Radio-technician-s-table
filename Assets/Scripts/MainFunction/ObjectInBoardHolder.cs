using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInBoardHolder : MonoBehaviour
{
    [SerializeField] Transform holderPoint;
    [SerializeField] BoardFixInHolder boardFixInHolder;

    [SerializeField] DragAndDrop dragAndDrop;

    public void ObjcetInHolder()
    {
        dragAndDrop.ClearHand();

        transform.position = holderPoint.position;
        transform.rotation = holderPoint.rotation;
        transform.parent = holderPoint.parent;

        boardFixInHolder.DisableTriggerZone(false);
    }
}
